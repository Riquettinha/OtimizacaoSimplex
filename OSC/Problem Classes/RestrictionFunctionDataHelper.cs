using System.Collections.Generic;
using System.Linq;
using OSC.Classes;
using OSC.SimplexApi;

namespace OSC.Problem_Classes
{
    static class RestrictionFunctionDataHelper
    {
        public static bool CheckIfIsNewRestriction(List<RestrictionFunctionData> problemRestrictions, RestrictionFunctionData newRestriction)
        {
            // Verifica quais problmas existentes possuem mesmos valores para todas as variáveis da restrição
            var existedProblemWithTheSameSubData = problemRestrictions.Where(p => p.RestrictionData.All(
                d => newRestriction.RestrictionData.Any(n => d.RestrictionValue == n.RestrictionValue &&
                d.RestrictionVariable.Value == n.RestrictionVariable.Value))).ToList();

            // Verifica desses existentes se algum deles possui também o mesmo tipo e valor da restrição
            return existedProblemWithTheSameSubData.All(e => e.RestrictionType != newRestriction.RestrictionType ||
            e.RestrictionValue != newRestriction.RestrictionValue);
        }

        public static string GetRestrictionString(RestrictionFunctionData restriction)
        {
            // Monta um string simples com os dados da restrição
            string restrictionString = "";
            for (int i = 0; i < restriction.RestrictionData.Count; i++)
            {
                if (i != 0)
                    restrictionString += "+ ";
                restrictionString += restriction.RestrictionData[i].RestrictionValue +
                                     restriction.RestrictionData[i].RestrictionVariable.Value + " ";
            }
            restrictionString += Helpers.GetRestrictionTypeString(restriction.RestrictionType) + restriction.RestrictionValue;
            return restrictionString;
        }

        public static string GetSimplexRestrictionString(RestrictionFunctionData restriction)
        {
            // Monta uma string com os dados da restrição incluindo variável de folga
            string restrictionString = "";
            foreach (RestrictionVariableData restr in restriction.RestrictionData)
            {
                var restrictionVariableValue = restr.RestrictionValue.GetString();
                if (string.IsNullOrEmpty(restrictionString))
                    restrictionVariableValue = restrictionVariableValue.Replace(" + ", "");

                if (restr.RestrictionValue != 0)
                {
                    if (restr.RestrictionValue != 1)
                        restrictionString += restrictionVariableValue;
                    else if (!string.IsNullOrEmpty(restrictionString))
                        restrictionString += restrictionVariableValue.Replace("1", "");
                    restrictionString += restr.RestrictionVariable.Value;
                }
            }

            restrictionString += restriction.RestrictionLeftOver.LeftOverVariable.FunctionValue.GetString().Replace("1", "") +
                restriction.RestrictionLeftOver.LeftOverVariable.Value;
            restrictionString += " " + Helpers.GetRestrictionTypeString(restriction.RestrictionType) + " " + restriction.RestrictionValue;
            return restrictionString;
        }

        public static string GetSimplexFreeMemberString(RestrictionFunctionData restriction)
        {
            // Monta uma string com os dados da restrição incluindo variável de folga
            string restrictionString = restriction.RestrictionLeftOver.LeftOverVariable.Value + " = " +
                restriction.RestrictionLeftOver.FreeMember.GetString().Replace(" + ", "") + " - (";
            foreach (RestrictionVariableData restr in restriction.RestrictionLeftOver.RestrictionVariables)
            {
                var restrictionVariableValue = restr.RestrictionValue.GetString();
                if (restr.RestrictionValue != 0)
                {
                    if (restr.RestrictionValue != 1)
                        restrictionString += restrictionVariableValue;
                    else if (!string.IsNullOrEmpty(restrictionString))
                        restrictionString += restrictionVariableValue.Replace("1", "");
                    restrictionString += restr.RestrictionVariable.Value;
                }
            }
            restrictionString += " )";
            return restrictionString;
        }

        public static bool CheckWithConflict(List<RestrictionFunctionData> problemRestrictions, RestrictionFunctionData newRestriction)
        {
            //TODO: Se possível corrigir erros desse método.
            // Verifica quais problmas existentes possuem mesmos valores para todas as variáveis da restrição
            var existedProblemWithTheSameSubData =
                problemRestrictions.Where(p => p.RestrictionData.All(d => newRestriction.RestrictionData.Any(n =>
                    d.RestrictionValue == n.RestrictionValue &&
                    d.RestrictionVariable.Value == n.RestrictionVariable.Value))).ToList();

            // Verifica se somente o tipo é diferente
            var onlyTypeIsDifferent =
                existedProblemWithTheSameSubData.Any(s => s.RestrictionValue == newRestriction.RestrictionValue && s.RestrictionType != newRestriction.RestrictionType);
            if (onlyTypeIsDifferent)
                return false;

            var possibleConflict = existedProblemWithTheSameSubData.Where(s => s.RestrictionType != newRestriction.RestrictionType).ToList();
            // Se somente o tipo é diferente quer dizer que está adicionando uma restrição que impossibilita
            // Se os tipos são diferentes, mas pelo menos um deles é do tipo "=" também impossibilita o cálculo
            if ((possibleConflict.Count > 0 &&
                 (possibleConflict.Any(p => p.RestrictionType == RestrictionType.EqualTo) ||
                  newRestriction.RestrictionType == RestrictionType.EqualTo)))
            {
                return false;
            }

            foreach (var existingPossibleConflict in possibleConflict)
            {
                if ((existingPossibleConflict.RestrictionType == RestrictionType.MoreThan &&
                     existingPossibleConflict.RestrictionValue > newRestriction.RestrictionValue) ||
                    existingPossibleConflict.RestrictionType == RestrictionType.LessThan &&
                    existingPossibleConflict.RestrictionValue < newRestriction.RestrictionValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
