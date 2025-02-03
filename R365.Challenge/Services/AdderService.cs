using R365.Challenge.Interfaces;

namespace R365.Challenge.Services
{
    public class AdderService : IAdderService
    {
        public int TryAdd(string[] args)
        {
            if (args.Length > 2)
            {
                throw new Exception("Arguement length cannot exceed 2 operands.");
            }

            var total = 0;

            foreach (var op in args)
            {
                if (int.TryParse(op, out int value))
                {
                    total += value;
                }
                else
                {
                    //Here for debugging purposes
                    total += 0;
                }
            }

            return total;
        }
    }
}
