using R365.Challenge.Models;

namespace R365.Challenge.Interfaces
{
    public interface IAdditionService
    {
        CalculationResult TryAdd(List<int> input);
    }
}
