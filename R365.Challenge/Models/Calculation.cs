namespace R365.Challenge.Models
{
    public class Calculation
    {
        public Calculation() { }

        public List<string> Delimiters { get; set; }
        public int OperandStartIndex { get; set; }
        public string Operands { get; set; }
        public string Formula { get; set; }
    }
}
