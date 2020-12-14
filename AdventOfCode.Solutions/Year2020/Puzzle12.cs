using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle12_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            INavigationBehavior behavior = new NavigatioNormalBehavior();
            var actions = NavigationAction.Parse(inputs);
            var ship = new Ship();
            foreach (var action in actions)
                action.ExecuteBehavior(behavior, ship);

            return ship.GetManhattanDistance().ToString();
        }
    }

    public class Puzzle12_2 : IPuzzle
    {
        public string Solve(string inputs)
        {

            INavigationBehavior behavior = new NavigationWayPointBehavior();
            var actions = NavigationAction.Parse(inputs);
            var ship = new Ship(new MapPoint(10, -1));
            foreach (var action in actions)
                action.ExecuteBehavior(behavior, ship);

            return ship.GetManhattanDistance().ToString();
        }
    }


    public class Direction
    {
        public static Direction Invalid = new Direction("INVALID");
        public static Direction East;
        public static Direction South;
        public static Direction West;
        public static Direction North;
        public static Direction[] All;

        private readonly string directionSymbol;
        public Direction Right { get; private set; }
        public Direction Left { get; private set; }


        static Direction()
        {
            East = new Direction("E");
            South = new Direction("S");
            West = new Direction("W");
            North = new Direction("N");

            East.AssignRightAndLeft(South, North);
            South.AssignRightAndLeft(West, East);
            West.AssignRightAndLeft(North, South);
            North.AssignRightAndLeft(East, West);

            All = new[] { East, South, West, North };
        }
        public Direction(string symbol)
        {
            directionSymbol = symbol;
        }

        private void AssignRightAndLeft(Direction right, Direction left)
        {
            Right = right;
            Left = left;
        }

        private Direction GetRight(int quantity)
        {
            if (quantity == 0) return this;
            return Right.GetRight(--quantity);
        }

        public Direction RotateRightBy(int degree)
        {
            return GetRight(degree / 90);
        }
        public Direction RotateLeftBy(int degree)
        {
            return GetLeft(degree / 90);
        }

        private Direction GetLeft(int quantity)
        {
            if (quantity == 0) return this;
            return Left.GetLeft(--quantity);
        }

        public bool Is(Direction direction)
        {
            return direction.directionSymbol == directionSymbol;
        }

        public bool IsValid => !Is(Invalid);

        public override string ToString()
        {
            return directionSymbol;
        }

        public static Direction Parse(string input)
        {
            foreach (var direction in All)
                if (direction.directionSymbol == input)
                    return direction;
            return Invalid;
        }

        public static Direction[] From(MapPoint waypoint)
        {
            var result = new List<Direction>();

            if (waypoint.X > 0) result.Add(East);
            if (waypoint.X < 0) result.Add(West);

            if (waypoint.Y > 0) result.Add(South);
            if (waypoint.Y < 0) result.Add(North);

            return result.ToArray();
        }

    }

    public class MapPoint
    {
        public int X = 0;
        public int Y = 0;
        public MapPoint(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    public class Ship
    {
        public Direction CurrentDirection;
        public MapPoint Position { get; }
        public MapPoint Waypoint { get; }

        public Ship() : this(new MapPoint())
        { }
        public Ship(MapPoint waypoint)
        {
            CurrentDirection = Direction.East;
            Position = new MapPoint();
            Waypoint = waypoint;
        }

        public int GetManhattanDistance()
        {
            return Position.X.AbsoluteValue() + Position.Y.AbsoluteValue();
        }
        public override string ToString()
        {
            return $"{CurrentDirection}, Position{Position}, Waypoint{Waypoint}";
        }
    }

    public class NavigationAction
    {
        public string Action { get; }
        public int Value { get; }

        public NavigationAction(string action, int value)
        {
            Action = action;
            Value = value;
        }

        public void ExecuteBehavior(INavigationBehavior behavior, Ship ship)
        {
            behavior.Navigate(Action, Value, ship);
        }

        public static NavigationAction[] Parse(string inputs)
        {
            return inputs.SplitByLine().Select(line => ParseSingle(line)).ToArray();
        }
        public static NavigationAction ParseSingle(string input)
        {
            return new NavigationAction(input[0].ToString(), int.Parse(input.Substring(1)));
        }

        public override string ToString()
        {
            return $"{Action} : {Value}";
        }
    }

    public interface INavigationBehavior
    {
        void Navigate(string Action, int Value, Ship ship);
    }
    public class NavigatioNormalBehavior : INavigationBehavior
    {
        public void Navigate(string Action, int Value, Ship ship)
        {
            switch (Action)
            {
                case "N":
                    MoveShiptTo(ship, Direction.North, Value);
                    break;
                case "E":
                    MoveShiptTo(ship, Direction.East, Value);
                    break;
                case "S":
                    MoveShiptTo(ship, Direction.South, Value);
                    break;
                case "W":
                    MoveShiptTo(ship, Direction.West, Value);
                    break;
                case "L":
                    ship.CurrentDirection = ship.CurrentDirection.RotateLeftBy(Value);
                    break;
                case "R":
                    ship.CurrentDirection = ship.CurrentDirection.RotateRightBy(Value);
                    break;
                case "F":
                    MoveShiptTo(ship, ship.CurrentDirection, Value);
                    break;
            }
        }
        private void MoveShiptTo(Ship ship, Direction direction, int value)
        {
            if (direction.Is(Direction.North))
                ship.Position.Y -= value;
            if (direction.Is(Direction.South))
                ship.Position.Y += value;

            if (direction.Is(Direction.East))
                ship.Position.X += value;
            if (direction.Is(Direction.West))
                ship.Position.X -= value;
        }

    }
    public class NavigationWayPointBehavior : INavigationBehavior
    {
        public void Navigate(string Action, int Value, Ship ship)
        {
            switch (Action)
            {
                case "N":
                    MoveWaypointTo(ship, Direction.North, Value);
                    break;
                case "E":
                    MoveWaypointTo(ship, Direction.East, Value);
                    break;
                case "S":
                    MoveWaypointTo(ship, Direction.South, Value);
                    break;
                case "W":
                    MoveWaypointTo(ship, Direction.West, Value);
                    break;
                case "L":
                    GoLeft(ship.Waypoint, Value);
                    break;
                case "R":
                    GoRight(ship.Waypoint, Value);
                    break;
                case "F":
                    MoveShipToWaypoint(ship, Value);
                    break;
            }
        }
        private void GoLeft(MapPoint point, int degree)
        {
            int times = degree / 90;
            for (int time = 0; time < times; time++)
            {
                var oldX = point.X;
                var oldY = point.Y;

                point.X = oldY;
                point.Y = oldX * -1;
            }
        }
        private void GoRight(MapPoint point, int degree)
        {
            int times = degree / 90;
            for (int time = 0; time < times; time++)
            {
                var oldX = point.X;
                var oldY = point.Y;

                point.X = oldY * -1;
                point.Y = oldX;
            }
        }
        private void MoveShipToWaypoint(Ship ship, int value)
        {
            var angle = Direction.From(ship.Waypoint);
            int xMove = ship.Waypoint.X.AbsoluteValue() * value;
            int yMove = ship.Waypoint.Y.AbsoluteValue() * value;

            foreach (var cardinalPoint in angle)
            {
                if (cardinalPoint.Is(Direction.North))
                    ship.Position.Y -= yMove;
                if (cardinalPoint.Is(Direction.South))
                    ship.Position.Y += yMove;

                if (cardinalPoint.Is(Direction.East))
                    ship.Position.X += xMove;
                if (cardinalPoint.Is(Direction.West))
                    ship.Position.X -= xMove;
            }
        }
        private void MoveWaypointTo(Ship ship, Direction direction, int value)
        {
            if (direction.Is(Direction.North))
                ship.Waypoint.Y -= value;
            if (direction.Is(Direction.South))
                ship.Waypoint.Y += value;

            if (direction.Is(Direction.East))
                ship.Waypoint.X += value;
            if (direction.Is(Direction.West))
                ship.Waypoint.X -= value;
        }
    }
}

