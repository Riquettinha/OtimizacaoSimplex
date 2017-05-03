﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OSC.Classes;
using OSC.Problem_Classes;

namespace OSC
{
    public partial class StepByStepForm : Form
    {
        private SimplexMethod _simplexMethodClass;
        public StepByStepForm(SimplexMethod simplexMethodClass)
        {
            InitializeComponent();
            _simplexMethodClass = simplexMethodClass;
            this.Text = _simplexMethodClass.Stage + @"ª Etapa " + _simplexMethodClass.Step + @"º Passo";
        }

        public void ConvertGridToDataTable()
        {
            // Transforma o grid e cabeçalhos em um datagridview legível
            DataTable tbl = new DataTable();
            tbl.Columns.Add("MB / MNB");
            foreach (var ch in _simplexMethodClass.SimplexData.NonBasicVariables)
                tbl.Columns.Add(ch);

            for (int r = 0; r < _simplexMethodClass.SimplexData.BasicVariables.Length; r++)
            {
                DataRow row = tbl.NewRow();
                row["MB / MNB"] = _simplexMethodClass.SimplexData.BasicVariables[r];
                for (int c = 0; c < _simplexMethodClass.SimplexData.NonBasicVariables.Length; c++)
                {
                    if (_simplexMethodClass.SimplexData.GridArray[c, r] != null)
                    {
                        var tblCol = tbl.Columns[c+1];
                        row[tblCol.ColumnName] = Math.Round(_simplexMethodClass.SimplexData.GridArray[c, r].Superior, 4) + " / " +
                                                  Math.Round(_simplexMethodClass.SimplexData.GridArray[c, r].Inferior, 4);
                    }
                }
                tbl.Rows.Add(row);
            }

            gridView.DataSource = tbl;
        }

        private void SimplexStep1_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            txtSimplex.Text = "";
            gridView.DataSource = null;
            
            if (_simplexMethodClass.SimplexData.Status == SimplexStatus.Sucess)
            {
                btnNextStep.Visible = false;
                gridView.Dock = DockStyle.Fill;
                Helpers.ShowErrorMessage("Ponto ótimo encontrado!");
                ShowSucess();
            }
            else if (_simplexMethodClass.SimplexData.Status == SimplexStatus.Fail)
            {
                Helpers.ShowErrorMessage("Solução ótima inatingível!");
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
            // TODO: Preencher dados básicos do inicio e valores finais de cada variável.
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
                restricionString += _simplexMethodClass.SimplexData.Problem.Restrictions[i].GetSimplexRestrictionString();
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
                var restricionString = restr.GetSimplexFreeMemberString();
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

            this.Text = _simplexMethodClass.Stage + @"ª Etapa " + _simplexMethodClass.Step + @"º Passo";
            FillData();
        }
    }
}