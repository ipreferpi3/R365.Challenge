using R365.Challenge.Models;

namespace R365.Challenge.Interfaces
{
    public interface IDelimiterParserService
    {
        Calculation GetDelimiters(string input);
    }
}
