using GalaxyCheck;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace csharpcore.Tests
{
    public class AboutBatching
    {
        public class ItemsAttribute : GenAttribute
        {
            public override IGen Value => DomainGen.Item().ListOf();
        }

        [Property]
        public void UpdatingManyItemsIsTheSameAsUpdatingEachItemIndividually(
            [Items] IReadOnlyList<TestProxy.Item> items)
        {
            var items0 = items.Select(item => TestProxy.UpdateQuality(item)).ToList();
            var items1 = TestProxy.UpdateQuality(items);

            Assert.Equal(items0, items1);
        }
    }
}
