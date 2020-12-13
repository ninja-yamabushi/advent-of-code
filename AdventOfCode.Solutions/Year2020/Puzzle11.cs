using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle11_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var room = new WaitingArea(inputs);
            var prediction = new Predictions();

            prediction.ApplyPrediction(room, 4, 1);

            return room.CountOccupiedPlaces().ToString();
        }
    }
    public class Puzzle11_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var room = new WaitingArea(inputs);
            var prediction = new Predictions();

            prediction.ApplyPrediction(room, 5);

            return room.CountOccupiedPlaces().ToString();
        }
    }

    public class Predictions
    {
        public void ApplyPrediction(WaitingArea area, int tolerance, long distance = long.MaxValue)
        {
            var count = 0;
            while (ApplyPredictionTo(area, tolerance, distance))
                count++;
        }

        private bool ApplyPredictionTo(WaitingArea area, int tolerance, long distance = long.MaxValue)
        {
            var snapshot = area.Clone();

            var changed = false;
            for (int rIndex = 0; rIndex < snapshot.RowCount; rIndex++)
                for (int cIndex = 0; cIndex < snapshot.ColCount; cIndex++)
                {
                    var staticChair = snapshot[cIndex, rIndex];
                    var chair = area[cIndex, rIndex];
                    if (!staticChair.IsSeat) continue;

                    var adjacentSeats = staticChair.FindAdjacentOrVisibleSeats(distance);
                    if (staticChair.IsEmpty)
                    {
                        if (CountOccupiedIn(adjacentSeats) == 0)
                        {
                            chair.ChangeState();
                            changed = true;
                        }
                    }
                    else
                    {
                        if (CountOccupiedIn(adjacentSeats) >= tolerance)
                        {
                            chair.ChangeState();
                            changed = true;
                        }
                    }
                }

            return changed;
        }

        private int CountOccupiedIn(IPlace[] seats)
        {
            return seats.Count(s => s.IsOccupied);
        }
    }

    public class WaitingArea
    {
        private List<IPlace[]> places;
        public int RowCount { get; }
        public int ColCount { get; }

        public WaitingArea(string inputs)
        {
            places = new List<IPlace[]>();
            inputs.SplitByLine().ToList().ForEach(line => places.Add(line.ToCharArray().Select(place => Chair.Parse(place, this)).ToArray()));

            RowCount = places.Count;
            ColCount = places[0].Length;

            for (int rIndex = 0; rIndex < RowCount; rIndex++)
                for (int cIndex = 0; cIndex < ColCount; cIndex++)
                    this[cIndex, rIndex].AssignPosition(rIndex, cIndex);
        }

        public IPlace this[int col, int row]
        {
            get { return places[row][col]; }
        }

        public WaitingArea Clone()
        {
            return new WaitingArea(ToString());
        }

        public int CountOccupiedPlaces()
        {
            return places.Select(row => row.Count(s => s.IsSeat && s.IsOccupied)).Sum();
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            for (int row = 0; row < RowCount; row++)
            {
                if (row > 0) output.AppendLine();
                for (int column = 0; column < ColCount; column++)
                    output.Append(places[row][column].ToString());
            }
            return output.ToString();
        }
    }

    public interface IPlace
    {
        bool IsSeat { get; }
        bool IsOccupied { get; }
        bool IsEmpty { get; }

        void ChangeState();
        void AssignPosition(int row, int coll);
        IPlace[] FindAdjacentOrVisibleSeats(long distance = long.MaxValue);
    }

    public class Chair : IPlace
    {
        private readonly WaitingArea area;
        private char state;
        private int myRow;
        private int myCol;

        public Chair(char input, WaitingArea area)
        {
            this.state = input;
            this.area = area;
        }

        public bool IsSeat => true;

        public bool IsOccupied => state == '#';

        public bool IsEmpty => state == 'L';

        public void ChangeState()
        {
            if (IsEmpty)
                state = '#';
            else
                state = 'L';
        }
        public void AssignPosition(int row, int coll)
        {
            this.myRow = row;
            this.myCol = coll;
        }

        public IPlace[] FindAdjacentOrVisibleSeats(long distance = long.MaxValue)
        {
            var result = new List<IPlace>();

            AddFirstChairFound(result, PlaceVariants.Decrease, PlaceVariants.Decrease, distance);
            AddFirstChairFound(result, PlaceVariants.Decrease, PlaceVariants.None, distance);
            AddFirstChairFound(result, PlaceVariants.Decrease, PlaceVariants.Increase, distance);
            AddFirstChairFound(result, PlaceVariants.None, PlaceVariants.Decrease, distance);
            AddFirstChairFound(result, PlaceVariants.None, PlaceVariants.Increase, distance);
            AddFirstChairFound(result, PlaceVariants.Increase, PlaceVariants.Decrease, distance);
            AddFirstChairFound(result, PlaceVariants.Increase, PlaceVariants.None, distance);
            AddFirstChairFound(result, PlaceVariants.Increase, PlaceVariants.Increase, distance);

            return result.ToArray();
        }

        public enum PlaceVariants
        { Increase = 1, None = 0, Decrease = -1 }
        private void AddFirstChairFound(List<IPlace> found, PlaceVariants xVariant, PlaceVariants yVariant, long distance = long.MaxValue)
        {
            bool done = false;
            int x = myCol;
            int y = myRow;
            long iteration = 0;

            while (!done)
            {
                if (iteration == distance) return;

                x = x + (int)xVariant;
                y = y + (int)yVariant;

                if (x < 0 || x >= area.ColCount) return;
                if (y < 0 || y >= area.RowCount) return;

                var place = area[x, y];
                if (place.IsSeat)
                {
                    found.Add(place);
                    done = true;
                }
                iteration++;
            }
        }

        public override string ToString()
        {
            return state.ToString();
        }


        public static IPlace Parse(char input, WaitingArea area)
        {
            if (input == '.') return new Floor();
            return new Chair(input, area);
        }


        private class Floor : IPlace
        {
            public bool IsSeat => false;
            public bool IsOccupied => throw new System.NotImplementedException();

            public bool IsEmpty => throw new System.NotImplementedException();

            public void AssignPosition(int row, int coll)
            {
            }

            public void ChangeState()
            {
                throw new System.NotImplementedException();
            }

            public IPlace[] FindAdjacentOrVisibleSeats(long distance)
            {
                throw new System.NotImplementedException();
            }

            public override string ToString()
            {
                return ".";
            }
        }
    }
}

