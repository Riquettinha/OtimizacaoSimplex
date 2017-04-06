using System;
using System.Collections.Generic;
using System.Drawing;
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

        private void AddNewVariableTextBoxAndLabel(int index, ref int locationX)
        {
            var txtVar = new TextBox
            {
                Name = "txtVar" + index,
                Location = new Point(locationX, 41),
                Size = new Size(60, 20)
            };

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
            locationX += TextRenderer.MeasureText(_problemaVariables[index].Value, Font).Width-1;
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

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void Function_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the entire project.
            Environment.Exit(0);
        }
    }
}
