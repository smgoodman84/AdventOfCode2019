﻿using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AdventOfCode2019.Day7
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
