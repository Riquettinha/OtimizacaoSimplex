using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OSC.Classes;
using OSC.Problem_Classes;
using OSC.SimplexApi;

namespace OSC
{
    public partial class Variables : Form
    {
        readonly ProblemData _problem;
        public Variables()
        {
            InitializeComponent();
            _problem = Create.ProblemData();
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            // Adiciona variável à lista caso isso seja possível
            var newVariable = CreateNewVariableObject(txtVariableValue.Text, txtVariableDesc.Text);

            if (VariableDataHelper.CheckIfVariableIsValid(newVariable))
            {
                if (VariableDataHelper.CheckForDuplicatedValueInProblemVariableList(_problem.Variables, newVariable))
                    Helpers.ShowErrorMessage(string.Concat("Valor ", newVariable.Value, " já adicionado."));
                else
                    AddNewVariable(newVariable);

                Helpers.ClearFormValues(this);
                txtVariableDesc.ForeColor = Color.DimGray;
                txtVariableValue.Focus();
            }

            UpdateButtonsEnableStatus();
        }

        private VariableData CreateNewVariableObject(string value, string desc)
        {
            return new VariableData
            {
                Value = value,
                Description = desc
            };
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Ao editar remove da lista e adiciona os valores nos campos
            if (variableList.SelectedIndex > -1)
            {
                var variableValue = variableList.Items[variableList.SelectedIndex].ToString();
                txtVariableValue.Text = variableValue.Split('-')[0].Trim();
                txtVariableDesc.Text = variableValue.Split('-')[1].Trim();

                _problem.Variables.RemoveAt(variableList.SelectedIndex);
                variableList.Items.RemoveAt(variableList.SelectedIndex);
            }
            else
                Helpers.ShowErrorMessage("Impossível editar valor de lista vazia!");

            UpdateButtonsEnableStatus();
        }

        private void btnRemoveVariable_Click(object sender, EventArgs e)
        {
            _problem.Variables.RemoveAt(variableList.SelectedIndex);
            variableList.Items.RemoveAt(variableList.SelectedIndex);
            UpdateButtonsEnableStatus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var function = new Function(_problem);
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
            if (!DescTBFontIsBlack())
                txtVariableDesc.Text = txtVariableValue.Text;
        }

        public void AddNewVariable(VariableData newVariable)
        {
            _problem.Variables.Add(newVariable);
            variableList.Items.Add(string.Concat(newVariable.Value, " - ", newVariable.Description));
        }

        private void UpdateButtonsEnableStatus()
        {
            // Caso esvazie a lista de variáveis, desabilita botão para ir para o próximo passo
            if (variableList.Items.Count == 0 || variableList.SelectedIndex == -1)
            {
                btnRemove.Enabled = false;
                btnEdit.Enabled = false;
            }

            // Caso esteja selecionado na lista algum valor, habilita os botões
            if (variableList.SelectedIndex != -1)
            {
                btnRemove.Enabled = true;
                btnEdit.Enabled = true;
            }

            // Verifica se a lista de variáveis tem valores o suficiente para ir para o próximo passo
            btnNext.Enabled = _problem.Variables.Count >= 2;
            
            // Verifica se é possível adicionar o valor a lista
            btnAdd.Enabled = txtVariableDesc.Text.Length != 0 && txtVariableDesc.Text.Length != 0;
            if (!btnAdd.Enabled && btnNext.Enabled)
                AcceptButton = btnNext;
            else
                AcceptButton = btnAdd;
        }
        
        public bool DescTBFontIsBlack()
        {
            return txtVariableDesc.ForeColor == Color.Black;
        }

        private void txtVariableDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (!DescTBFontIsBlack())
            {
                txtVariableDesc.ForeColor = Color.Black;
            }
        }

        private void txtVariableDesc_MouseClick(object sender, MouseEventArgs e)
        {
            if (!DescTBFontIsBlack())
            {
                txtVariableDesc.SelectAll();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Variables_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["Main"].Show();
        }
    }
}
