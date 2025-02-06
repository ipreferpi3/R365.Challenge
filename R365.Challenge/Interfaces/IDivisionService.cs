using R365.Challenge.Models;

namespace R365.Challenge.Interfaces
{
    public interface IDivisionService
    {
        CalculationResult TryDivide(List<int> input);
    }
}
