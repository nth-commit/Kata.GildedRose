using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests.AboutSpecificItems
{
    public class AboutConcertTickets
    {
        [Property]
        public IGen<Test> SellInDecrements() =>
            from initial in DomainGen.ConcertTickets()
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.SellIn - 1, result.SellIn);
            });

        [Property]
        public IGen<Test> IfConcertIsMoreThan10DaysInFuture_QualityIncreasesByOne() =>
            from initial in DomainGen.ConcertTickets(
                sellIn: DomainGen.SellIn(min: 11),
                quality: DomainGen.Quality(max: 49))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality + 1, result.Quality);
            });

        [Property]
        public IGen<Test> IfConcertIsBetweenFiveAndTenDaysInFuture_QualityIncreasesByTwo() =>
            from initial in DomainGen.ConcertTickets(
                sellIn: DomainGen.SellIn(6, 10),
                quality: DomainGen.Quality(max: 48))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality + 2, result.Quality);
            });

        [Property]
        public IGen<Test> IfConcertIsWithinFiveDaysInFuture_QualityIncreasesByThree() =>
            from initial in DomainGen.ConcertTickets(
                sellIn: DomainGen.SellIn(1, 5),
                quality: DomainGen.Quality(max: 47))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(initial.Quality + 3, result.Quality);
            });

        [Property]
        public IGen<Test> IfConcertHasPassed_QualityBecomesZero() =>
            from initial in DomainGen.ConcertTickets(sellIn: DomainGen.SellIn(RelativeSellIn.PastOrPresent))
            select Property.ForThese(() =>
            {
                var result = TestProxy.UpdateQuality(initial);

                Assert.Equal(0, result.Quality);
            });
    }
}
