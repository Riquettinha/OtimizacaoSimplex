using OSC.Classes;
using System.Collections.Generic;
using System.Linq;

namespace OSC.Problem_Classes
{
    public class VariableData
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public decimal FunctionValue { get; set; }
        public decimal FinalValue { get; set; }

        public bool CheckIfVariableIsValid()
        {
            if (string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Description))
            {
                Helpers.ShowErrorMessage("Para adicionar uma nova variável é necessário preencher o valor e a descrição.");
                return false;
            }
            if (Helpers.CheckForSpace(Value))
            {
                Helpers.ShowErrorMessage("O valor não pode possuir espaços em branco.");
                return false;
            }
            if (Helpers.CheckForInvalidChars(Value) || Helpers.CheckForInvalidChars(Description))
            {
                Helpers.ShowErrorMessage("O valor ou descrição possuem caracteres inválidos, somente são permitidos letras e númros.");
                return false;
            }

            return true;
        }

        public bool CheckForDuplicatedValueInProblemVariableList(List<VariableData> problemVariables)
        {
            return problemVariables.Any(p => p.Value == Value);
        }
    }
}
