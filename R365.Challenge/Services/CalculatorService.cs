using R365.Challenge.Interfaces;
using R365.Challenge.Models;

namespace R365.Challenge.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IAdditionService _additionService;
        private readonly ISubtractionService _subtractionService;
        private readonly IMultiplicationService _multiplicationService;
        private readonly IDivisionService _divisionService;
        private readonly IInputParserService _inputParser;
        private readonly IDelimiterParserService _delimiterParser;

        public CalculatorService(IAdditionService additionService, ISubtractionService subtractionService,
            IMultiplicationService multiplicationService, IDivisionService divisionService,
            IInputParserService inputParser, IDelimiterParserService delimiterParser)
        {
            _additionService = additionService;
            _subtractionService = subtractionService;
            _multiplicationService = multiplicationService;
            _divisionService = divisionService;
            _inputParser = inputParser;
            _delimiterParser = delimiterParser;
        }

        public CalculationResult Calculate(CalculationRequest request)
        {
            try
            {
                var calculation = _delimiterParser.GetDelimiters(request.CalculationString);
                calculation.OperandString = request.CalculationString.Substring(calculation.OperandStartIndex);

                var operands = calculation.OperandString.Split(calculation.Delimiters.ToArray(), StringSplitOptions.None);

                var parsedInput = _inputParser.Parse(operands);
                switch (request.CalculationType)
                {
                    case CalculationTypes.Addition:
                        return _additionService.TryAdd(parsedInput);
                    case CalculationTypes.Subtraction:
                        return _subtractionService.TrySubtract(parsedInput);
                    case CalculationTypes.Multiplication:
                        return _multiplicationService.TryMultiply(parsedInput);
                    case CalculationTypes.Division:
                        return _divisionService.TryDivide(parsedInput);
                    default:
                        throw new Exception("An unexpected error has occured.");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
