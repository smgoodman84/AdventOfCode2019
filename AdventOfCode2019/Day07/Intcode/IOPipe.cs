using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day07.Intcode
{
    public class IOPipe : IInput, IOutput
    {
        private readonly SemaphoreSlim _enumerationSemaphore = new SemaphoreSlim(0);
        private Queue<int> pipe = new Queue<int>();

        public void Output(int output)
        {
            pipe.Enqueue(output);
            _enumerationSemaphore.Release();
        }

        public async Task<int> ReadInput()
        {
            await _enumerationSemaphore.WaitAsync();
            return pipe.Dequeue();
        }
    }
}
