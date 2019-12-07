using System;

namespace AdventOfCode2019.Day7
{
    public class CombinedInput : IInput
    {
        private IInput[] _inputs;
        int index = 0;

        public CombinedInput(params IInput[] inputs)
        {
            _inputs = inputs;
        }

        public int ReadInput()
        {
            var result = _inputs[index].ReadInput();

            index += 1;

            return result;
        }
    }
}
