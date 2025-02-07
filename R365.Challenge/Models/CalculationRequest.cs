namespace R365.Challenge.Models
{
    public class CalculationRequest
    {
        public CalculationRequest() { }

        public string CalculationString { get; set; }
        public CalculationTypes CalculationType { get; set; }
        public bool AllowNegatives { get; set; }
        public int Ceiling { get; set; }
    }
}
