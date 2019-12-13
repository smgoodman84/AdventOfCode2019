﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day09.Intcode
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

        public bool HasInputToRead()
        {
            return pipe.Count > 0;
        }

        public async Task<long> ReadInput()
        {
            await _enumerationSemaphore.WaitAsync();
            return pipe.Dequeue();
        }
    }
}
