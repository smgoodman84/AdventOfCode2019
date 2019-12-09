using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AdventOfCode2019.Day9
{
    public class IOPipe : IInput, IOutput
    {
        private readonly SemaphoreSlim _enumerationSemaphore = new SemaphoreSlim(0);
        private Queue<long> pipe = new Queue<long>();

        public void Output(long output)
        {
            pipe.Enqueue(output);
            _enumerationSemaphore.Release();
        }

        public async Task<long> ReadInput()
        {
            await _enumerationSemaphore.WaitAsync();
            return pipe.Dequeue();
        }
    }
}
