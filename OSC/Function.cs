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
    public partial class Function : Form
    {
        List<VariableData> variables = new List<VariableData>();

        public Function(List<VariableData> variables)
        {
            InitializeComponent();
            this.variables = variables;
        }

        private void AddNewVariableTextBoxAndLabel(int index, ref int locationX)
        {
            var txtVar = new TextBox
            {
                Name = "txtVar" + index,
                Location = new Point(locationX, 38),
                Size = new Size(60, 20)
            };

            Controls.Add(txtVar);
            locationX += 60;
            
            var lbVar = new Label
            {
                Name = "lbVar" + index,
                Location = new Point(locationX, 40),
                Size = TextRenderer.MeasureText(variables[index].Value, Font),
                Text = variables[index].Value,
                BackColor = Color.Transparent
            };
            
            Controls.Add(lbVar);
            locationX += TextRenderer.MeasureText(variables[index].Value, Font).Width-1;
        }

        private void AddPlusLabel(int index, ref int locationX)
        {
            var plus = new Label
            {
                Location = new Point(locationX, 40),
                Size = TextRenderer.MeasureText("+", Font),
                Text = @"+",
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(45, 55, 175),
                Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold)
            };
        
            if (index + 1 < variables.Count)
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

        private void Function_Load(object sender, EventArgs e)
        {
            int locationX = 12;
            for (int i = 0; i < variables.Count; i++)
            {
                AddNewVariableTextBoxAndLabel(i, ref locationX);
                AddPlusLabel(i, ref locationX);
            }
        }

        private void Function_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }
    }
}
