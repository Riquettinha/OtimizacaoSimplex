using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OSC.Classes;
using OSC.Problem_Classes;

namespace OSC
{
    public partial class Restriction : Form
    {
        readonly ProblemData _problem = new ProblemData();

        public Restriction(ProblemData problem)
        {
            InitializeComponent();
            _problem = problem;

            LoadExtraFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (GetFilledTextBoxesCount() > 1 && NeededValueIsFilled())
            {
                var newRestr = CreateRestrictionObject();
                if (newRestr.CheckIfIsNewRestriction(_problem.Restrictions))
                {
                    restrictionList.Items.Add(newRestr.GetRestrictionString());
                    _problem.Restrictions.Add(newRestr);
                    Helpers.ClearFormValues(this);
                    UpdateButtonsEnableStatus();
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
            _problem.Restrictions.RemoveAt(restrictionList.SelectedIndex);
            restrictionList.Items.RemoveAt(restrictionList.SelectedIndex);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var restriction = _problem.Restrictions[restrictionList.SelectedIndex];
            for (int i = 0; i < restriction.RestrictionData.Count; i++)
                Controls["txtVar" + i].Text = restriction.RestrictionData[i].RestrictionValue.ToString();

            Controls["txtCond"].Text = restriction.RestrictionValue.ToString();
            (Controls["cbCondiction"] as ComboBox).SelectedValue = restriction.RestrictionType;

            _problem.Restrictions.RemoveAt(restrictionList.SelectedIndex);
            restrictionList.Items.RemoveAt(restrictionList.SelectedIndex);
        }

        private void txtVar_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnableStatus();
        }
        
        private void cbCondiction_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            var simplexForm = new SimplexMain(_problem);
            simplexForm.Show();
            Hide();
        }

        private void LoadExtraFields()
        {
            int locationX = 12;
            for (int i = 0; i < _problem.Variables.Count; i++)
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
                Size = new Size(60, 20),
                TabIndex = 3 + index
            };
            
            txtVar.KeyPress += txtVar_KeyPress;
            txtVar.TextChanged += txtVar_TextChanged;

            Controls.Add(txtVar);
            locationX += 60;

            var lbVar = new Label
            {
                Name = "lbVar" + index,
                Location = new Point(locationX, 175),
                Size = TextRenderer.MeasureText(_problem.Variables[index].Value, Font),
                Text = _problem.Variables[index].Value,
                BackColor = Color.Transparent
            };

            Controls.Add(lbVar);
            locationX += TextRenderer.MeasureText(_problem.Variables[index].Value, Font).Width - 1;
        }

        private void AddPlusLabel(int index, ref int locationX)
        {
            if (index + 1 < _problem.Variables.Count)
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
                TabIndex = 1000,
                AutoCompleteMode = AutoCompleteMode.None
            };
            condiction.SelectedIndexChanged += cbCondiction_SelectedIndexChanged;
            
            FillComboBoxItems(condiction);
            
            Controls.Add(condiction);
            locationX += 45;

            // Create condiction value TextBox
            var txtVar = new TextBox
            {
                Name = "txtCond",
                Location = new Point(locationX, 173),
                Size = new Size(60, 20),
                TabIndex = 1001
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

            cbSource.Add(new ComboBoxItem
            {
                Text = Helpers.GetRestrictionTypeString(RestrictionType.LessThan),
                Value = RestrictionType.LessThan
            });
            cbSource.Add(new ComboBoxItem
            {
                Text = Helpers.GetRestrictionTypeString(RestrictionType.MoreThan),
                Value = RestrictionType.MoreThan
            });
            condiction.DataSource = cbSource;
            condiction.DisplayMember = "Text";
            condiction.ValueMember = "Value";
            condiction.SelectedValue = RestrictionType.LessThan;

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

        private RestrictionFunctionData CreateRestrictionObject()
        {
            // Transforma os valores preenchidos na tela em um objeto do tipo RestrictionFunctionData
            var listRestricitonData = new List<RestrictionVariableData>();
            for (int i = 0; i < _problem.Variables.Count; i++)
            {
                if (!String.IsNullOrEmpty(Controls["txtVar" + i].Text))
                {
                    var newRestritcionData = new RestrictionVariableData
                    {
                        RestrictionValue = Controls["txtVar" + i].Text.ConvertToDecimal(),
                        RestrictionVariable = _problem.Variables[i]
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

        private int GetFilledTextBoxesCount()
        {
            int count = 0;
            foreach (var control in Controls)
                if (control.GetType() == typeof(TextBox))
                    if (((TextBox)control).Text.Length > 0)
                        count++;

            return count;
        }

        private bool NeededValueIsFilled()
        {
            return Controls["txtCond"]?.Text.Length > 0 && (Controls["cbCondiction"] as ComboBox).SelectedIndex != -1;
        }

        private void restrictionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnableStatus();
        }

        private void UpdateButtonsEnableStatus()
        {
            // Caso esvazie a lista de variáveis, desabilita botão para ir para o próximo passo
            if (_problem.Restrictions.Count == 0 || restrictionList.SelectedIndex == -1)
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
            btnNext.Enabled = _problem.Restrictions.Count >= 2;

            // Verifica se é possível adicionar o valor a lista
            var textsFilles = GetFilledTextBoxesCount();
            btnAdd.Enabled = NeededValueIsFilled() && textsFilles >= 2;
        }
    }
}
