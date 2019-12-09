using System.Threading.Tasks;

namespace AdventOfCode2019.Day9
{
    public class PreparedInput : IInput
    {
        private long[] _inputs;
        private int index = 0;

        public PreparedInput(params long[] inputs)
        {
            _inputs = inputs;
        }

        public Task<long> ReadInput()
        {
            var result = _inputs[index];

            index += 1;

            return Task.FromResult(result);
        }
    }
}
