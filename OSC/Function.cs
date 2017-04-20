using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace OSC
{
    public partial class Function : Form
    {
        readonly List<VariableData> _problemaVariables = new List<VariableData>();

        public Function(List<VariableData> problemaVariables)
        {
            InitializeComponent();
            _problemaVariables = problemaVariables;
        }

        private void Function_Load(object sender, EventArgs e)
        {
            // Load the variables controls.
            int locationX = 12;
            for (int i = 0; i < _problemaVariables.Count; i++)
            {
                AddNewVariableTextBoxAndLabel(i, ref locationX);
                AddPlusLabel(i, ref locationX);
            }
        }

        private void txtVar_TextChanged(object sender, EventArgs e)
        {
            if (CheckIfAllTextAreFilled() && (rdMinValue.Checked || rdMaxValue.Checked))
                btnNext.Enabled = true;
            else
                btnNext.Enabled = false;
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
            else if (!char.IsControl(e.KeyChar) && !CheckIfIsAValidDecimal(((TextBox) sender).Text + e.KeyChar))
            {
                Helpers.ShowErrorMessage("Valor inserido inválido.");
                e.Handled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CheckIfAllTextAreFilled() && rdMinValue.Checked || rdMaxValue.Checked)
            {
                for (int i = 0; i < _problemaVariables.Count; i++)
                {
                    var functionValue = Convert.ToDecimal(Controls["txtVar" + i].Text);
                    _problemaVariables[i].FunctionValue = functionValue;

                }

                var functionData = new FunctionData
                {
                    Maximiza = rdMaxValue.Checked,
                    Variables = _problemaVariables
                };

                var restrictions = new Restriction(functionData);
                restrictions.Show();
                Hide();
            }
            else
            {
                MessageBox.Show(@"Para prosseguir preencha todos os dados necessários.", @"Atenção!");
            }
        }

        private void rd_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckIfAllTextAreFilled() && (rdMinValue.Checked || rdMaxValue.Checked))
                btnNext.Enabled = true;
            else
                btnNext.Enabled = false;
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
                var aux = value.ConvertToDecimal();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddNewVariableTextBoxAndLabel(int index, ref int locationX)
        {
            var txtVar = new TextBox
            {
                Name = "txtVar" + index,
                Location = new Point(locationX, 41),
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
                Location = new Point(locationX, 43),
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
                Location = new Point(locationX, 43),
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
                    Size = new Size(locationX + 30, Size.Height);
            }
        }

        private void Function_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            var userDecision =
                MessageBox.Show(@"Se você voltar para tela anterior perderá todas as alterações "
                + @"realizadas nesta tela, deseja voltar para tela anteiror mesmo assim?",
                    @"Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (userDecision == DialogResult.Yes)
            {
                Application.OpenForms["Variables"].Show();
                Hide();
            }
        }
    }
}
