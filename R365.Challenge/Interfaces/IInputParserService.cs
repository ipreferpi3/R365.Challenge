namespace R365.Challenge.Interfaces
{
    public interface IInputParserService
    {
        List<int> Parse(string[] input, bool allowNegatives = false, int ceiling = 1000);
    }
}
