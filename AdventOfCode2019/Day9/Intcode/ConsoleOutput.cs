using System;

namespace AdventOfCode2019.Day9
{
    public class ConsoleOutput : IOutput
    {
        public void Output(long output)
        {
            Console.WriteLine($"Output: {output}");
        }
    }
}
