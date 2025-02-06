using R365.Challenge.Interfaces;
using R365.Challenge.Models;

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

        public CalculationResult Calculate(string input)
        {
            try
            {
                var calculation = _delimiterParser.GetDelimiters(input);
                calculation.OperandString = input.Substring(calculation.OperandStartIndex);

                var operands = calculation.OperandString.Split(calculation.Delimiters.ToArray(), StringSplitOptions.None);

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
