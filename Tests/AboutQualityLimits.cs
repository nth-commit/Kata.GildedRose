using GalaxyCheck;
using GalaxyCheck.Gens.Injection.Int32;
using Xunit;
using static csharpcore.Tests.DomainGen;

namespace csharpcore.Tests
{
    public class AboutQualityLimits
    {
        [Property]
        public void QualityIsNeverNegative(
            [Item] TestProxy.Item item,
            [Between(1, 50)] int numberOfDays)
        {
            for (var day = 0; day < numberOfDays; day++)
            {
                item = TestProxy.UpdateQuality(item);
                Assert.True(item.Quality >= 0);
            }
        }

        [Property]
        public void QualityNeverExceedsFifty(
            [Item] TestProxy.Item item,
            [Between(1, 50)] int numberOfDays)
        {
            for (var day = 0; day < numberOfDays; day++)
            {
                item = TestProxy.UpdateQuality(item);
                Assert.True(item.Quality <= 50);
            }
        }
    }
}
