using GalaxyCheck;
using GalaxyCheck.Gens;
using System;

namespace csharpcore.Tests
{
    public static class DomainGen
    {
        public static IIntGen<int> Quality(int min = 0, int max = 50) => Gen.Int32().Between(min, max);

        public static IIntGen<int> SellIn() => Gen.Int32().Between(-10_000, 10_000);

        public static IIntGen<int> SellIn(RelativeSellIn sellIn = RelativeSellIn.PastOrPresent | RelativeSellIn.Future)
        {
            return sellIn switch
            {
                RelativeSellIn.PastOrPresent => SellIn(max: 0),
                RelativeSellIn.Future => SellIn(min: 1),
                _ => SellIn()
            };
        }

        public static IIntGen<int> SellIn(int min = -10_000, int max = 10_000) => Gen.Int32().Between(min, max);

        private static IReflectedGen<TestProxy.Item> BaseItemGen(
            IGen<int>? genSellIn = null,
            IGen<int>? genQuality = null) => Gen
                .Create<TestProxy.Item>()
                .OverrideMember(x => x.SellIn, genSellIn ?? SellIn())
                .OverrideMember(x => x.Quality, genQuality ?? Quality());

        private static IReflectedGen<TestProxy.Item> WithName(this IReflectedGen<TestProxy.Item> gen, string name) =>
            gen.OverrideMember(x => x.Name, Gen.Constant(name));

        public static IGen<TestProxy.Item> RegularItem(
            IGen<int>? sellIn = null,
            IGen<int>? quality = null) => BaseItemGen(sellIn, quality);

        public static IGen<TestProxy.Item> AgedBrie(
            IGen<int>? sellIn = null,
            IGen<int>? quality = null) =>
                BaseItemGen(sellIn, quality).WithName("Aged Brie");

        public static IGen<TestProxy.Item> ConcertTickets(
            IGen<int>? sellIn = null,
            IGen<int>? quality = null) =>
                BaseItemGen(sellIn, quality).WithName("Backstage passes to a TAFKAL80ETC concert");

        public static IGen<TestProxy.Item> Conjured(
            IGen<int>? sellIn = null,
            IGen<int>? quality = null) =>
                BaseItemGen(sellIn, quality).WithName("Conjured");

        public static IGen<TestProxy.Item> Sulfuras() =>
            BaseItemGen().WithName("Sulfuras, Hand of Ragnaros");

        public static IGen<TestProxy.Item> Item() => Gen.Choose(RegularItem(), AgedBrie(), ConcertTickets(), Conjured(), Sulfuras());

        public class ItemAttribute : GenAttribute
        {
            public override IGen Value => Item();
        }
    }

    [Flags]
    public enum RelativeSellIn
    {
        PastOrPresent = 1,
        Future = 2
    }
}