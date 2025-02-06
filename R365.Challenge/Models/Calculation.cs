namespace R365.Challenge.Models
{
    public class Calculation
    {
        public Calculation() { }

        public List<string> Delimiters { get; set; }
        public int OperandStartIndex { get; set; }
        public string OperandString { get; set; }
        public List<string> Operands { get; set; }
    }
}
