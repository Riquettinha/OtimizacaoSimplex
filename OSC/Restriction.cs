using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSC.Classes;

namespace OSC
{
    public partial class Restriction : Form
    {
        readonly List<VariableData> _problemVariables = new List<VariableData>();
        readonly List<RestrictionFunctionData> _problemRestrictions = new List<RestrictionFunctionData>();

        public Restriction(FunctionData functionData)
        {
            InitializeComponent();
            _problemVariables = functionData.Variables;

            LoadExtraFields();
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
            var txtVar = new TextBox
            {
                Name = "txtVar" + index,
                Location = new Point(locationX, 173),
                Size = new Size(60, 20)
            };
            // Validate the input
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
            // Create condiction type ComboBox
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

            condiction.SelectedIndexChanged += condiction_SelectedIndexChanged;
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
            var cbSource = new List<ComboBoxItem>
            {
                new ComboBoxItem
                {
                    Text = GetRestrictionString(RestrictionType.LessThan),
                    Value = RestrictionType.LessThan
                },
                new ComboBoxItem
                {
                    Text = GetRestrictionString(RestrictionType.MoreThan),
                    Value = RestrictionType.MoreThan
                }
            };
            condiction.DataSource = cbSource;
            condiction.DisplayMember = "Text";
            condiction.ValueMember = "Value";
            condiction.SelectedValue = RestrictionType.MoreThan;
        }

        public void ResizeControls(int locationX)
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

        private void condiction_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtVar_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = CheckEveryThingIsFilled();
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
            else if (!char.IsControl(e.KeyChar) && !CheckIfIsAValidDecimal(((TextBox)sender).Text + e.KeyChar))
            {
                Helpers.ShowErrorMessage("Valor inserido inválido.");
                e.Handled = true;
            }
        }

        public bool CheckIfIsAValidDecimal(string value)
        {
            try
            {
                // Testa se é possível converter para decimal
                value.ConvertToDecimal();
                return true;
            }
            catch (Exception)
            {
                return false;
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

            var listRestricitonData = new List<RestrictionData>();
            for (int i = 0; i < _problemVariables.Count; i++)
            {
                var newRestritcionData = new RestrictionData
                {
                    RestrictionValue = Controls["txtVar" + i].Text.ConvertToDecimal(),
                    RestrictionVariable = _problemVariables[i]
                };
                listRestricitonData.Add(newRestritcionData);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckEveryThingIsFilled())
            {
                var newRestr = CreateRestrictionObject();
                if (CheckIfIsNewRestriction(newRestr))
                {
                    restrictionList.Items.Add(CreateRestrictionString(newRestr));
                    _problemRestrictions.Add(newRestr);
                }
                else
                {
                    Helpers.ShowErrorMessage(
                    "Já existe uma restrição com estes mesmos dados!");
                }
            }
            else
            {
                Helpers.ShowErrorMessage(
                    "Para adicionar uma função de restrição é necessário que todos os valores e o tipo de restrição sejam preenchidos.");
            }
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _problemRestrictions.RemoveAt(restrictionList.SelectedIndex);
            restrictionList.Items.RemoveAt(restrictionList.SelectedIndex);
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            var userDecision =
                MessageBox.Show(@"Se você voltar para tela anterior perderá todas as alterações "
                + @"realizadas nesta tela, deseja voltar para tela anteiror mesmo assim?",
                    @"Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (userDecision == DialogResult.Yes)
            {
                Application.OpenForms["Function"].Show();
                Hide();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private bool CheckEveryThingIsFilled()
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    if (((TextBox)control).Text.Length == 0)
                        return false;
                }
            }
            if ((Controls["cbCondiction"] as ComboBox).SelectedIndex == -1)
                return false;

            return true;
        }

        private void restrictionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = restrictionList.SelectedIndex != -1;
            btnRemove.Enabled = restrictionList.SelectedIndex != -1;
        }
    }
}
