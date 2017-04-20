using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OSC
{
    public partial class Variables : Form
    {
        readonly List<VariableData> _problemVariables = new List<VariableData>();
        public Variables()
        {
            InitializeComponent();
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
                }
                else
                {
                    AddNewVariable(newValue, newDesc);

                    // If you already have at least two variables, allow go to the next step
                    if (variableList.Items.Count > 1)
                        btnNext.Enabled = true;
                }

                Helpers.ClearFormValues(this);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Case possible, remove from list and load the previous value
            if (variableList.SelectedIndex > -1)
            {
                var variableValue = variableList.Items[variableList.SelectedIndex].ToString();
                txtVariableValue.Text = variableValue.Split('-')[0].Trim();
                txtVariableDesc.Text = variableValue.Split('-')[1].Trim();

                _problemVariables.RemoveAt(variableList.SelectedIndex);
                variableList.Items.RemoveAt(variableList.SelectedIndex);
            }
            else
                Helpers.ShowErrorMessage("Impossível editar valor de lista vazia!");
        }

        private void btnRemoveVariable_Click(object sender, EventArgs e)
        {
            // Remove variable from list case possible.
            if (variableList.SelectedIndex > -1)
            {
                _problemVariables.RemoveAt(variableList.SelectedIndex);
                variableList.Items.RemoveAt(variableList.SelectedIndex);

                if (variableList.Items.Count == 0)
                {
                    // Disable edit and remove buttons case empty variableList.
                    btnRemoveVariable.Enabled = false;
                    btnEdit.Enabled = false;
                }

                // If you already have less than two variables, don't allow go to the next step
                if (variableList.Items.Count < 2)
                    btnNext.Enabled = false;
            }
            else
                Helpers.ShowErrorMessage("Impossível remover valor de lista vazia!");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Go to next Simples step case possible.
            if (variableList.Items.Count < 2)
            {
                Helpers.ShowErrorMessage("Número de variáveis insuficientes!");
            }
            else
            {
                var function = new Function(_problemVariables);
                function.Show();

                Hide();
            }
        }

        public void AddNewVariable(string value, string desc)
        {
            var newVariable = new VariableData
            {
                Value = value,
                Description = desc
            };
            _problemVariables.Add(newVariable);

            variableList.Items.Add(string.Concat(newVariable.Value, " - ", newVariable.Description));
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
            if (string.IsNullOrEmpty(newValue) || string.IsNullOrEmpty(newDesc))
            {
                Helpers.ShowErrorMessage("Para adicionar uma nova variável é necessário preencher o valor e a descrição.");
                return false;
            }
            if (char.IsNumber(newValue[0]))
            {
                Helpers.ShowErrorMessage("O primeiro dígito do valor não pode ser um número!");
                return false;
            }
            if (Helpers.CheckForSpace(newValue))
            {
                Helpers.ShowErrorMessage("O valor não pode possuir espaços em branco.");
                return false;
            }
            if (Helpers.CheckForInvalidChars(newValue) || Helpers.CheckForInvalidChars(newDesc))
            {
                Helpers.ShowErrorMessage("O valor ou descrição possuem caracteres inválidos, somente são permitidos letras e númros.");
                return false;
            }

            return true;
        }

        private void variableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(variableList.SelectedIndex != -1)
            {
                btnRemoveVariable.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnRemoveVariable.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void Variables_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_problemVariables.Count > 0)
            {
                var userDecision =
                    MessageBox.Show(@"O cálculo ainda não foi finalizado, deseja fechar o programa mesmo assim?",
                        @"Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (userDecision == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
