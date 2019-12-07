using System.Collections.Generic;

namespace AdventOfCode2019.Day7
{
    public class IOPipe : IInput, IOutput
    {
        private Queue<int> pipe = new Queue<int>();
        public void Output(int output)
        {
            pipe.Enqueue(output);
        }

        public int ReadInput()
        {
            return pipe.Dequeue();
        }
    }
}
