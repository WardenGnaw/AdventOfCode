using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Day10
{
    public class Main
    {
        [Fact]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt")).Split("\r\n");

            int syntax_error = 0;

            foreach (string part in data)
            {
                Stack<char> brace = new Stack<char>();
                char[] input = part.ToCharArray();
                foreach (char c in input)
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        brace.Push(c);
                    }
                    else
                    {
                        if (c == ')')
                        {
                            if (brace.Peek() == '(')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected ) but found " + brace.Peek());
                                syntax_error += 3;
                                break;
                            }
                        }
                        else if (c == ']')
                        {
                            if (brace.Peek() == '[')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected ] but found " + brace.Peek());
                                syntax_error += 57;
                                break;
                            }
                        }
                        else if (c == '}')
                        {
                            if (brace.Peek() == '{')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected } but found " + brace.Peek());
                                syntax_error += 1197;
                                break;
                            }
                        }
                        else if (c == '>')
                        {
                            if (brace.Peek() == '<')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected > but found " + brace.Peek());
                                syntax_error += 25137;
                                break;
                            }
                        }
                        else
                        {
                            throw new Exception("WHAT IS THIS? " + c);
                        }
                    }
                }
            }

            


            Assert.Equal(0, syntax_error);
        }

        [Fact]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt")).Split("\r\n");

            List<char[]> incomplete = new List<char[]>();

            List<ulong> auto_complete_scores = new();

            foreach (string part in data)
            {
                int syntax_error = 0;
                Stack<char> brace = new Stack<char>();
                char[] input = part.ToCharArray();
                foreach (char c in input)
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        brace.Push(c);
                    }
                    else
                    {
                        if (c == ')')
                        {
                            if (brace.Peek() == '(')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected ) but found " + brace.Peek());
                                syntax_error += 3;
                                break;
                            }
                        }
                        else if (c == ']')
                        {
                            if (brace.Peek() == '[')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected ] but found " + brace.Peek());
                                syntax_error += 57;
                                break;
                            }
                        }
                        else if (c == '}')
                        {
                            if (brace.Peek() == '{')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected } but found " + brace.Peek());
                                syntax_error += 1197;
                                break;
                            }
                        }
                        else if (c == '>')
                        {
                            if (brace.Peek() == '<')
                            {
                                brace.Pop();
                            }
                            else
                            {
                                Console.WriteLine("Expected > but found " + brace.Peek());
                                syntax_error += 25137;
                                break;
                            }
                        }
                        else
                        {
                            throw new Exception("WHAT IS THIS? " + c);
                        }
                    }
                }
                if (syntax_error != 0)
                {
                    continue;
                }

                ulong middle = 0;
                while (brace.Count != 0)
                {
                    char c = brace.Pop();
                    if (c == '(')
                    {
                        middle = middle * 5 + 1;
                    }
                    else if (c == '[')
                    {
                        middle = middle * 5 + 2;
                    }
                    else if (c == '{')
                    {
                        middle = middle * 5 + 3;
                    }
                    else if (c == '<')
                    {
                        middle = middle * 5 + 4;
                    }
                }

                auto_complete_scores.Add(middle);
            }

            Assert.Equal((ulong)0, auto_complete_scores.OrderByDescending(x => x).ElementAt(auto_complete_scores.Count / 2));
        }
    }
}