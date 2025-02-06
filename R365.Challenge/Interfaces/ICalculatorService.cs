using R365.Challenge.Models;

namespace R365.Challenge.Interfaces
{
    public interface ICalculatorService
    {
        CalculationResult Calculate(CalculationRequest request);
    }
}
