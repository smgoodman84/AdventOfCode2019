using System;

namespace AdventOfCode2019.Day7
{
    public class ConsoleOutput : IOutput
    {
        public void Output(int output)
        {
            Console.WriteLine($"Output: {output}");
        }
    }
}
