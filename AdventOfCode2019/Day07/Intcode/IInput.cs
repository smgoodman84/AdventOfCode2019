using System.Threading.Tasks;

namespace AdventOfCode2019.Day07.Intcode
{
    public interface IInput
    {
        Task<int> ReadInput();
    }
}
