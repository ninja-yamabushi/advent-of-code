using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.AdventOfCode.Solutions.Year2020.Day13;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle13_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var values = inputs.SplitByLine();
            var timestamp = int.Parse(values[0]);
            var scheddule = Scheddule.Parse(values[1]);

            return scheddule.FindMinutesToWaitFromTimestamp(timestamp).ToString();
        }
    }

    public class Puzzle13_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var values = inputs.SplitByLine();
            var scheddule = Scheddule.Parse(values[1]);

            return scheddule.FindEarliestConsecutiveDeparture().ToString();
        }
    }

    namespace AdventOfCode.Solutions.Year2020.Day13
    {
        public class Shuttle
        {
            public int Id { get; }
            public int Index { get; }

            public bool IsValid { get; }

            public Shuttle(string id, int index)
            {
                IsValid = id.IsNumeric();
                if (IsValid) Id = int.Parse(id);
                Index = index;
            }

            public bool DepartsAt(long timestamp)
            {
                return timestamp % Id == 0;
            }

            public IEnumerable<long> Departures
            {
                get
                {
                    long timestamp = 0;
                    while (true) yield return timestamp += Id;
                }
            }

            public override string ToString()
            {
                return $"[{Index}] {Id}";
            }
        }

        public class Scheddule
        {
            private readonly Shuttle[] shuttles;

            public Scheddule(Shuttle[] shuttles)
            {
                this.shuttles = shuttles;
            }

            public int FindMinutesToWaitFromTimestamp(int timestamp)
            {
                int actualTimestamp = timestamp;
                Shuttle bus = null;
                while (bus == null)
                {
                    bus = findBusLeavingAt(actualTimestamp);
                    if (bus == null) actualTimestamp++;
                }

                return (actualTimestamp - timestamp) * bus.Id;
            }
            private Shuttle findBusLeavingAt(int timestamp)
            {
                return shuttles.Where(s => s.DepartsAt(timestamp)).FirstOrDefault();
            }

            public long FindEarliestConsecutiveDeparture()
            {
                var bus = shuttles[0];
                var availableBuses = shuttles.Subset(1);

                foreach (var departureTime in bus.Departures)
                {
                    if (followersMatche(bus, availableBuses, departureTime))
                        return departureTime;
                }
                return -1;
            }

            private bool followersMatche(Shuttle bus, Shuttle[] availableBuses, long departureTime)
            {
                foreach (var shuttle in availableBuses)
                    if (!shuttle.DepartsAt(departureTime + (shuttle.Index - bus.Index)))
                        return false;
                return true;
            }


            public static Scheddule Parse(string inputs)
            {
                int index = 0;
                var allShuttles = inputs.SplitOn(",").Select(s => new Shuttle(s, index++)).ToArray();
                var validShuttles = allShuttles.Where(s => s.IsValid).ToArray();

                return new Scheddule(validShuttles);
            }
        }
    }
}

