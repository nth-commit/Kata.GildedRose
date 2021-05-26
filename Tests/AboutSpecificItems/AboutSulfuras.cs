using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests.AboutSpecificItems
{
    public class AboutSulfurasQuality
    {
        public class SulfurasAttribute : GenAttribute
        {
            public override IGen Value => DomainGen.Sulfuras();
        }

        [Property]
        public void ItNeverDegrades([Sulfuras] TestProxy.Item initial)
        {
            var result = TestProxy.UpdateQuality(initial);

            Assert.Equal(initial, result);
        }
    }
}
