using OSC.Classes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
        public VariableData RestrictionLeftOver { get; set; }

        public bool CheckIfIsNewRestriction(List<RestrictionFunctionData> problemRestrictions)
        {
            // Verifica quais problmas existentes possuem mesmos valores para todas as variáveis da restrição
            var existedProblemWithTheSameSubData = problemRestrictions.Where(p => p.RestrictionData.All(
                d => RestrictionData.Any(n => d.RestrictionValue == n.RestrictionValue &&
                d.RestrictionVariable.Value == n.RestrictionVariable.Value))).ToList();

            // Verifica desses existentes se algum deles possui também o mesmo tipo e valor da restrição
            return existedProblemWithTheSameSubData.All(e => e.RestrictionType != RestrictionType ||
            e.RestrictionValue != RestrictionValue);
        }

        public string CreateRestrictionString()
        {
            string restrictionString = "";
            for (int i = 0; i < RestrictionData.Count; i++)
            {
                if (i != 0)
                    restrictionString += "+ ";
                restrictionString += RestrictionData[i].RestrictionValue +
                                     RestrictionData[i].RestrictionVariable.Value + " ";
            }
            restrictionString += Helpers.GetRestrictionString(RestrictionType) + RestrictionValue;
            return restrictionString;
        }
        
        public bool CheckWithConflict(List<RestrictionFunctionData> problemRestrictions)
        {
            //TODO: Se possível corrigir erros desse método.
            // Verifica quais problmas existentes possuem mesmos valores para todas as variáveis da restrição
            var existedProblemWithTheSameSubData =
                problemRestrictions.Where(p => p.RestrictionData.All(d => RestrictionData.Any(n =>
                    d.RestrictionValue == n.RestrictionValue &&
                    d.RestrictionVariable.Value == n.RestrictionVariable.Value))).ToList();

            // Verifica se somente o tipo é diferente
            var onlyTypeIsDifferent =
                existedProblemWithTheSameSubData.Any(s => s.RestrictionValue == RestrictionValue && s.RestrictionType != RestrictionType);
            if (onlyTypeIsDifferent)
                return false;

            var possibleConflict = existedProblemWithTheSameSubData.Where(s => s.RestrictionType != RestrictionType).ToList();
            // Se somente o tipo é diferente quer dizer que está adicionando uma restrição que impossibilita
            // Se os tipos são diferentes, mas pelo menos um deles é do tipo "=" também impossibilita o cálculo
            if (onlyTypeIsDifferent || (possibleConflict.Count > 0 &&
                (possibleConflict.Any(p => p.RestrictionType == RestrictionType.EqualTo) ||
                RestrictionType == RestrictionType.EqualTo)))
            {
                return false;
            }

            foreach (var existingPossibleConflict in possibleConflict)
            {
                if ((existingPossibleConflict.RestrictionType == RestrictionType.MoreThan &&
                     existingPossibleConflict.RestrictionValue > RestrictionValue) ||
                    existingPossibleConflict.RestrictionType == RestrictionType.LessThan &&
                    existingPossibleConflict.RestrictionValue < RestrictionValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
