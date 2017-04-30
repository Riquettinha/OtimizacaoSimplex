using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using OSC.Problem_Classes;
using System.Windows.Forms;
using OSC.Classes;

namespace OSC
{
    public partial class SimplexStep0 : Form
    {
        private ProblemData _problem;
        private int _step;
        public SimplexStep0(ProblemData problem, int step)
        {
            InitializeComponent();
            _problem = problem;
            _step = step;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Helpers.BackForm())
            {
                Application.OpenForms["SimplexMain"].Show();
                Hide();
            }
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            var aux = new SimplexMethod(_problem, ++_step);
        }

        private void SimplexStep0_Load(object sender, EventArgs e)
        {
            if (_step == -1)
                ShowStep01();
            else if (_step == 0)
                ShowStep0();
        }

        private void ShowStep01()
        {
            // Cria um texto com a função e restrições ajustadas
            var functionString = @"MIN Z = 0";
            foreach (VariableData variable in _problem.Variables)
            {
                var varValue = variable.FunctionValue;
                functionString += varValue.GetString() + variable.Value;
            }

            if (Size.Width < TextRenderer.MeasureText(functionString, Font).Width)
                Size = new Size(TextRenderer.MeasureText(functionString, Font).Width + 30, Height);

            txtSimplex.Text += functionString + Environment.NewLine + Environment.NewLine;
            for (int i = 0; i < _problem.Restrictions.Count; i++)
            {
                var restricionString = @"(Restrição " + (i + 1) + @"): ";
                restricionString += _problem.Restrictions[i].GetSimplexRestrictionString();
                if (Size.Width < TextRenderer.MeasureText(restricionString, Font).Width)
                    Size = new Size(TextRenderer.MeasureText(restricionString, Font).Width + 30, Height);
                txtSimplex.Text += restricionString + Environment.NewLine;
            }
        }

        private void ShowStep0()
        {
            // Cria um texto com a função e restrições ajustadas
            var functionString = @"MIN Z = 0";
            foreach (VariableData variable in _problem.Variables)
            {
                var varValue = variable.FunctionValue;
                functionString += varValue.GetString() + variable.Value + @" ";
            }

            if (Size.Width < TextRenderer.MeasureText(functionString, Font).Width)
                Size = new Size(TextRenderer.MeasureText(functionString, Font).Width + 30, Height);

            txtSimplex.Text += functionString + Environment.NewLine + Environment.NewLine;
            foreach (RestrictionFunctionData restr in _problem.Restrictions)
            {
                var restricionString = restr.GetSimplexFreeMemberString();
                if (Size.Width < TextRenderer.MeasureText(restricionString, Font).Width)
                    Size = new Size(TextRenderer.MeasureText(restricionString, Font).Width + 30, Height);
                txtSimplex.Text += restricionString + Environment.NewLine;
            }
        }
    }
}
