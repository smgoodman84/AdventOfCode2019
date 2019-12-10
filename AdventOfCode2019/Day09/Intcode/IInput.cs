using System.Threading.Tasks;

namespace AdventOfCode2019.Day09.Intcode
{
    public interface IInput
    {
        Task<long> ReadInput();
    }
}
