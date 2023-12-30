using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Problems
{
    [TestClass]
    public class Day13
    {
        public interface IValue
        {
            // -1 not right order
            // 0 equal
            // 1 right order
            public abstract int IsRightOrder(IValue o);
        }

        public class IntValue : IValue
        {
            public int Value { get; set; }

            public int IsRightOrder(IValue o)
            {
                if (o is IntValue iv)
                {
                    if (this.Value > iv.Value)
                    {
                        return -1;
                    }
                    else if (this.Value < iv.Value)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (o is ListValue lv)
                {
                    return new ListValue() { Value = new List<IValue> { this } }.IsRightOrder(lv);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public class StartListValue : IValue
        {
            public int IsRightOrder(IValue o)
            {
                return 0; // Unused
            }
        }

        public class ListValue : IValue
        {
            public List<IValue> Value { get; set; } = new List<IValue>();
            public int IsRightOrder(IValue o)
            {
                if (o is IntValue iv)
                {
                    return this.IsRightOrder(new ListValue() { Value = new List<IValue> { iv } });
                }
                else if (o is ListValue lv)
                {
                    List<IValue> lhs = this.Value;
                    List<IValue> rhs = lv.Value;
                    int i = 0;
                    for (i = 0; i < lhs.Count && i < rhs.Count; i++) 
                    {
                        int order = lhs[i].IsRightOrder(rhs[i]);
                        if (order != 0)
                        {
                            return order;
                        }
                    }
                    // Extras on left hand side.
                    if (i < lhs.Count)
                    {
                        return -1;
                    }
                    // Extras on right hand side.
                    else if (i < rhs.Count)
                    {
                        return 1;
                    }
                }

                return 0;
            }

        }

        public class Packet
        {
            public IValue Value { get; set; }

            public static Packet Parse(string data)
            {
                Packet p = new Packet();
                char[] chars = data.Trim().ToCharArray();
                Stack<IValue> stack = new Stack<IValue>();
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] == '[')
                    {
                        stack.Push(new StartListValue());
                    }
                    else if (chars[i] == ']')
                    {
                        Stack<IValue> values = new Stack<IValue>();
                        while (stack.Any() && stack.Peek() is not StartListValue)
                        {
                            values.Push(stack.Pop());
                        }
                        Debug.Assert(stack.Pop() is StartListValue);
                        ListValue lv = new ListValue() { Value = values.ToList() };
                        stack.Push(lv);
                    }
                    else if (char.IsDigit(chars[i]))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(chars[i]);
                        int j = i + 1;
                        while (j < chars.Length && char.IsDigit(chars[j]))
                        {
                            sb.Append(chars[j]);
                            j++;
                        }
                        stack.Push(new IntValue() {  Value = int.Parse(sb.ToString()) });
                        i = j - 1;
                    }
                    else if (chars[i] == ',') 
                    {
                        continue;
                    }
                    else
                    {
                        Debug.Assert(false, "Wtf is " + chars[i]);
                    }
                }

                p.Value = stack.Pop();
                return p;
            }

            public int IsRightOrder(Packet other)
            {
                return this.Value.IsRightOrder(other.Value);
            }
        }

        [TestMethod]
        public void Part1()
        {
            int index = 1;
            int sum = 0;
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day13.txt")).Split("\r\n");
            for (int i = 0; i < data.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(data[i]))
                {
                    Packet left = Packet.Parse(data[i]);
                    i++;
                    Packet right = Packet.Parse(data[i]);
                    if (left.IsRightOrder(right) == 1)
                    {
                        sum += index;
                    }
                    index++;
                }
            }


            Assert.AreEqual(5555, sum);
        }

        [TestMethod]
        public void Part2()
        {
            int index = 1;
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day13.txt")).Split("\r\n");
            List<Packet> packets = new List<Packet>();
            for (int i = 0; i < data.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(data[i]))
                {
                    packets.Add(Packet.Parse(data[i]));
                }
            }

            var p2 = new Packet() { Value = new ListValue() { Value = new List<IValue>() { new ListValue() { Value = new List<IValue>() { new IntValue { Value = 2 } } } } } };
            var p6 = new Packet() { Value = new ListValue() { Value = new List<IValue>() { new ListValue() { Value = new List<IValue>() { new IntValue { Value = 6 } } } } } };
            packets.Add(p2);
            packets.Add(p6);
            packets.Sort((x, y) =>
            {
                return y.IsRightOrder(x);
            });

            int index2 = packets.IndexOf(p2) + 1;
            int index6 = packets.IndexOf(p6) + 1;

            Assert.AreEqual(0, index2 * index6);
        }
    }
}