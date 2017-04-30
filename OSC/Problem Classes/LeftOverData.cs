using System.Collections.Generic;

namespace OSC.Problem_Classes
{
    public class LeftOverData
    {
        public VariableData LeftOverVariable = new VariableData();
        public decimal FreeMember { get; set; }
        public List<RestrictionVariableData> RestrictionVariables = new List<RestrictionVariableData>();
    }
}
