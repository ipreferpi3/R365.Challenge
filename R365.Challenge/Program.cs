using R365.Challenge.Interfaces;
using R365.Challenge.Services;
using System;
using Microsoft.Extensions.DependencyInjection;
using R365.Challenge.Models;

namespace R365.Challenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = CreateServices();

            var app = services.GetRequiredService<Application>();

            app.Run();
        }

        private static ServiceProvider CreateServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IAdditionService, AdditionService>()
                .AddSingleton<ISubtractionService, SubtractionService>()
                .AddSingleton<IMultiplicationService, MultiplicationService>()
                .AddSingleton<IDivisionService, DivisionService>()
                .AddSingleton<ICalculatorService, CalculatorService>()
                .AddSingleton<IInputParserService, InputParserService>()
                .AddSingleton<IDelimiterParserService, DelimiterParserService>()
                .AddSingleton<Application>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }

    public class Application
    {
        private readonly ICalculatorService _calculator;

        public Application(ICalculatorService calculator)
        {
            _calculator = calculator;
        }

        public void Run()
        {
            Console.Write("Select operation type [+,-,*,/]: ");
            var operationType = Console.ReadLine();
            Console.Write("Provide calculation input: ");
            var calculationInput = Console.ReadLine();
            
            try
            {
                var calculationInputType = CalculationTypes.None;

                calculationInputType = operationType switch
                {
                    "+" => CalculationTypes.Addition,
                    "-" => CalculationTypes.Subtraction,
                    "*" => CalculationTypes.Multiplication,
                    "/" => CalculationTypes.Division,
                    _ => throw new Exception("An operation must be chosen."),
                };
                var request = new CalculationRequest
                {
                    CalculationString = calculationInput,
                    CalculationType = calculationInputType
                };

                var result = _calculator.Calculate(request);
                Console.WriteLine(string.Format("Result: {0}", result.Total));
                Console.WriteLine(string.Format("Parsed formula: {0}", result.Formula));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
