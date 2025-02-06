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
                var delimiterString = input.Substring(2, 1);
                delimiters.Add(delimiterString);
                //Add one to this value since the close for custom delimiters is 2 characters long.
                calculation.OperandStartIndex = input.IndexOf("\\n") + 2;

                if (calculation.OperandStartIndex != 5)
                {
                    //At this point, custom delimiters should be single characters and bound between // and \n
                    //If this format is not used, we need to throw here
                    throw new ArgumentException("Custom delimiters must be a single character and denoted between // and \\n");
                }
            }

            calculation.Delimiters = delimiters;

            return calculation;
        }
    }
}
