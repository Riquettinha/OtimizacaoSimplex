using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OSC
{
    public partial class Main : Form
    {
        List<VariableData> variables = new List<VariableData>();
        public Main()
        {
            InitializeComponent();
        }

        private void btnRemoveVariable_Click(object sender, EventArgs e)
        {
            // Remove variable from list case possible.
            if(variableList.Items.Count != 0)
                variableList.Items.RemoveAt(variableList.SelectedIndex);
            else
                Helpers.ShowErrorMessage("Impossível remover valor de lista vazia!");
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            // Add to variable list the new value case possible
            var newValue = txtVariableValue.Text;
            var newDesc = txtVariableDesc.Text;

            if (CheckIfValueIsValid(newValue, newDesc))
            {
                if (CheckForDuplicatedValueInVariableList(newValue))
                {
                    Helpers.ShowErrorMessage(string.Concat("Valor ", newValue, " já adicionado."));
                    Helpers.ClearFormValues(this);
                }
                else
                {
                    AddNewVariable(newValue, newDesc);
                }
            }
        }

        public void AddNewVariable(string value, string desc)
        {
            var newVariable = new VariableData
            {
                Value = value,
                Description = desc
            };
            variables.Add(newVariable);

            variableList.Items.Add(string.Concat(newVariable.Value, " - ", newVariable.Description));
        }

        private void btnNext(object sender, EventArgs e)
        {
            // Go to next Simples step case possible.
            if (variableList.Items.Count < 2)
            {
                Helpers.ShowErrorMessage("Número de variáveis insuficientes!");
            }
            else
            {
                var function = new Function(variables);
                function.ShowDialog();

                Hide();
            }
        }

        private bool CheckForDuplicatedValueInVariableList(string newValue)
        {
            foreach (object existedValue in variableList.Items)
                if (existedValue.ToString().Split('-')[0].Trim() == newValue)
                    return true;

            return false;
        }

        private bool CheckIfValueIsValid(string newValue, string newDesc)
        {
            if(string.IsNullOrEmpty(newValue) || string.IsNullOrEmpty(newDesc))
            {
                Helpers.ShowErrorMessage("Para adicionar uma nova variável é necessário preencher o valor e a descrição.");
                return false;
            }
            if (char.IsNumber(newValue[0]))
            {
                Helpers.ShowErrorMessage("O primeiro dígito do valor não pode ser um número!");
                return false;
            }            
            if (Helpers.CheckForInvalidChars(newValue) || Helpers.CheckForInvalidChars(newDesc))
            {
                Helpers.ShowErrorMessage("O valor ou descrição possuem caracteres inválidos, somente são permitidos letras e númros.");
                return false;
            }

            return true;
        }
    }
}
