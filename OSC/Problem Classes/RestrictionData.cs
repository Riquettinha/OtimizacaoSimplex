namespace OSC
{
    public class RestrictionData
    {
        public decimal RestrictionValue { get; set; }
        public VariableData RestrictionVariable { get; set; }

        public RestrictionData()
        {
            RestrictionValue = 0;
        }
    }
}
