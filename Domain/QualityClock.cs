using System;
using System.Collections.Immutable;
using System.Linq;

namespace csharpcore.Domain
{
    public interface IQualityClock
    {
        int Tick(Item item);
    }

    public static class QualityClocks
    {
        private class StoppedQualityClock : IQualityClock
        {
            private readonly int? _value;

            public StoppedQualityClock(int? value)
            {
                _value = value;
            }

            public int Tick(Item item) => _value ?? item.Quality;
        }

        private class TickingQualityClock : IQualityClock
        {
            private readonly int _tickAmount;

            public TickingQualityClock(int tickAmount)
            {
                _tickAmount = tickAmount;
            }

            public int Tick(Item item) => Math.Min(Math.Max(item.Quality + _tickAmount, 0), 50);
        }

        public static IQualityClock SetTo(int value) => new StoppedQualityClock(value);

        public static IQualityClock TickWith(int tick) => new TickingQualityClock(tick);

        public static IQualityClock Noop => new StoppedQualityClock(null);

        public static IQualityClockConditionalBuilder If(Func<Item, bool> pred, IQualityClock clock) =>
            new QualityClockConditionalBuilder(ImmutableList.Create<(Func<Item, bool> pred, IQualityClock clock)>().Add((pred, clock)));

        private record QualityClockConditionalBuilder(
            ImmutableList<(Func<Item, bool> pred, IQualityClock clock)> Matchers) : IQualityClockConditionalBuilder
        {
            public IQualityClockConditionalBuilder ElseIf(Func<Item, bool> pred, IQualityClock clock) =>
                new QualityClockConditionalBuilder(Matchers.Add((pred, clock)));

            public IQualityClock Else(IQualityClock clock) =>
                new QualityClockConditionalBuilder(Matchers.Add(((_) => true, clock)));

            public int Tick(Item item)
            {
                var clock = Matchers
                    .Where(m => m.pred(item))
                    .Select(m => m.clock)
                    .First();

                return clock.Tick(item);
            }
        }
    }

    public interface IQualityClockConditionalBuilder : IQualityClock
    {
        IQualityClockConditionalBuilder ElseIf(Func<Item, bool> pred, IQualityClock clock);

        IQualityClock Else(IQualityClock clock);
    }
}
