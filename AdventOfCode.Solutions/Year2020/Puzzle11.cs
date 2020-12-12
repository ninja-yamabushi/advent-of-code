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

            prediction.ApplyPrediction1(room);

            return room.CountOccupiedPlaces().ToString();
        }
    }
    public class Puzzle11_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var room = new WaitingArea(inputs);
            var prediction = new Predictions();

            prediction.ApplyPrediction2(room);

            return room.CountOccupiedPlaces().ToString();
        }
    }

    public class Predictions
    {
        public void ApplyPrediction1(WaitingArea area)
        {
            var count = 0;
            while (ApplyPrediction1To(area))
                count++;
        }

        public void ApplyPrediction2(WaitingArea area)
        {
            var count = 0;
            while (ApplyPrediction2To(area))
                count++;
        }

        private bool ApplyPrediction1To(WaitingArea area)
        {
            var snapshot = area.Clone();

            var changed = false;
            for (int rIndex = 0; rIndex < snapshot.RowCount; rIndex++)
                for (int cIndex = 0; cIndex < snapshot.ColCount; cIndex++)
                {
                    var staticChair = snapshot[cIndex, rIndex];
                    var chair = area[cIndex, rIndex];
                    if (!staticChair.IsSeat) continue;

                    var adjacentSeats = staticChair.FindAdjacentSeats();
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
                        if (CountOccupiedIn(adjacentSeats) >= 4)
                        {
                            chair.ChangeState();
                            changed = true;
                        }
                    }
                }

            return changed;
        }

        private bool ApplyPrediction2To(WaitingArea area)
        {
            var snapshot = area.Clone();

            var changed = false;
            for (int rIndex = 0; rIndex < snapshot.RowCount; rIndex++)
                for (int cIndex = 0; cIndex < snapshot.ColCount; cIndex++)
                {
                    var staticChair = snapshot[cIndex, rIndex];
                    var chair = area[cIndex, rIndex];
                    if (!staticChair.IsSeat) continue;

                    var adjacentSeats = staticChair.FindAdjacentOrVisibleSeats();
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
                        if (CountOccupiedIn(adjacentSeats) >= 5)
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
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public WaitingArea(string inputs)
        {
            places = new List<IPlace[]>();
            inputs.SplitByLine().ToList().ForEach(line => places.Add(line.ToCharArray().Select(place => Chair.Parse(place, this)).ToArray()));
            init();
        }
        private WaitingArea(List<IPlace[]> places)
        {
            this.places = places;
            init();
        }

        private void init()
        {
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
        IPlace[] FindAdjacentSeats();
        IPlace[] FindAdjacentOrVisibleSeats();
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

        public IPlace[] FindAdjacentSeats()
        {
            int startR = myRow == 0 ? 0 : myRow - 1;
            int startC = myCol == 0 ? 0 : myCol - 1;
            int endR = myRow == area.RowCount - 1 ? myRow : myRow + 1;
            int endC = myCol == area.ColCount - 1 ? myCol : myCol + 1;

            var result = new List<IPlace>();
            for (int rIndex = startR; rIndex <= endR; rIndex++)
                for (int cIndex = startC; cIndex <= endC; cIndex++)
                {
                    if (rIndex != myRow || cIndex != myCol)
                    {
                        var c = area[cIndex, rIndex];
                        if (c.IsSeat)
                            result.Add(area[cIndex, rIndex]);
                    }
                }

            return result.ToArray();
        }
        public IPlace[] FindAdjacentOrVisibleSeats()
        {
            var result = new List<IPlace>();

            AddFirstChairFound(result, -1, -1);
            AddFirstChairFound(result, -1, 0);
            AddFirstChairFound(result, -1, 1);
            AddFirstChairFound(result, 0, -1);
            AddFirstChairFound(result, 0, 1);
            AddFirstChairFound(result, 1, -1);
            AddFirstChairFound(result, 1, 0);
            AddFirstChairFound(result, 1, 1);

            return result.ToArray();
        }
        private void AddFirstChairFound(List<IPlace> found, int xVariant, int yVariant)
        {
            bool done = false;
            int x = myCol;
            int y = myRow;

            while (!done)
            {
                x = x + xVariant;
                y = y + yVariant;

                if (x < 0 || x >= area.ColCount) return;
                if (y < 0 || y >= area.RowCount) return;

                var place = area[x, y];
                if (place.IsSeat)
                {
                    found.Add(place);
                    done = true;
                }
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

            public IPlace[] FindAdjacentOrVisibleSeats()
            {
                throw new System.NotImplementedException();
            }

            public IPlace[] FindAdjacentSeats()
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

