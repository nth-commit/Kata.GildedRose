# GildedRose Kata (C#)

Models the internals of the `UpdateQuality` function as a pair of "clocks". One clock ticks for quality, the other ticks for expiry (SellIn).

The clocks provide a nice lil DSL for configuring their rules, and you can cascade rules using the `If`/`ElseIf`/`Else` functions.

```csharp
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
```
