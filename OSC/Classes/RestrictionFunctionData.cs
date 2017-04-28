using System.Collections.Generic;
using System.ComponentModel;

namespace OSC
{
    public enum RestrictionType
    {
        [Description("Menor ou igual a")]
        LessThan = 0,
        [Description("Maior ou igual a")]
        MoreThan = 1,
        [Description("Igual a")]
        EqualTo = 2
    }

    public class RestrictionFunctionData
    {
        public RestrictionType RestrictionType { get; set; }
        public decimal RestrictionValue { get; set; }
        public List<RestrictionData> RestrictionData { get; set; }
    }
}
