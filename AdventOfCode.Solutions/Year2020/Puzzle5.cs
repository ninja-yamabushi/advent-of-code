using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle5_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var seats = Seat.Parse(inputs);
            return seats.Max(s => s.Id).ToString();
        }
    }

    public class Puzzle5_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var takenSeats = Seat.Parse(inputs);
            var plane = new Plane(takenSeats);
            var availableSeats = plane.GetAvailableSeats();

            foreach (var seat in availableSeats)
            {
                if (takenSeats.Contains(seat.Id - 1) && takenSeats.Contains(seat.Id + 1))
                    return seat.Id.ToString();
            }

            return "-1";
        }
    }

    public static class SeatExtensions
    {
        public static bool Contains(this Seat[] source, int id)
        {
            return source.Count(s => s.Id == id) > 0;
        }
    }

    public class Plane
    {
        private Seat[,] availability = new Seat[128, 8];

        public Plane(Seat[] takenSeats)
        {
            foreach (var seat in takenSeats)
                availability[seat.Row, seat.Column] = seat;
        }

        internal Seat[] GetAvailableSeats()
        {
            var result = new List<Seat>();

            for (int row = 1; row < 127; row++)
                for (int column = 0; column < 7; column++)
                    if (availability[row, column] == null)
                        result.Add(new Seat(row, column));

            return result.ToArray();
        }
    }

    public class Seat
    {

        public Seat(string position)
        {
            position = position.Replace("F", "0").Replace("B", "1").Replace("R", "1").Replace("L", "0");
            Row = Convert.ToInt32(position.Substring(0, 7), 2);
            Column = Convert.ToInt32(position.Substring(7), 2);
            Id = Row * 8 + Column;
        }

        public Seat(int row, int column)
        {
            Row = row;
            Column = column;
            Id = Row * 8 + Column;
        }

        public int Id { get; }
        public int Row { get; }
        public int Column { get; }


        public static Seat ParseSingle(string input)
        {
            return new Seat(input);
        }

        public static Seat[] Parse(string inputs)
        {
            string[] values = inputs.SplitByLine();
            return values.Select(s => ParseSingle(s)).ToArray();
        }
    }
}
