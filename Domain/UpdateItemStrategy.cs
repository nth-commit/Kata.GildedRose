namespace csharpcore.Domain
{
    public record UpdateItemStrategy(
        string ItemName,
        IQualityClock Quality,
        ISellInClock SellIn);
}
