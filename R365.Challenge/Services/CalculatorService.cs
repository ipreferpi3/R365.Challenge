using R365.Challenge.Interfaces;

namespace R365.Challenge.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IAdderService _adder;
        private readonly IInputParserService _inputParser;

        public CalculatorService(IAdderService adder, IInputParserService inputParser)
        {
            _adder = adder;
            _inputParser = inputParser;
        }

        public int Calculate(string input)
        {
            var operands = input.Split(new string[] { ",", "\\n" }, StringSplitOptions.None);
            try
            {
                var parsedInput = _inputParser.Parse(operands);
                return _adder.TryAdd(parsedInput);
            }
            catch
            {
                throw;
            }
        }
    }
}
