using System.Collections.Generic;

namespace OSC.Problem_Classes
{
    public class ProblemData
    {
        public List<VariableData> Variables = new List<VariableData>();
        public FunctionData Function { get; set; }
        public List<RestrictionFunctionData> Restrictions = new List<RestrictionFunctionData>();
    }
}
