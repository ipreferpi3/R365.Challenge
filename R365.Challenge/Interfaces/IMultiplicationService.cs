using R365.Challenge.Models;

namespace R365.Challenge.Interfaces
{
    public interface IMultiplicationService
    {
        CalculationResult TryMultiply(List<int> input);
    }
}
