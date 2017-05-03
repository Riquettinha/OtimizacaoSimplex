using System.Collections.Generic;
using System.Linq;

namespace OSC.Problem_Classes
{
    public class LeftOverData
    {
        public VariableData LeftOverVariable = new VariableData();
        public decimal FreeMember { get; set; }
        public List<RestrictionVariableData> RestrictionVariables = new List<RestrictionVariableData>();

        public LeftOverData Clone()
        {
            return new LeftOverData
            {
                LeftOverVariable = LeftOverVariable.Clone(),
                FreeMember = FreeMember,
                RestrictionVariables = RestrictionVariables.Select(r => r.Clone()).ToList()
            };
        }
    }
}
