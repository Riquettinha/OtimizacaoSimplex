using System.Collections.Generic;
using System.ComponentModel;

namespace OSC
{
    public enum RestrictionType
    {
        [Description("Menor ou igual a")]
        LessThan = 0,
        [Description("Maior ou igual a")]
        MoreThan = 0,
        [Description("Igual a")]
        EqualTo = 0
    }

    public class RestrictionFunctionData
    {
        public RestrictionType RestrictionType { get; set; }
        public decimal RestrictionValue { get; set; }
        public List<RestrictionData> RestrictionData { get; set; }
    }
}
