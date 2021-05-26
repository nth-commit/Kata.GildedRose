using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutRegularItems
    {
        [Property]
        public Property SellInDecrements() => Properties.SellInDecrements(DomainGen.RegularItem());

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
