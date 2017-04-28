using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OSC.Classes;

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
            // Adiciona variável à lista caso isso seja possível
            var newValue = txtVariableValue.Text;
            var newDesc = txtVariableDesc.Text;

            if (CheckIfVariableIsValid(newValue, newDesc))
            {
                if (CheckForDuplicatedValueInProblemVariableList(newValue))
                    Helpers.ShowErrorMessage(string.Concat("Valor ", newValue, " já adicionado."));
                else
                    AddNewVariable(newValue, newDesc);

                Helpers.ClearFormValues(this);
            }

            UpdateButtonsEnableStatus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Ao editar remove da lista e adiciona os valores nos campos
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

            UpdateButtonsEnableStatus();
        }

        private void btnRemoveVariable_Click(object sender, EventArgs e)
        {
            _problemVariables.RemoveAt(variableList.SelectedIndex);
            variableList.Items.RemoveAt(variableList.SelectedIndex);
            UpdateButtonsEnableStatus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var function = new Function(_problemVariables);
            function.Show();

            Hide();
        }

        private void variableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnableStatus();
        }

        private void txtVariableValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtVariableValue.Text.Length == 0 && Char.IsNumber(e.KeyChar))
            {
                Helpers.ShowErrorMessage(@"O primeiro dígito do valor da variável não pode ser um número!");
                e.Handled = true;
            }
        }

        private void txtVariableData_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnableStatus();
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

        private void UpdateButtonsEnableStatus()
        {
            // Caso esvazie a lista de variáveis, desabilita botão para ir para o próximo passo
            if (variableList.Items.Count == 0 || variableList.SelectedIndex == -1)
            {
                btnRemoveVariable.Enabled = false;
                btnEdit.Enabled = false;
            }

            // Caso esteja selecionado na lista algum valor, habilita os botões
            if (variableList.SelectedIndex != -1)
            {
                btnRemoveVariable.Enabled = true;
                btnEdit.Enabled = true;
            }

            // Verifica se a lista de variáveis tem valores o suficiente para ir para o próximo passo
            btnNext.Enabled = _problemVariables.Count >= 2;

            // Verifica se é possível adicionar o valor a lista
            btnAddVariable.Enabled = txtVariableDesc.Text.Length != 0 && txtVariableDesc.Text.Length != 0;
        }

        private bool CheckForDuplicatedValueInProblemVariableList(string newValue)
        {
            return _problemVariables.Any(p => p.Value == newValue);
        }

        private bool CheckIfVariableIsValid(string newValue, string newDesc)
        {
            if (string.IsNullOrEmpty(newValue) || string.IsNullOrEmpty(newDesc))
            {
                Helpers.ShowErrorMessage("Para adicionar uma nova variável é necessário preencher o valor e a descrição.");
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
    }
}
