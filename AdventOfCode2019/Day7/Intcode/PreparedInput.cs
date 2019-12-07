using System.Threading.Tasks;

namespace AdventOfCode2019.Day7
{
    public class PreparedInput : IInput
    {
        private int[] _inputs;
        private int index = 0;

        public PreparedInput(params int[] inputs)
        {
            _inputs = inputs;
        }

        public Task<int> ReadInput()
        {
            var result = _inputs[index];

            index += 1;

            return Task.FromResult(result);
        }
    }
}
