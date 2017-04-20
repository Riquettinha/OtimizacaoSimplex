using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSC
{
    public partial class Restriction : Form
    {
        readonly List<VariableData> _problemaVariables = new List<VariableData>();

        public Restriction(FunctionData functionData)
        {
            InitializeComponent();
            _problemaVariables = functionData.Variables;
        }

        private void Restriction_Load(object sender, EventArgs e)
        {
            int locationX = 12;
            for (int i = 0; i < _problemaVariables.Count; i++)
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
                Size = TextRenderer.MeasureText(_problemaVariables[index].Value, Font),
                Text = _problemaVariables[index].Value,
                BackColor = Color.Transparent
            };

            Controls.Add(lbVar);
            locationX += TextRenderer.MeasureText(_problemaVariables[index].Value, Font).Width - 1;
        }

        private void AddPlusLabel(int index, ref int locationX)
        {
            var plus = new Label
            {
                Location = new Point(locationX, 173),
                Size = TextRenderer.MeasureText("+", Font),
                Text = @"+",
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(45, 55, 175),
                Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold)
            };

            if (index + 1 < _problemaVariables.Count)
            {
                Controls.Add(plus);
                locationX += TextRenderer.MeasureText("+", Font).Width;
            }
            else
            {
                if (locationX > Size.Width)
                    Size = new Size(locationX + 173, Size.Height);
                locationX += 10;
            }
        }

        private void AddCondictionComboBoxAndTextBox(int locationX)
        {
            var condiction = new ComboBox
            {
                Location = new Point(locationX, 173),
                Size = new Size(30, 20),
                ForeColor = Color.FromArgb(45, 55, 175),
                Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold)
            };

            condiction.Items.Add(RestrictionType.LessThan);
            condiction.Items.Add(RestrictionType.MoreThan);
            condiction.Items.Add(RestrictionType.EqualTo);

            condiction.SelectedIndexChanged += condiction_SelectedIndexChanged;
            Controls.Add(condiction);

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
        }

        private void condiction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckIfAllTextAreFilled())
            {
                
            }
        }

        private void txtVar_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
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

        private bool CheckIfAllTextAreFilled()
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    if (((TextBox)control).Text.Length == 0)
                        return false;
                }
            }
            return true;
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
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

        private void Restriction_FormClosing(object sender, FormClosingEventArgs e)
        {
            var userDecision =
                MessageBox.Show(@"O cálculo ainda não foi finalizado, deseja fechar o programa mesmo assim?",
                    @"Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (userDecision == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }
    }
}
