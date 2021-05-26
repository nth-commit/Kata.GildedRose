using GalaxyCheck;
using GalaxyCheck.Gens.Injection.Int32;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutConcertTickets
    {
        [Property]
        public void IfConcertIsMoreThan10DaysInFuture_QualityIncreasesByOne(
            [GreaterThanEqual(11)] int sellIn,
            [Between(0, 49)] int quality)
        {
            var item = new TestProxy.Item("Backstage passes to a TAFKAL80ETC concert", sellIn, quality);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(item.Quality + 1, result.Quality);
        }

        [Property]
        public void IfConcertIsBetweenFiveAndTenDaysInFuture_QualityIncreasesByTwo(
            [Between(6, 10)] int sellIn,
            [Between(0, 48)] int quality)
        {
            var item = new TestProxy.Item("Backstage passes to a TAFKAL80ETC concert", sellIn, quality);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(item.Quality + 2, result.Quality);
        }

        [Property]
        public void IfConcertIsWithinFiveDaysInFuture_QualityIncreasesByThree(
            [Between(1, 5)] int sellIn,
            [Between(0, 47)] int quality)
        {
            var item = new TestProxy.Item("Backstage passes to a TAFKAL80ETC concert", sellIn, quality);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(item.Quality + 3, result.Quality);
        }

        [Property]
        public void IfConcertHasPassed_QualityBecomesZero(
            [Between(0, -10_000)] int sellIn,
            int quality)
        {
            var item = new TestProxy.Item("Backstage passes to a TAFKAL80ETC concert", sellIn, quality);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(0, result.Quality);
        }
    }
}
