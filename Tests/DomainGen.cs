using GalaxyCheck;
using GalaxyCheck.Gens;

namespace csharpcore.Tests
{
    public static class DomainGen
    {
        public static IIntGen<int> Quality() => Gen.Int32().Between(0, 50);

        public class QualityAttribute : GenAttribute
        {
            public override IGen Value => Quality();
        }

        public static IIntGen<int> SellIn() => Gen.Int32().Between(-10_000, 10_000);

        public class SellInAttribute : GenAttribute
        {
            public override IGen Value => SellIn();
        }

        private static IReflectedGen<TestProxy.Item> BaseItemGen() => Gen
            .Create<TestProxy.Item>()
            .OverrideMember(x => x.Quality, Quality())
            .OverrideMember(x => x.SellIn, SellIn());

        public static IGen<TestProxy.Item> RegularItem() => BaseItemGen();

        public static IGen<TestProxy.Item> AgedBrie() =>
            BaseItemGen().OverrideMember(x => x.Name, Gen.Constant("Aged Brie"));

        public static IGen<TestProxy.Item> Sulfuras() =>
            BaseItemGen().OverrideMember(x => x.Name, Gen.Constant("Sulfuras, Hand of Ragnaros"));

        public static IGen<TestProxy.Item> Item() => Gen.Choose(RegularItem(), AgedBrie(), Sulfuras());
    }
}