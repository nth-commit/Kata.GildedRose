using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests.AboutSpecificItems
{
    public class AboutAgedBrie
    {
        [Property]
        public Property SellInDecrements() => Properties.SellInDecrements(DomainGen.AgedBrie());

        [Property]
        public IGen<Test> IfSellInIsFuture_ItIncreasesByOne() =>
            from initial in DomainGen.AgedBrie(
                sellIn: DomainGen.SellIn(RelativeSellIn.Future),
                quality: DomainGen.Quality(max: 49))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality + 1, result.Quality);
            });

        [Property]
        public IGen<Test> IfSellInHasPassed_ItIncreasesByTwo() =>
            from initial in DomainGen.AgedBrie(
                sellIn: DomainGen.SellIn(RelativeSellIn.PastOrPresent),
                quality: DomainGen.Quality(max: 48))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality + 2, result.Quality);
            });
    }
}
