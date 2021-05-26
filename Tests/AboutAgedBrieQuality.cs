using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutAgedBrieQuality
    {
        private class AgedBrieAttribute : GenAttribute
        {
            public override IGen Value => DomainGen.AgedBrie();
        }

        [Property]
        public void IfSellInIsFuture_ItIncreasesByOne([AgedBrie] TestProxy.Item item)
        {
            Property.Precondition(item.Quality <= 49 && item.SellIn > 0);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(item.Quality + 1, result.Quality);
        }

        [Property]
        public void IfSellInHasPassed_ItDegradesByTwo([AgedBrie] TestProxy.Item item)
        {
            Property.Precondition(item.Quality <= 48 && item.SellIn <= 0);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(item.Quality + 2, result.Quality);
        }
    }
}
