using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutRegularItemQuality
    {
        [Property]
        public IGen<Test> SellInDecrements() =>
            from initial in DomainGen.RegularItem()
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.SellIn - 1, result.SellIn);
            });

        [Property]
        public IGen<Test> IfSellInIsFuture_ItDegradesByOne() =>
            from initial in DomainGen.RegularItem(
                sellIn: DomainGen.SellIn(RelativeSellIn.Future),
                quality: DomainGen.Quality(min: 1))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality - 1, result.Quality);
            });

        [Property]
        public IGen<Test> IfSellInHasPassed_ItDegradesByTwo() =>
            from initial in DomainGen.RegularItem(
                sellIn: DomainGen.SellIn(RelativeSellIn.PastOrPresent),
                quality: DomainGen.Quality(min: 2))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality - 2, result.Quality);
            });
    }
}
