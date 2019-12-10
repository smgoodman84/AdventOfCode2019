using System.Threading.Tasks;

namespace AdventOfCode2019.Day07.Intcode
{
    public class CombinedInput : IInput
    {
        private IInput[] _inputs;
        int index = 0;

        public CombinedInput(params IInput[] inputs)
        {
            _inputs = inputs;
        }

        public Task<int> ReadInput()
        {
            var result = _inputs[index].ReadInput();

            index += 1;

            return result;
        }
    }
}
