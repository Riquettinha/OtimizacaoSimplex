using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OSC.Classes;

namespace OSC
{
    public partial class Restriction : Form
    {
        readonly List<VariableData> _problemVariables;
        readonly List<RestrictionFunctionData> _problemRestrictions = new List<RestrictionFunctionData>();

        public Restriction(FunctionData functionData)
        {
            InitializeComponent();
            _problemVariables = functionData.Variables;

            LoadExtraFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TextBoxFilledCount() > 1 && RestrictionValueFilled())
            {
                var newRestr = CreateRestrictionObject();
                if (CheckIfIsNewRestriction(newRestr))
                {
                    restrictionList.Items.Add(CreateRestrictionString(newRestr));
                    _problemRestrictions.Add(newRestr);
                }
                else
                {
                    Helpers.ShowErrorMessage("Já existe uma restrição com estes mesmos dados!");
                }
            }
            else
            {
                Helpers.ShowErrorMessage(
                    "Para adicionar uma função de restrição é necessário que ao menos um valor seja informado.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _problemRestrictions.RemoveAt(restrictionList.SelectedIndex);
            restrictionList.Items.RemoveAt(restrictionList.SelectedIndex);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var restriction = _problemRestrictions[restrictionList.SelectedIndex];
            for (int i = 0; i < restriction.RestrictionData.Count; i++)
                Controls["txtVar" + i].Text = restriction.RestrictionData[i].RestrictionValue.ToString();

            Controls["txtCond"].Text = restriction.RestrictionValue.ToString();
            (Controls["cbCondiction"] as ComboBox).SelectedValue = restriction.RestrictionType;

            _problemRestrictions.RemoveAt(restrictionList.SelectedIndex);
            restrictionList.Items.RemoveAt(restrictionList.SelectedIndex);
        }

        private void txtVar_TextChanged(object sender, EventArgs e)
        {
            FillComboBoxItems(Controls["cbCondiction"] as ComboBox);
            UpdateButtonsEnableStatus();
        }

        private void txtVar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == ',' && (sender as TextBox).Text.ToCharArray().Any(p => p == ','))
            {
                e.Handled = true;
            }
            else if (!char.IsControl(e.KeyChar) && !Helpers.CheckIfIsAValidDecimal(((TextBox)sender).Text + e.KeyChar))
            {
                Helpers.ShowErrorMessage("Valor inserido inválido.");
                e.Handled = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Helpers.BackForm())
            {
                Application.OpenForms["Function"].Show();
                Hide();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void LoadExtraFields()
        {
            int locationX = 12;
            for (int i = 0; i < _problemVariables.Count; i++)
            {
                AddNewVariableTextBoxAndLabel(i, ref locationX);
                AddPlusLabel(i, ref locationX);
            }
            AddCondictionComboBoxAndTextBox(locationX);
        }

        private void AddNewVariableTextBoxAndLabel(int index, ref int locationX)
        {
            // Cria a textbox do valor da variável nas restrição e a label com o nome da variável
            var txtVar = new TextBox
            {
                Name = "txtVar" + index,
                Location = new Point(locationX, 173),
                Size = new Size(60, 20)
            };
            
            txtVar.KeyPress += txtVar_KeyPress;
            txtVar.TextChanged += txtVar_TextChanged;

            Controls.Add(txtVar);
            locationX += 60;

            var lbVar = new Label
            {
                Name = "lbVar" + index,
                Location = new Point(locationX, 175),
                Size = TextRenderer.MeasureText(_problemVariables[index].Value, Font),
                Text = _problemVariables[index].Value,
                BackColor = Color.Transparent
            };

            Controls.Add(lbVar);
            locationX += TextRenderer.MeasureText(_problemVariables[index].Value, Font).Width - 1;
        }

        private void AddPlusLabel(int index, ref int locationX)
        {
            if (index + 1 < _problemVariables.Count)
            {
                var plus = new Label
                {
                    Location = new Point(locationX, 175),
                    Size = TextRenderer.MeasureText("+", Font),
                    Text = @"+",
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(45, 55, 175),
                    Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold)
                };

                Controls.Add(plus);
                locationX += TextRenderer.MeasureText("+", Font).Width;
            }
        }

