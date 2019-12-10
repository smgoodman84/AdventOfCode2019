using System;

namespace AdventOfCode2019.Day09.Intcode
{
    public class ConsoleOutput : IOutput
    {
        public void Output(long output)
        {
            Console.WriteLine($"Output: {output}");
        }
    }
}
