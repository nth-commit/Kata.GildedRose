using csharpcore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class GildedRose
    {
        private static readonly IQualityClock DefaultQualityClock = QualityClocks
            .If(item => item.SellIn <= 0, QualityClocks.TickWith(-2))
            .Else(QualityClocks.TickWith(-1));

        private static readonly ISellInClock DefaultSellInClock = SellInClocks.Decrement;

        private static readonly IReadOnlyList<UpdateItemStrategy> Strategies = new List<UpdateItemStrategy>
        {
            new UpdateItemStrategy(
                "Aged Brie",
                QualityClocks
                    .If(item => item.SellIn <= 0, QualityClocks.TickWith(2))
                    .Else(QualityClocks.TickWith(1)),
                SellInClocks.Decrement),

            new UpdateItemStrategy(
                "Backstage passes to a TAFKAL80ETC concert",
                QualityClocks
                    .If(item => item.SellIn <= 0, QualityClocks.SetTo(0))
                    .ElseIf(item => item.SellIn <= 5, QualityClocks.TickWith(3))
                    .ElseIf(item => item.SellIn <= 10, QualityClocks.TickWith(2))
                    .Else(QualityClocks.TickWith(1)),
                SellInClocks.Decrement),

            new UpdateItemStrategy(
                "Sulfuras, Hand of Ragnaros",
                QualityClocks.Noop,
                SellInClocks.Noop),

            new UpdateItemStrategy(
                "Conjured",
                QualityClocks
                    .If(item => item.SellIn <= 0, QualityClocks.TickWith(-4))
                    .Else(QualityClocks.TickWith(-2)),
                SellInClocks.Decrement),
        };

        private readonly IList<Item> _items;
        private readonly IQualityClock _defaultQualityClock;
        private readonly ISellInClock _defaultSellInClock;
        private readonly IReadOnlyDictionary<string, UpdateItemStrategy> _updateItemStrategiesByName;

        public GildedRose(IList<Item> items)
            : this(items, DefaultQualityClock, DefaultSellInClock, Strategies)
        {
        }

        public GildedRose(
            IList<Item> items,
            IQualityClock defaultQualityClock,
            ISellInClock defaultSellInClock,
            IReadOnlyList<UpdateItemStrategy> updateItemStrategies)
        {
            _items = items;
            _defaultQualityClock = defaultQualityClock;
            _defaultSellInClock = defaultSellInClock;
            _updateItemStrategiesByName = updateItemStrategies.ToDictionary(s => s.ItemName);
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                UpdateOneItem(_defaultQualityClock, _defaultSellInClock, _updateItemStrategiesByName, item);
            }
        }

        private static void UpdateOneItem(
            IQualityClock _defaultQualityClock,
            ISellInClock _defaultSellInClock,
            IReadOnlyDictionary<string, UpdateItemStrategy> updateItemStrategiesByName,
            Item item)
        {
            var qualityClock = _defaultQualityClock;
            var sellInClock = _defaultSellInClock;

            if (updateItemStrategiesByName.TryGetValue(item.Name, out var strategy))
            {
                qualityClock = strategy.Quality;
                sellInClock = strategy.SellIn;
            }

            var domainItem = new Domain.Item(item.Name, item.SellIn, item.Quality);
            var nextQuality = qualityClock.Tick(domainItem);
            var nextSellIn = sellInClock.Tick(domainItem);

            item.Quality = nextQuality;
            item.SellIn = nextSellIn;
        }
    }
}
