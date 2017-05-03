namespace OSC.Problem_Classes
{
    public class RestrictionVariableData
    {
        public decimal RestrictionValue { get; set; }
        public VariableData RestrictionVariable { get; set; }

        public RestrictionVariableData()
        {
            RestrictionValue = 0;
        }

        public RestrictionVariableData Clone()
        {
            return new RestrictionVariableData
            {
                RestrictionValue = RestrictionValue,
                RestrictionVariable = RestrictionVariable.Clone()
            };
        }
    }
}
