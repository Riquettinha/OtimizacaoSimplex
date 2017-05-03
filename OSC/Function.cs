using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OSC.Classes;
using OSC.Problem_Classes;

namespace OSC
{
    public partial class Function : Form
    {
        readonly ProblemData _problem;

        public Function(ProblemData problem)
        {
            InitializeComponent();
            _problem = problem;
            LoadExtraFields();
        }

        private void LoadExtraFields()
        {
            // Carrega uma textbox e uma label para cada variável e depois redefine tamanho da tela.
            int locationX = 12;
            for (int i = 0; i < _problem.Variables.Count; i++)
            {
                AddNewVariableTextBoxAndLabel(i, ref locationX);
                AddPlusLabelOrResizeForm(i, ref locationX);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _problem.Variables.Count; i++)
            {
                var functionValue = Convert.ToDecimal(Controls["txtVar" + i].Text);
                _problem.Variables[i].FunctionValue = functionValue;
            }
            
            _problem.Function = new FunctionData
            {
                Maximiza = rdMaxValue.Checked
            };

            var restrictions = new Restriction(_problem);
            restrictions.Show();
            Hide();
        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Helpers.BackForm())
            {
                _problem.Function = null;
                Application.OpenForms["Variables"].Show();
                Hide();
            }
        }

        private void txtVar_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnableStatus();
        }

        private void txtVar_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Valida se o valor preenchido é um decimal válido (apenas números e uma única vírgula)
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

        private void rd_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtonsEnableStatus();
        }

        private void UpdateButtonsEnableStatus()
        {
            // Verifica se todos os campos necessários foram preenchidos,
            // para então permitir ou não ir para o próximo passo
            btnNext.Enabled = Helpers.CheckIfAllTextAreFilled(Controls) && (rdMinValue.Checked || rdMaxValue.Checked);
        }

        private void AddNewVariableTextBoxAndLabel(int index, ref int locationX)
        {
            var txtVar = new TextBox
            {
                Name = "txtVar" + index,
                Location = new Point(locationX, 41),
                Size = new Size(60, 20),
                TabIndex = 2 + index
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
                Size = TextRenderer.MeasureText(_problem.Variables[index].Value, Font),
                Text = _problem.Variables[index].Value,
                BackColor = Color.Transparent
            };

            Controls.Add(lbVar);
            locationX += TextRenderer.MeasureText(_problem.Variables[index].Value, Font).Width - 1;
        }

        private void AddPlusLabelOrResizeForm(int index, ref int locationX)
        {
            if (index + 1 < _problem.Variables.Count)
            {
                // Add plus label

                var plus = new Label
                {
                    Location = new Point(locationX, 43),
                    Size = TextRenderer.MeasureText("+", Font),
                    Text = @"+",
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(45, 55, 175),
                    Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold)
                };

                Controls.Add(plus);
                locationX += TextRenderer.MeasureText("+", Font).Width;
            }
            else
            {
                // Resize form
                if (locationX + 30 > Size.Width)
                    Size = new Size(locationX + 26, Size.Height);
            }
        }
    }
}
