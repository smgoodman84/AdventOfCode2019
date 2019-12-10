using System;

namespace AdventOfCode2019.Day07.Intcode
{
    public class ConsoleOutput : IOutput
    {
        public void Output(int output)
        {
            Console.WriteLine($"Output: {output}");
        }
    }
}
