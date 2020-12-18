using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.AdventOfCode.Solutions.Year2020.Day15;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle15_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var startingNumbers = inputs.SplitOn(",").Select(n => int.Parse(n)).ToList();

            var game = new Game(startingNumbers);
            return game.PlayUntil(2020).ToString();
        }
    }
    public class Puzzle15_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var startingNumbers = inputs.SplitOn(",").Select(n => int.Parse(n)).ToList();

            var game = new Game(startingNumbers);
            return game.PlayUntil(30000000).ToString();
        }
    }

    namespace AdventOfCode.Solutions.Year2020.Day15
    {
        public class Game
        {
            private Dictionary<int, NumberQuantityAndTurn> turns = new Dictionary<int, NumberQuantityAndTurn>();
            private int turnCounter;
            private int lastNumberSpoken;

            public Game(List<int> startingNumbers)
            {
                for (int index = 0; index < startingNumbers.Count; index++)
                {
                    turnCounter++;
                    var spokenNumber = startingNumbers[index];
                    turns.Add(spokenNumber, new NumberQuantityAndTurn(1, turnCounter));
                    lastNumberSpoken = spokenNumber;
                }
            }

            internal int PlayUntil(int stopAfterTurn)
            {
                while (turnCounter < stopAfterTurn)
                    speakNextNumber();
                return lastNumberSpoken;
            }

            private void speakNextNumber()
            {
                var nextSpokenNumber = 0;
                if (turns.ContainsKey(lastNumberSpoken) && turns[lastNumberSpoken].TimesSpoken > 1)
                    nextSpokenNumber = turnCounter - turns[lastNumberSpoken].PreviousTurn;
                
                turnCounter++;
                addNextSpokenNumber(nextSpokenNumber);
                lastNumberSpoken = nextSpokenNumber;
            }

            private void addNextSpokenNumber(int nextSpokenNumber)
            {
                if (!turns.ContainsKey(nextSpokenNumber))
                    turns.Add(nextSpokenNumber, new NumberQuantityAndTurn(1, turnCounter));
                else
                {
                    turns[nextSpokenNumber].AssignLastTurnSpoken(turnCounter);
                    turns[nextSpokenNumber].TimesSpoken++;
                }
            }

            private class NumberQuantityAndTurn
            {
                private Queue<int> _turns = new Queue<int>();
                public int TimesSpoken;

                public NumberQuantityAndTurn(int timesSpoken, int lastTurnSpoken)
                {
                    TimesSpoken = timesSpoken;
                    _turns.Enqueue(lastTurnSpoken);
                }

                public void AssignLastTurnSpoken(int turn)
                {
                    if (_turns.Count == 2) _turns.Dequeue();
                    _turns.Enqueue(turn);
                }

                public int PreviousTurn
                {
                    get { return _turns.Peek(); }
                }

                public override string ToString()
                {
                    return $"Times: {TimesSpoken}, Last: {string.Join(",", _turns.Select(x => x.ToString()).ToArray())}";
                }
            }
        }
    }
}

