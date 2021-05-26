using GalaxyCheck;
using System.Linq;
using Xunit;

namespace csharpcore.Tests
{
    public static class Properties
    {
        public static Property SellInDecrements(IGen<TestProxy.Item> item) => item.ForAll(initial =>
        {
            var result = TestProxy.UpdateQuality(initial);

            Assert.Equal(initial.SellIn - 1, result.SellIn);
        });
    }
}
