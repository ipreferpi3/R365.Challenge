using R365.Challenge.Interfaces;

namespace R365.Challenge.Services
{
    public class AdderService : IAdderService
    {
        public int TryAdd(List<int> input)
        {
            var total = 0;

            foreach (var op in input)
            {
                total += op;
            }

            return total;
        }
    }
}
