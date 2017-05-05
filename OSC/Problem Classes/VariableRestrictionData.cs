namespace OSC.Problem_Classes
{
    public class VariableRestrictionData
    {
        public decimal RestrictionValue { get; set; }
        public VariableData RestrictionVariable { get; set; }

        public VariableRestrictionData()
        {
            RestrictionValue = 0;
        }
    }
}
