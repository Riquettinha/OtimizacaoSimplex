namespace OSC.Problem_Classes
{
    public class FunctionData
    {
        public bool Maximiza { get; set; }
        public decimal FinalValue { get; set; }

        public FunctionData Clone()
        {
            return new FunctionData
            {
                Maximiza = Maximiza
            };
        }
    }
}
