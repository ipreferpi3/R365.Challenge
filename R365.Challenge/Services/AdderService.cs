using R365.Challenge.Interfaces;
using R365.Challenge.Models;

namespace R365.Challenge.Services
{
    public class AdderService : IAdderService
    {
        public CalculationResult TryAdd(List<int> input)
        {
            var result = new CalculationResult();
            var total = 0;

            foreach (var op in input)
            {
                total += op;
            }

            result.Total = total;
            result.Formula = string.Format("{0} = {1}", string.Join('+', input), total);

            return result;
        }
    }
}
