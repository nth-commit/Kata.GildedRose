namespace csharpcore.Domain
{
    public interface ISellInClock
    {
        int Tick(Item item);
    }

    public static class SellInClocks
    {
        private class TickingQualityClock : ISellInClock
        {
            private readonly int _tickAmount;

            public TickingQualityClock(int tickAmount)
            {
                _tickAmount = tickAmount;
            }

            public int Tick(Item item) => item.SellIn + _tickAmount;
        }

        public static ISellInClock Decrement => new TickingQualityClock(-1);

        public static ISellInClock Noop => new TickingQualityClock(0);
    }
}
