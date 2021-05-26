using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutSulfuras
    {
        public class SulfurasAttribute : GenAttribute
        {
            public override IGen Value => DomainGen.Sulfuras();
        }

        [Property]
        [Replay("H4sIAAAAAAAACjO1NDIzNDQyMdIzNNMDAM1CHLINAAAA")]
        public void ItNeverDegrades([Sulfuras] TestProxy.Item sulfuras)
        {
            var sulfuras0 = TestProxy.UpdateQuality(sulfuras);

            Assert.Equal(sulfuras, sulfuras0);
        }
    }
}
