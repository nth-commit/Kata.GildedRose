using System.Collections.Generic;
using System.Linq;

namespace csharpcore.Tests
{
    /// <summary>
    /// Provides an immutable interface to the GildedRose function, which makes the tests cleaner. 
    /// </summary>
    public class TestProxy
    {
        public static Item UpdateQuality(Item item) => UpdateQuality(new List<Item> { item }).Single();

        public static IReadOnlyList<Item> UpdateQuality(IReadOnlyList<Item> proxyItems)
        {
            var items = proxyItems.Select(MapFromProxyItem).ToList();

            new GildedRose(items).UpdateQuality();

            return items.Select(MapToProxyItem).ToList();
        }

        private static csharpcore.Item MapFromProxyItem(Item item) => new csharpcore.Item
        {
            Name = item.Name,
            SellIn = item.SellIn,
            Quality = item.Quality
        };

        private static Item MapToProxyItem(csharpcore.Item item) => new Item(
            item.Name,
            item.SellIn,
            item.Quality);

        public record Item(
            string Name,
            int SellIn,
            int Quality);
    }
}
