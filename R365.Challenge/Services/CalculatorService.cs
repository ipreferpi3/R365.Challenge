using R365.Challenge.Interfaces;

namespace R365.Challenge.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IAdderService _adder;
        private readonly IInputParserService _inputParser;
        private readonly IDelimiterParserService _delimiterParser;

        public CalculatorService(IAdderService adder, IInputParserService inputParser, IDelimiterParserService delimiterParser)
        {
            _adder = adder;
            _inputParser = inputParser;
            _delimiterParser = delimiterParser;
        }

        public int Calculate(string input)
        {
            try
            {
                var calculation = _delimiterParser.GetDelimiters(input);
                calculation.Operands = input.Substring(calculation.OperandStartIndex);

                var operands = calculation.Operands.Split(calculation.Delimiters.ToArray(), StringSplitOptions.None);

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
