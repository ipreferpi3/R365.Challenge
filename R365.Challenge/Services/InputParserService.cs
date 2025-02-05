using R365.Challenge.Interfaces;
using System.Diagnostics;

namespace R365.Challenge.Services
{
    public class InputParserService : IInputParserService
    {
        public List<int> Parse(string[] input)
        {
            var inputList = new List<int>();

            foreach (var inputItem in input)
            {
                inputList.Add(int.TryParse(inputItem, out int item) ? item : 0);
            }

            ContainsNegatives(inputList);

            return inputList;
        }

        private bool ContainsNegatives(List<int> input) 
        {
            var negatives = input.Where(i => i < 0);

            if (negatives.Any())
            {
                throw new ArgumentException(string.Format("Input cannot contain negatives : {0}", string.Join(", ", negatives)));
            }

            return negatives.Any();
        }
    }
}
