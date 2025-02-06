using R365.Challenge.Models;

namespace R365.Challenge.Interfaces
{
    public interface ISubtractionService
    {
        CalculationResult TrySubtract(List<int> input);
    }
}
