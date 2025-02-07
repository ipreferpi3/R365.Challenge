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
        private readonly IInputParserService _inputParserService;
        private readonly IDelimiterParserService _delimiterParserService;

        public CalculatorService(IAdditionService additionService, ISubtractionService subtractionService,
            IMultiplicationService multiplicationService, IDivisionService divisionService,
            IInputParserService inputParserService, IDelimiterParserService delimiterParserService)
        {
            _additionService = additionService;
            _subtractionService = subtractionService;
            _multiplicationService = multiplicationService;
            _divisionService = divisionService;
            _inputParserService = inputParserService;
            _delimiterParserService = delimiterParserService;
        }

        public CalculationResult Calculate(CalculationRequest request)
        {
            try
            {
                var calculation = _delimiterParserService.GetDelimiters(request.CalculationString);
                calculation.OperandString = request.CalculationString.Substring(calculation.OperandStartIndex);

                var operands = calculation.OperandString.Split(calculation.Delimiters.ToArray(), StringSplitOptions.None);

                var parsedInput = _inputParserService.Parse(operands, request.AllowNegatives, request.Ceiling);

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
