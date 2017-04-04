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

        private void Function_Load(object sender, EventArgs e)
        {
            int locationX = 12;
            for (int i = 0; i < variables.Count; i++)
            {
                var txtVar = new TextBox
                {
                    Name = "txtVar" + i,
                    Location = new Point(locationX, 35),
                    Size = new Size(50, 20)
                };
                txtVar.BringToFront();

                Controls.Add(txtVar);
                locationX += 55;

                var lbVar = new Label
                {
                    Name = "lbVar" + i,
                    Location = new Point(locationX, 37),
                    Size = TextRenderer.MeasureText(variables[i].Value, Font),
                    Text = variables[i].Value
                };

                lbVar.BringToFront();
                Controls.Add(lbVar);
                locationX += TextRenderer.MeasureText(variables[i].Value, Font).Width-1;

                var plus = new Label
                {
                    Location = new Point(locationX, 37),
                    Size = TextRenderer.MeasureText("+", Font),
                    Text = @"+"
                    //Text = i + 1 < variables.Count ? @"+" : maxValue.Checked ? ">" : "<"
                };
                plus.ForeColor = Color.Gray;
                plus.BringToFront();
                locationX += 13;

                if (i + 1 < variables.Count)
                    Controls.Add(plus);
                //    plus.Name = "end";
            }
            
            if(locationX > Size.Width)
            this.Size = new Size(locationX + 80, Size.Height);

            //var txtFinal = new TextBox
            //{
            //    Location = new Point(locationX, 35),
            //    Size = new Size(50, 20)
            //};
            //txtFinal.BringToFront();
            //Controls.Add(txtFinal);
        }

        private void Function_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
