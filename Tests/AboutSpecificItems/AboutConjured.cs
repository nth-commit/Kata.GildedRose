using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests.AboutSpecificItems
{
    public class AboutConjured
    {
        [Property]
        public Property SellInDecrements() => Properties.SellInDecrements(DomainGen.Conjured());

        [Property]
        public IGen<Test> IfSellInIsFuture_ItDegradesByTwo() =>
            from initial in DomainGen.Conjured(
                sellIn: DomainGen.SellIn(RelativeSellIn.Future),
                quality: DomainGen.Quality(min: 2))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality - 2, result.Quality);
            });

        [Property]
        public IGen<Test> IfSellInHasPassed_ItDegradesByFour() =>
            from initial in DomainGen.Conjured(
                sellIn: DomainGen.SellIn(RelativeSellIn.PastOrPresent),
                quality: DomainGen.Quality(min: 4))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality - 4, result.Quality);
            });
    }
}
