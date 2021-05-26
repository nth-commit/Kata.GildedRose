using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutRegularItemQuality
    {
        private class RegularItemAttribute : GenAttribute
        {
            public override IGen Value => DomainGen.RegularItem();
        }

        [Property]
        public void IfSellInIsFuture_ItDegradesByOne([RegularItem] TestProxy.Item item)
        {
            Property.Precondition(item.Quality >= 1 && item.SellIn > 0);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(item.Quality - 1, result.Quality);
        }

        [Property]
        public void IfSellInHasPassed_ItDegradesByTwo([RegularItem] TestProxy.Item item)
        {
            Property.Precondition(item.Quality >= 2 && item.SellIn <= 0);

            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(item.Quality - 2, result.Quality);
        }
    }
}