        private void AddCondictionComboBoxAndTextBox(int locationX)
        {
            // Cria combobox com tipo de restrição e textbox para adicionar o valor da restrição
            locationX += 5;
            var condiction = new ComboBox
            {
                Location = new Point(locationX, 172),
                Size = new Size(40, 18),
                Name = "cbCondiction",
                ForeColor = Color.FromArgb(45, 55, 175),
                Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold),
                AutoCompleteMode = AutoCompleteMode.None
            };

            FillComboBoxItems(condiction);
            
            Controls.Add(condiction);
            locationX += 45;

            // Create condiction value TextBox
            var txtVar = new TextBox
            {
                Name = "txtCond",
                Location = new Point(locationX, 173),
                Size = new Size(60, 20)
            };

            // Validate the input
            txtVar.KeyPress += txtVar_KeyPress;
            txtVar.TextChanged += txtVar_TextChanged;

            Controls.Add(txtVar);
            locationX += 60;

            // Resize form
            ResizeControls(locationX);
        }

        private void FillComboBoxItems(ComboBox condiction)
        {
            var cbSource = new List<ComboBoxItem>();
            var textsFilles = TextBoxFilledCount();
            var restrictionValueFilled = RestrictionValueFilled();
            var singleVariableValueFilled = textsFilles == 1 && !restrictionValueFilled || textsFilles == 2 && restrictionValueFilled;
            if (singleVariableValueFilled)
            {
                // Se somente um valor de váriavel de restrição está preenchido permite igualar
                cbSource.Add(new ComboBoxItem
                {
                    Text = GetRestrictionString(RestrictionType.EqualTo),
                    Value = RestrictionType.EqualTo
                });
            }

            cbSource.Add(new ComboBoxItem
            {
                Text = GetRestrictionString(RestrictionType.LessThan),
                Value = RestrictionType.LessThan
            });
            cbSource.Add(new ComboBoxItem
            {
                Text = GetRestrictionString(RestrictionType.MoreThan),
                Value = RestrictionType.MoreThan
            });
            condiction.DataSource = cbSource;
            condiction.DisplayMember = "Text";
            condiction.ValueMember = "Value";
            condiction.SelectedValue = singleVariableValueFilled ? RestrictionType.EqualTo : RestrictionType.LessThan;

        }

        private void ResizeControls(int locationX)
        {
            if (locationX + 30 > Size.Width)
            {
                var oldSize = Width*1;
                Size = new Size(locationX + 26, Size.Height);

                var diff = (locationX - oldSize + 26) /2;
                btnEdit.Size = new Size(btnEdit.Width + diff, btnEdit.Height);
                btnEdit.Location = new Point(12, 139);
                btnRemove.Size = new Size(btnRemove.Width + diff, btnRemove.Height);
                btnRemove.Location = new Point(127 + diff, btnRemove.Location.Y);
            }
        }

        private string CreateRestrictionString(RestrictionFunctionData newRestr)
        {
            string restrictionString = "";
            for (int i = 0; i < newRestr.RestrictionData.Count; i++)
            {
                if (i != 0)
                    restrictionString += "+ ";
                restrictionString += newRestr.RestrictionData[i].RestrictionValue +
                                     newRestr.RestrictionData[i].RestrictionVariable.Value + " ";
            }
            restrictionString += GetRestrictionString(newRestr.RestrictionType) + newRestr.RestrictionValue;
            return restrictionString;
        }

        private RestrictionFunctionData CreateRestrictionObject()
        {
            // Transforma os valores preenchidos na tela em um objeto do tipo RestrictionFunctionData
            var listRestricitonData = new List<RestrictionData>();
            for (int i = 0; i < _problemVariables.Count; i++)
            {
                if (!String.IsNullOrEmpty(Controls["txtVar" + i].Text))
                {
                    var newRestritcionData = new RestrictionData
                    {
                        RestrictionValue = Controls["txtVar" + i].Text.ConvertToDecimal(),
                        RestrictionVariable = _problemVariables[i]
                    };
                    listRestricitonData.Add(newRestritcionData);
                }
            }

            return new RestrictionFunctionData
            {
                RestrictionValue = Controls["txtCond"].Text.ConvertToDecimal(),
                RestrictionType = ((Controls["cbCondiction"] as ComboBox).SelectedItem as ComboBoxItem).Value,
                RestrictionData = listRestricitonData
            };
        }

