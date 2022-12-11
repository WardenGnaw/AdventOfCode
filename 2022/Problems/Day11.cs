using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Problems
{
    public class Test
    {
        private ulong divisable;
        private int @true;
        private int @false;

        public Test(ulong divisable, int @true, int @false)
        {
            this.divisable = divisable;
            this.@true = @true;
            this.@false = @false;
        }

        public int Run(ulong value)
        {
            if (value % divisable == 0)
            {
                return @true;
            }
            else
            {
                return @false;
            }

            return -1;
        }
    }

    public class Expression
    {
        private enum Operator
        {
            None,
            Add,
            Multiply,
            Subtract,
            Divide
        }

        public string expression;
        private Operator op;
        private ulong? lhs;
        private ulong? rhs;

        public ulong Count { get; private set; }

        public Expression(string expression)
        {
            this.expression = expression;
            this.Count = 0;
        }

        public void Parse()
        {
            string[] expressionArr = expression.Split('=');
            string lhs = expressionArr[0].Trim();
            string rhs = expressionArr[1].Trim();

            Regex regex = new Regex(@"(\w+|\d+) ([+\-\/\*]) (\w+|\d+)");
            Match match = regex.Match(rhs);
            if (match.Success)
            {
                if (ulong.TryParse(match.Groups[1].Value, out ulong value))
                {
                    this.lhs = value;
                }
                switch (match.Groups[2].Value.Trim())
                {
                    case "+":
                        op = Operator.Add; break;
                    case "-":
                        op = Operator.Subtract; break;
                    case "*":
                        op = Operator.Multiply; break;
                    case "/":
                        op = Operator.Divide; break;
                }
                if (ulong.TryParse(match.Groups[3].Value, out ulong value2))
                {
                    this.rhs = value2;
                }
            }
        }

        public ulong Run(ulong old)
        {
            this.Count++;
            ulong result = 0;
            ulong lhs = this.lhs == null ? old : this.lhs.Value;
            ulong rhs = this.rhs == null ? old : this.rhs.Value;
            switch (this.op)
            {
                case Operator.Add:
                    result = lhs + rhs;
                    break;
                case Operator.Subtract:
                    result = lhs - rhs;
                    break;
                case Operator.Multiply:
                    result = lhs * rhs;
                    break;
                case Operator.Divide:
                    result = lhs / rhs;
                    break;
            }

            return result;
        }
    }

    public class Item
    {
        public ulong WorryLevel { get; set; }
    }

    public class Monkey
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public Expression Operation { get; set; }
        public Test Test { get; set; }
    }

    [TestClass]
    public class Day11
    {
        [TestMethod]
        public void Part1()
        {
            List<Monkey> monkeys = new List<Monkey>();
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day11.txt")).Trim().Split("\r\n");
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].StartsWith("Monkey"))
                {
                    Monkey m = new Monkey();
                    i++; // Move to starting line
                    string starting = data[i].Trim();
                    Assert.IsTrue(starting.StartsWith("Starting items: "));
                    starting = starting.Substring("Starting items: ".Length);
                    foreach (string s in starting.Split(", "))
                    {
                        m.Items.Add(new Item() { WorryLevel = ulong.Parse(s) });
                    }
                    i++; // Move to operation line
                    string operation = data[i].Trim();
                    Assert.IsTrue(operation.StartsWith("Operation: "));
                    operation = operation.Substring("Operation: ".Length);
                    m.Operation = new Expression(operation);
                    m.Operation.Parse();

                    i++;
                    string test = data[i].Trim();
                    Assert.IsTrue(test.StartsWith("Test: divisible by "));
                    test = test.Substring("Test: divisible by ".Length);
                    ulong divisable = ulong.Parse(test);

                    i++;
                    string @true = data[i].Trim();
                    Assert.IsTrue(@true.StartsWith("If true: throw to monkey "));
                    @true = @true.Substring("If true: throw to monkey ".Length);
                    int trueIdx = int.Parse(@true);

                    i++;
                    string @false = data[i].Trim();
                    Assert.IsTrue(@false.StartsWith("If false: throw to monkey "));
                    @false = @false.Substring("If false: throw to monkey ".Length);
                    int falseIdx = int.Parse(@false);
                    m.Test = new Test(divisable, trueIdx, falseIdx);

                    monkeys.Add(m);
                }
            }

            for (int round = 0; round < 20; round++)
            {
                foreach (Monkey m in monkeys)
                {
                    List<Item> items = m.Items;
                    m.Items = new List<Item>();
                    foreach (Item i in items)
                    {
                        i.WorryLevel = m.Operation.Run(i.WorryLevel) / 3;
                        int idx = m.Test.Run(i.WorryLevel);
                        monkeys[idx].Items.Add(i);
                    }
                }
            }

            Assert.AreEqual((ulong)54036, monkeys.OrderByDescending(m => m.Operation.Count).Take(2).Aggregate((ulong)1, (v, m) => v * m.Operation.Count));
        }

        [TestMethod]
        public void Part2()
        {
            ulong maxNumber = 1;
            List<Monkey> monkeys = new List<Monkey>();
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day11.txt")).Trim().Split("\r\n");
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].StartsWith("Monkey"))
                {
                    Monkey m = new Monkey();
                    i++; // Move to starting line
                    string starting = data[i].Trim();
                    Assert.IsTrue(starting.StartsWith("Starting items: "));
                    starting = starting.Substring("Starting items: ".Length);
                    foreach (string s in starting.Split(", "))
                    {
                        m.Items.Add(new Item() { WorryLevel = ulong.Parse(s) });
                    }
                    i++; // Move to operation line
                    string operation = data[i].Trim();
                    Assert.IsTrue(operation.StartsWith("Operation: "));
                    operation = operation.Substring("Operation: ".Length);
                    m.Operation = new Expression(operation);
                    m.Operation.Parse();

                    i++;
                    string test = data[i].Trim();
                    Assert.IsTrue(test.StartsWith("Test: divisible by "));
                    test = test.Substring("Test: divisible by ".Length);
                    ulong divisable = ulong.Parse(test);
                    maxNumber *= divisable;

                    i++;
                    string @true = data[i].Trim();
                    Assert.IsTrue(@true.StartsWith("If true: throw to monkey "));
                    @true = @true.Substring("If true: throw to monkey ".Length);
                    int trueIdx = int.Parse(@true);

                    i++;
                    string @false = data[i].Trim();
                    Assert.IsTrue(@false.StartsWith("If false: throw to monkey "));
                    @false = @false.Substring("If false: throw to monkey ".Length);
                    int falseIdx = int.Parse(@false);
                    m.Test = new Test(divisable, trueIdx, falseIdx);

                    monkeys.Add(m);
                }
            }

            for (int round = 0; round < 10000; round++)
            {
                foreach (Monkey m in monkeys)
                {
                    List<Item> items = m.Items;
                    m.Items = new List<Item>();
                    foreach (Item i in items)
                    {
                        i.WorryLevel = m.Operation.Run(i.WorryLevel);
                        i.WorryLevel = i.WorryLevel % maxNumber;
                        int idx = m.Test.Run(i.WorryLevel);
                        monkeys[idx].Items.Add(i);
                    }
                }
            }

            Assert.AreEqual(13237873355, monkeys.OrderByDescending(m => m.Operation.Count).Take(2).Aggregate((ulong)1, (v, m) => v * m.Operation.Count));
        }
    }
}