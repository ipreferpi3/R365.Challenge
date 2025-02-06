using R365.Challenge.Interfaces;
using R365.Challenge.Models;
using System.Collections.Generic;

namespace R365.Challenge.Services
{
    public class DelimiterParserService : IDelimiterParserService
    {
        private readonly List<string> DefaultDelimiters = new List<string> { ",", "\\n" };

        public Calculation GetDelimiters(string input)
        {
            var delimiters = DefaultDelimiters;
            var calculation = new Calculation();

            if (input.StartsWith("//"))
            {
                var operandStartIndex = input.IndexOf("\\n");

                if (operandStartIndex == 3)
                {
                    delimiters.Add(GetSingleCharacterCustomDelimiter(input));
                    calculation.Delimiters = delimiters;
                    calculation.OperandStartIndex = operandStartIndex + 2;
                    return calculation;
                }

                if (operandStartIndex > 3)
                {
                    delimiters.AddRange(GetMultiCharacterCustomDelimiter(input, operandStartIndex));
                    calculation.Delimiters = delimiters;
                    calculation.OperandStartIndex = operandStartIndex + 2;
                    return calculation;
                }

                if (operandStartIndex <= 0)
                {
                    //Custom delimiter must denote start and end for correct format. 
                    //Throw error if input does not contain \n
                    throw new ArgumentException("Custom delimiters must be denoted between // and \\n");
                }
            }

            calculation.Delimiters = delimiters;

            return calculation;
        }
        private string GetSingleCharacterCustomDelimiter(string input)
        {
            var delimiterString = input.Substring(2, 1);
            return delimiterString;
        }

        private List<string> GetMultiCharacterCustomDelimiter(string input, int endIndex)
        {
            var delimiterString = input.Substring(2, endIndex - 2);
            var delimiters = delimiterString.Split(new string[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return delimiters;
        }
    }
}
