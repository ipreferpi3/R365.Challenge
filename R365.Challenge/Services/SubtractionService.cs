using R365.Challenge.Interfaces;
using R365.Challenge.Models;

namespace R365.Challenge.Services
{
    public class SubtractionService : ISubtractionService
    {
        public CalculationResult TrySubtract(List<int> input)
        {
            var result = new CalculationResult();
            var total = input.First();

            for (int i = 1; i < input.Count; i++)
            {
                total -= input[i];
            }

            result.Total = total;
            result.Formula = string.Format("{0} = {1}", string.Join('-', input), total);

            return result;
        }
    }
}
