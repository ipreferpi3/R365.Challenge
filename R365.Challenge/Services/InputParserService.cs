using R365.Challenge.Interfaces;
using System.Diagnostics;

namespace R365.Challenge.Services
{
    public class InputParserService : IInputParserService
    {
        public List<int> Parse(string[] input, bool allowNegatives = false, int ceiling = 1000)
        {
            var inputList = new List<int>();

            foreach (var inputItem in input)
            {
                inputList.Add(int.TryParse(inputItem, out int item) ? item : 0);
            }

            ContainsNegatives(inputList, allowNegatives);

            var filteredInputList = FilterGreaterThan(inputList, ceiling);

            return filteredInputList;
        }

        private bool ContainsNegatives(List<int> input, bool isAllowed = false) 
        {
            var negatives = input.Where(i => i < 0);

            if (negatives.Any() && !isAllowed)
            {
                throw new ArgumentException(string.Format("Input cannot contain negatives : {0}", string.Join(", ", negatives)));
            }

            return negatives.Any();
        }

        private List<int> FilterGreaterThan(List<int> input, int ceiling = 1000)
        {
            var filteredInputList = input.Select(i => i <= ceiling ? i : 0).ToList();

            return filteredInputList;
        }
    }
}