        private bool CheckIfIsNewRestriction(RestrictionFunctionData newRestr)
        {
            // Verifica quais problmas existentes possuem mesmos valores para todas as variáveis da restrição
            var existedProblemWithTheSameSubData = _problemRestrictions.Where(p => p.RestrictionData.All(d => newRestr.RestrictionData.Any(n =>
                d.RestrictionValue == n.RestrictionValue && d.RestrictionVariable.Value == n.RestrictionVariable.Value))).ToList();

            // Verifica desses existentes se algum deles possui também o mesmo tipo e valor da restrição
            return existedProblemWithTheSameSubData.All(e => e.RestrictionType != newRestr.RestrictionType
                || e.RestrictionValue != newRestr.RestrictionValue);
        }

        private bool CheckWithConflict(RestrictionFunctionData newRestr)
        {
            //TODO: Se possível corrigir erros desse método.
            // Verifica quais problmas existentes possuem mesmos valores para todas as variáveis da restrição
            var existedProblemWithTheSameSubData =
                _problemRestrictions.Where(p => p.RestrictionData.All(d => newRestr.RestrictionData.Any(n =>
                    d.RestrictionValue == n.RestrictionValue &&
                    d.RestrictionVariable.Value == n.RestrictionVariable.Value))).ToList();

            // Verifica se somente o tipo é diferente
            var onlyTypeIsDifferent =
                existedProblemWithTheSameSubData.Any(s => s.RestrictionValue == newRestr.RestrictionValue && s.RestrictionType != newRestr.RestrictionType);
            if(onlyTypeIsDifferent)
                return false;

            var possibleConflict = existedProblemWithTheSameSubData.Where(s => s.RestrictionType != newRestr.RestrictionType).ToList();
            // Se somente o tipo é diferente quer dizer que está adicionando uma restrição que impossibilita
            // Se os tipos são diferentes, mas pelo menos um deles é do tipo "=" também impossibilita o cálculo
            if (onlyTypeIsDifferent || (possibleConflict.Count > 0 && 
                (possibleConflict.Any(p => p.RestrictionType == RestrictionType.EqualTo) ||
                newRestr.RestrictionType == RestrictionType.EqualTo)))
            {
                return false;
            }

            foreach (var existingPossibleConflict in possibleConflict)
            {
                if ((existingPossibleConflict.RestrictionType == RestrictionType.MoreThan &&
                     existingPossibleConflict.RestrictionValue > newRestr.RestrictionValue) ||
                    existingPossibleConflict.RestrictionType == RestrictionType.LessThan &&
                    existingPossibleConflict.RestrictionValue < newRestr.RestrictionValue)
                {
                    return false;
                }
            }

            return true;
        }

        private string GetRestrictionString(RestrictionType restType)
        {
            switch (restType)
            {
                case RestrictionType.EqualTo:
                    return "=";
                case RestrictionType.LessThan:
                    return "<=";
                case RestrictionType.MoreThan:
                    return ">=";
                default:
                    return "";
            }
        }

        private int TextBoxFilledCount()
        {
            int count = 0;
            foreach (var control in Controls)
                if (control.GetType() == typeof(TextBox))
                    if (((TextBox)control).Text.Length > 0)
                        count++;

            return count;
        }

        private bool RestrictionValueFilled()
        {
            return Controls["txtCond"]?.Text.Length > 0;
        }

        private void restrictionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnableStatus();
        }
        
        private void UpdateButtonsEnableStatus()
        {
            // Caso esvazie a lista de variáveis, desabilita botão para ir para o próximo passo
            if (_problemRestrictions.Count == 0 || restrictionList.SelectedIndex == -1)
            {
                btnRemove.Enabled = false;
                btnEdit.Enabled = false;
            }
            // Caso esteja selecionado na lista algum valor, habilita os botões
            if (restrictionList.SelectedIndex != -1)
            {
                btnRemove.Enabled = true;
                btnEdit.Enabled = true;
            }

            // Verifica se a lista de variáveis tem valores o suficiente para ir para o próximo passo
            btnNext.Enabled = _problemRestrictions.Count >= 2;

            // Verifica se é possível adicionar o valor a lista
            var textsFilles = TextBoxFilledCount();
            btnAdd.Enabled = RestrictionValueFilled() && textsFilles >= 2;
        }
    }
}
