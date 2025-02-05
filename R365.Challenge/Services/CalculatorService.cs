using R365.Challenge.Interfaces;

namespace R365.Challenge.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IAdderService _adder;

        public CalculatorService(IAdderService adder)
        {
            _adder = adder;
        }

        public int Calculate(string input)
        {
            var operands = input.Split(new string[] { ",", "\\n" }, StringSplitOptions.None);
            try
            {
                return _adder.TryAdd(operands);
            }
            catch
            {
                throw;
            }
        }
    }
}
