using System;
using System.Drawing;
using OSC.Problem_Classes;
using System.Windows.Forms;
using OSC.Classes;

namespace OSC
{
    public partial class SimplexText : Form
    {
        private readonly SimplexMethod _simplexMethodClass;

        public SimplexText(SimplexMethod simplexMethodClass)
        {
            InitializeComponent();
            _simplexMethodClass = simplexMethodClass;
            var location = _simplexMethodClass.Stage * _simplexMethodClass.Stage + _simplexMethodClass.Step * 10;
            Location = new Point(location, location);
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            _simplexMethodClass.NextStep();
        }

        private void SimplexStep0_Load(object sender, EventArgs e)
        {
            if (_simplexMethodClass.Step-1 == -1)
                ShowStep01();
            else if (_simplexMethodClass.Step-1 == 0)
                ShowStep0();

            txtSimplex.Select(0, 0);
        }

        private void ShowStep01()
        {
            // Cria um texto com a função e restrições ajustadas em um formato legível
            var functionString = @"MIN Z = 0 - ( ";
            foreach (VariableData variable in _simplexMethodClass.SimplexData.Problem.Variables)
            {
                var varValue = variable.FunctionValue;
                functionString += varValue.GetString() + variable.Value;
            }
            functionString += " )";

            if (Size.Width < TextRenderer.MeasureText(functionString, Font).Width)
                Size = new Size(TextRenderer.MeasureText(functionString, Font).Width + 30, Height);

            txtSimplex.Text += functionString + Environment.NewLine + Environment.NewLine;
            for (int i = 0; i < _simplexMethodClass.SimplexData.Problem.Restrictions.Count; i++)
            {
                var restricionString = @"(Restrição " + (i + 1) + @"): ";
                restricionString += _simplexMethodClass.SimplexData.Problem.Restrictions[i].GetSimplexRestrictionString();
                if (Size.Width < TextRenderer.MeasureText(restricionString, Font).Width)
                    Size = new Size(TextRenderer.MeasureText(restricionString, Font).Width + 30, Height);
                txtSimplex.Text += restricionString + Environment.NewLine;
            }

        }

        private void ShowStep0()
        {
            // Cria um texto com a função e restrições ajustadas em um formato legível
            var functionString = @"MIN Z = 0 - (";
            foreach (VariableData variable in _simplexMethodClass.SimplexData.Problem.Variables)
            {
                var varValue = variable.FunctionValue;
                functionString += varValue.GetString() + variable.Value;
            }
            functionString += " )";

            if (Size.Width < TextRenderer.MeasureText(functionString, Font).Width)
                Size = new Size(TextRenderer.MeasureText(functionString, Font).Width + 30, Height);

            txtSimplex.Text += functionString + Environment.NewLine + Environment.NewLine;
            foreach (RestrictionFunctionData restr in _simplexMethodClass.SimplexData.Problem.Restrictions)
            {
                var restricionString = restr.GetSimplexFreeMemberString();
                if (Size.Width < TextRenderer.MeasureText(restricionString, Font).Width)
                    Size = new Size(TextRenderer.MeasureText(restricionString, Font).Width + 30, Height);
                txtSimplex.Text += restricionString + Environment.NewLine;
            }
        }
    }
}
