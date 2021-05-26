using GalaxyCheck;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutName
    {
        [Property]
        public void ItDoesNotMutateItemName(TestProxy.Item item)
        {
            var result = TestProxy.UpdateQuality(item);

            Assert.Equal(result.Name, item.Name);
        }
    }
}
