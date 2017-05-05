using System;
using System.Collections.Generic;
using System.Linq;
using OSC.Classes;
using OSC.SimplexApi;

namespace OSC.Problem_Classes
{
    static class VariableDataHelper
    {
        public static bool CheckIfVariableIsValid(VariableData variable)
        {
            if (string.IsNullOrEmpty(variable.Value) || string.IsNullOrEmpty(variable.Description))
            {
                Helpers.ShowErrorMessage("Para adicionar uma nova variável é necessário preencher o valor e a descrição.");
                return false;
            }
            if (Helpers.CheckForSpace(variable.Value))
            {
                Helpers.ShowErrorMessage("O valor não pode possuir espaços em branco.");
                return false;
            }
            if (Helpers.CheckForInvalidChars(variable.Value) || Helpers.CheckForInvalidChars(variable.Description))
            {
                Helpers.ShowErrorMessage("O valor ou descrição possuem caracteres inválidos, somente são permitidos letras e númros.");
                return false;
            }

            return true;
        }

        public static bool CheckForDuplicatedValueInProblemVariableList(List<VariableData> problemVariables, VariableData newVariable)
        {
            return problemVariables.Any(p => p.Value == newVariable.Value);
        }
    }
}
