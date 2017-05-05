using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OSC.Classes;
using OSC.Problem_Classes;
using OSC.SimplexApi;

namespace OSC
{
    public partial class StepByStepForm : Form
    {
        private SimplexMethod _simplexMethodClass;
        public StepByStepForm(SimplexMethod simplexMethodClass)
        {
            InitializeComponent();
            _simplexMethodClass = simplexMethodClass;
        }

        private void SimplexStep_Load(object sender, EventArgs e)
        {
            FillData();
        }

        public void ConvertGridToDataTable()
        {
            // Transforma o grid e cabeçalhos em um datagridview legível
            DataTable tbl = new DataTable();
            tbl.Columns.Add("MB / MNB");
            foreach (var ch in _simplexMethodClass.SimplexData.NonBasicVariables)
                tbl.Columns.Add(ch);

            for (int r = 0; r < _simplexMethodClass.SimplexData.BasicVariables.Count; r++)
            {
                DataRow row = tbl.NewRow();
                row["MB / MNB"] = _simplexMethodClass.SimplexData.BasicVariables[r];
                for (int c = 0; c < _simplexMethodClass.SimplexData.NonBasicVariables.Count; c++)
                {
                    if (_simplexMethodClass.SimplexData.GridArray[c][r] != null)
                    {
                        var tblCol = tbl.Columns[c+1];
                        row[tblCol.ColumnName] = Math.Round(_simplexMethodClass.SimplexData.GridArray[c][r].Superior, 4) + " / " +
                                                  Math.Round(_simplexMethodClass.SimplexData.GridArray[c][r].Inferior, 4);
                    }
                }
                tbl.Rows.Add(row);
            }

            gridView.DataSource = tbl;
        }

        private void FillData()
        {
            txtSimplex.Text = "";
            gridView.DataSource = null;
            
            if (_simplexMethodClass.SimplexData.Status == SimplexStatus.Sucess)
            {
                btnNextStep.Visible = false;
                gridView.Dock = DockStyle.Fill;
                ShowSucess();
                Helpers.ShowErrorMessage("Ponto ótimo encontrado!");
            }
            else if (_simplexMethodClass.SimplexData.Status == SimplexStatus.Impossible)
            {
                Helpers.ShowErrorMessage("Solução ótima impossível de ser atingida!");
            }
            else if (_simplexMethodClass.SimplexData.Status == SimplexStatus.Infinite)
            {
                Helpers.ShowErrorMessage("Solução infinita!");
            }
            else
            {
                if (_simplexMethodClass.Step - 1 == -1)
                    ShowStep01();
                else if (_simplexMethodClass.Step - 1 == 0)
                    ShowStep0();
                else
                    ShowGridSteps();
            }

            UpdateStepsLabel();
        }

        private void UpdateStepsLabel()
        {
            if(_simplexMethodClass.SimplexData.Status == SimplexStatus.Sucess)
                lbStep.Text = @"Ponto ótimo encontrado.";
            else if (_simplexMethodClass.Step - 1 == -1)
                lbStep.Text = @"Adicionada variável de folga.";
            else if (_simplexMethodClass.Step - 1 == 0)
                lbStep.Text = @"Claculado elementos livres.";
            else if (_simplexMethodClass.Step - 1 == 1)
                lbStep.Text = @"Preenchidas células superiores e verifica pelo fim da etapa.";
            else if (_simplexMethodClass.Step - 1 == 2)
                lbStep.Text = @"Encontrada coluna selecionada.";
            else if (_simplexMethodClass.Step - 1 == 3)
                lbStep.Text = @"Encontrada linha selecionada.";
            else if (_simplexMethodClass.Step - 1 == 4)
                lbStep.Text = @"Preenchidas células inferiores.";
            else if (_simplexMethodClass.Step - 1 == 5)
                lbStep.Text = @"Troca variável básica com não básica";
            else if (_simplexMethodClass.Step - 1 == 5)
                lbStep.Text = @"Troca variável básica com não básica";
        }

        private void ShowSucess()
        {
            txtSimplex.Visible = true;
            txtSimplex.BringToFront();
            gridView.Visible = false;

            var problem = _simplexMethodClass.SimplexData.Problem;
            var finalString = string.Concat("Z = ", problem.Function.FinalValue.ToString("0.###"),
                Environment.NewLine, Environment.NewLine);
            foreach (VariableData data in problem.Variables)
                finalString += string.Concat(data.Value, " = ", data.FinalValue.ToString("0.###"), Environment.NewLine);

            finalString += Environment.NewLine;
            for (int i = 0; i < problem.Restrictions.Count; i++)
            {
                var restriction = problem.Restrictions[i];
                finalString += string.Concat("(Restrição ", (i + 1).ToString(), ") = ", restriction.RestrictionFinalSum.ToString("0.###"), Environment.NewLine);
                finalString += string.Concat(restriction.RestrictionLeftOver.LeftOverVariable.Value, " = ", restriction.RestrictionLeftOver.LeftOverVariable.FinalValue.ToString("0.###").Replace("-", ""), Environment.NewLine);
            }

            txtSimplex.Text = finalString;
        }

        private void ShowStep01()
        {
            txtSimplex.Visible = true;
            txtSimplex.BringToFront();
            gridView.Visible = false;
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
                restricionString += RestrictionFunctionDataHelper.GetSimplexRestrictionString(_simplexMethodClass.SimplexData.Problem.Restrictions[i]);
                if (Size.Width < TextRenderer.MeasureText(restricionString, Font).Width)
                    Size = new Size(TextRenderer.MeasureText(restricionString, Font).Width + 30, Height);
                txtSimplex.Text += restricionString + Environment.NewLine;
            }

        }

        private void ShowStep0()
        {
            txtSimplex.Visible = true;
            txtSimplex.BringToFront();
            gridView.Visible = false;
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
                var restricionString = RestrictionFunctionDataHelper.GetSimplexFreeMemberString(restr);
                if (Size.Width < TextRenderer.MeasureText(restricionString, Font).Width)
                    Size = new Size(TextRenderer.MeasureText(restricionString, Font).Width + 30, Height);
                txtSimplex.Text += restricionString + Environment.NewLine;
            }
        }

        private void ShowGridSteps()
        {
            txtSimplex.Visible = false;
            gridView.Visible = true;
            gridView.BringToFront();

            ConvertGridToDataTable();

            MinimumSize = new Size(Width, Height);
            MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            gridView.MinimumSize = new Size(Width, Height);
            gridView.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            
            gridView.AutoSize = true;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            _simplexMethodClass.NextStep();
        }

        public void Restart(SimplexMethod simplexMethodClass)
        {
            _simplexMethodClass = simplexMethodClass;
            
            FillData();
        }

        private void StepByStepForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _simplexMethodClass.SimplexData = new SimplexData
            {
                Problem = _simplexMethodClass.SimplexData.Problem,
                Status = SimplexStatus.Pending,
                AllowedColumn = 0,
                AllowedRow = 0,
                BasicVariables = new ArrayOfString(),
                NonBasicVariables = new ArrayOfString(),
                GridArray = new List<List<GridCell>>()
            };

            _simplexMethodClass.Stage = 1;
            _simplexMethodClass.Step = -1;
        }
    }
}
