using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.Day18;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle18_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            foreach (var op in Operator.Operators)
                op.ChangePrecedence(2);

            var mathExpressions = inputs.SplitByLine();
            long sum = 0;

            var algo = new ShuntingYardAlgorithm();
            foreach (var expression in mathExpressions)
                sum += algo.Execute(expression);

            return sum.ToString();
        }
    }

    public class Puzzle18_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            Operator.Add.ChangePrecedence(3);
            Operator.Subtract.ChangePrecedence(3);
            Operator.Multiply.ChangePrecedence(2);
            Operator.Divide.ChangePrecedence(2);

            var mathExpressions = inputs.SplitByLine();
            long sum = 0;

            var algo = new ShuntingYardAlgorithm();
            foreach (var expression in mathExpressions)
                sum += algo.Execute(expression);

            return sum.ToString();
        }
    }
}
namespace AdventOfCode.Solutions.Year2020.Day18
{
    public enum OperatorAssociativity { Left, Right }
    public class Operator
    {
        public static Operator Invalid = new Operator();
        public static Operator Add = new Operator("+", 2, OperatorAssociativity.Left, (left, right) => left + right);
        public static Operator Subtract = new Operator("-", 2, OperatorAssociativity.Left, (left, right) => left - right);
        public static Operator Multiply = new Operator("*", 3, OperatorAssociativity.Left, (left, right) => left * right);
        public static Operator Divide = new Operator("/", 3, OperatorAssociativity.Left, (left, right) => left / right);

        public static Operator OpeningParenthese = new Operator("(");
        public static Operator ClosingParenthese = new Operator(")");

        public static Operator[] All = { Add, Subtract, Multiply, Divide, OpeningParenthese, ClosingParenthese };
        public static Operator[] Operators = { Add, Subtract, Multiply, Divide };
        public static Operator[] Parentheses = { OpeningParenthese, ClosingParenthese };


        private Operator()
        { }
        private Operator(string operation)
        {
            Op = operation;
            Precedence = int.MaxValue;
        }

        private Operator(string operation, int precedence, OperatorAssociativity associativity, Func<long, long, long> action)
        {
            Op = operation;
            Precedence = precedence;
            Associativity = associativity;
            this.action = action;
        }

        private Func<long, long, long> action;
        public string Op { get; }
        public int Precedence { get; private set; }
        public OperatorAssociativity Associativity { get; }
        public long Exectute(long left, long right) => action.Invoke(left, right);


        public bool Is(Operator op) => Op == op.Op;
        public bool IsValid => !Is(Invalid);
        public bool IsOperator => Operators.Where(x => x.Op == Op).Count() > 0;
        public bool IsParenthese => Parentheses.Where(x => x.Op == Op).Count() > 0;

        public bool HasPrecedenceOn(Operator op)
        {
            return Precedence >= op.Precedence;
        }
        public void ChangePrecedence(int value)
        { 
            Precedence = value;
        }

        public override string ToString()
        {
            return Op;
        }

        public static Operator Parse(string value)
        {
            foreach (var op in All)
                if (op.Op == value)
                    return op;
            return Invalid;
        }
    }
    public class ShuntingYardAlgorithm
    {
        public long Execute(string mathExpression)
        {
            foreach (var op in Operator.All)
                mathExpression = mathExpression.Replace(op.Op, $" {op.Op} ");

            var values = new Stack<long>();
            var ops = new Stack<Operator>();

            var tokens = mathExpression.SplitOn(" ");
            foreach (var token in tokens)
            {
                var op = Operator.Parse(token);
                if (token.IsNumeric())
                    values.Push(long.Parse(token));
                else if (op.IsParenthese)
                {
                    if (op.Is(Operator.OpeningParenthese))
                        ops.Push(op);
                    else
                    {
                        while (!ops.Peek().Is(Operator.OpeningParenthese))
                            values.Push(ops.Pop().Exectute(values.Pop(), values.Pop()));
                        ops.Pop();
                    }
                }
                else if (op.IsOperator)
                {
                    while (ops.Count > 0 && !ops.Peek().IsParenthese && ops.Peek().HasPrecedenceOn(op))
                        values.Push(ops.Pop().Exectute(values.Pop(), values.Pop()));
                    ops.Push(op);
                }
            }
            while (ops.Count > 0)
                values.Push(ops.Pop().Exectute(values.Pop(), values.Pop()));

            return values.Pop();
        }
    }
}

