using R365.Challenge.Interfaces;
using R365.Challenge.Services;
using System;
using Microsoft.Extensions.DependencyInjection;

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
                .AddSingleton<IAdderService, AdderService>()
                .AddSingleton<ICalculatorService, CalculatorService>()
                .AddSingleton<IInputParserService, InputParserService>()
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
            Console.Write("Provide an input: ");
            var input = Console.ReadLine();

            try
            {
                var sum = _calculator.Calculate(input);
                Console.WriteLine(string.Format("Total value: {0}", sum));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
