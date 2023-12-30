using System.Text;

namespace Problems
{
    [TestClass]
    public class Day10
    {
        public abstract class Instruction
        {
            public int numCycles = 0;

            public virtual  void Run(Register r)
            {
                numCycles++;
            }

            public abstract bool IsDone { get; }
        }

        public class AddX : Instruction
        {
            private int add;

            public AddX(int add)
            {
                this.add = add;
                numCycles = 0;
            }

            public override void Run(Register r)
            {
                base.Run(r);
                if (IsDone)
                {
                    r.Add(add);
                }
            }

            public override bool IsDone
            {
                get
                {
                    return numCycles == 2;
                }
            }
        }

        public class Noop : Instruction
        {
            public override void Run(Register r)
            {
                base.Run(r);
            }

            public override bool IsDone
            {
                get
                {
                    return numCycles == 1;
                }
            }
        }
        
        public class Register
        {
            public int value;
            public Register(int value)
            {
                this.value = value;
            }

            public void Add(int add)
            {
                this.value += add;
            }
        }

        public class Pipeline
        {
            private Register register;
            private Queue<Instruction> instructions;
            public int currentCycles;
            public int Strength = 0;
            private Instruction currentInstruction;
            StringBuilder output;
            private int currentPixel;

            public Pipeline(Queue<Instruction> instructions)
            {
                currentPixel = 0;
                currentCycles = 0;
                register = new Register(1);
                this.instructions = instructions;
                output = new StringBuilder();
            }

            public bool Cycle()
            {
                if (instructions.Any())
                {
                    currentCycles++;
                    if (currentCycles == 20 ||
                        currentCycles == 60 ||
                        currentCycles == 100 ||
                        currentCycles == 140 ||
                        currentCycles == 180 ||
                        currentCycles == 220)
                    {
                        Strength += currentCycles * register.value;
                    }
                    if (IsPixelLit())
                    {
                        output.Append('#');
                    }
                    else
                    {
                        output.Append('.');
                    }
                    if (currentCycles % 40 == 0)
                    {
                        output.AppendLine();
                    }
                    currentPixel = (currentPixel + 1) % 40;
                    if (currentInstruction == null)
                    {
                        currentInstruction = instructions.Dequeue();
                    }
                    currentInstruction.Run(register);
                    if (currentInstruction.IsDone)
                    {
                        currentInstruction = null;
                    }
                }

                return instructions.Any();
            }

            public bool IsPixelLit()
            {
                return register.value - 1 == currentPixel || register.value == currentPixel || register.value + 1 == currentPixel;
            }

            public string GetOutput()
            {
                return output.ToString();
            }
        }

        [TestMethod]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day10.txt")).Trim().Split("\r\n");
            Queue<Instruction> instructions = new Queue<Instruction>();
            foreach (string s in data)
            {
                if (s.StartsWith("noop"))
                {
                    instructions.Enqueue(new Noop());
                }
                else if (s.StartsWith("addx"))
                {
                    int add = int.Parse(s.Split(" ")[1]);
                    instructions.Enqueue(new AddX(add));
                }
            }
            Pipeline p = new Pipeline(instructions);
            while (p.Cycle()) ;
            Assert.AreEqual(11780, p.Strength);
        }

        [TestMethod]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day10.txt")).Trim().Split("\r\n");
            Queue<Instruction> instructions = new Queue<Instruction>();
            foreach (string s in data)
            {
                if (s.StartsWith("noop"))
                {
                    instructions.Enqueue(new Noop());
                }
                else if (s.StartsWith("addx"))
                {
                    int add = int.Parse(s.Split(" ")[1]);
                    instructions.Enqueue(new AddX(add));
                }
            }
            Pipeline p = new Pipeline(instructions);
            while (p.Cycle())
            {
            }
            Console.Write(p.GetOutput());

            Assert.AreEqual(@"##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....
", p.GetOutput());
        }
    }
}