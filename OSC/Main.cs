using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OSC.Classes;
using OSC.SimplexApi;

namespace OSC
{
    public partial class Main : Form
    {
        private ProblemData _problem;
        public Main()
        {
            InitializeComponent();
            _problem = Create.ProblemData();
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            ofdCsv.ShowDialog();
        }

        private void ofdCsv_FileOk(object sender, CancelEventArgs e)
        {
            txtFile.Text = ofdCsv.FileName;
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            new Variables().Show();
            Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                ofdCsv.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Desktop";
            }
            catch (Exception)
            {
                // Ignora e deixa iniciar na pasta padrão do sistema
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            
            if (File.Exists(txtFile.Text))
            {
                try
                {
                    var sr = new StreamReader(txtFile.Text);
                    _problem = Create.ProblemData();
                    // Cria lista de dados importados
                    List<string[]> lines = new List<string[]>();
                    string line;
                    while((line = sr.ReadLine()) != null)
                        lines.Add(line.Split(';'));

                    if (GetVariables(lines))
                    {
                        if (GetFunctionType(lines[0]))
                        {
                            if (GetRestrictions(lines))
                            {
                                var simplex = new SimplexMain(_problem);
                                simplex.Show();
                                Hide();
                            }
                            else
                            {
                                Helpers.ShowErrorMessage("Restrição com valor inválido.");
                            }
                        }
                        else
                        {
                            Helpers.ShowErrorMessage("Tipo da função não definido da forma correta, " +
                                                     "insira \"+\" última coluna da primeira lina para maximização e " +
                                                     "\"-\" na última coluna da primeira linha para minização.");
                        }
                    }
                    else
                    {
                        Helpers.ShowErrorMessage("Valor inválido inserido com número");
                    }
                }
                catch (Exception)
                {
                    Helpers.ShowErrorMessage("Arquivo sendo utilizado ou em formato inválido, verifique o formato e feche ele caso esteja abaerto.");
                }
            }
            else
                Helpers.ShowErrorMessage("Arquivo inexistente, verifique o caminho.");
        }

        private bool GetFunctionType(string[] line)
        {
            if (line[line.Length - 2].Equals("-"))
                _problem.Function = Create.FunctionData(false);
            else if (line[line.Length - 2].Equals("+"))
                _problem.Function = Create.FunctionData(true);
            else
                return false;
            return true;
        }

        private bool GetVariables(List<string[]> lines)
        {
            for (int i = 0; i < lines[0].Length - 2; i++)
            {
                var value = lines[0][i];
                var variableData = new VariableData
                {
                    Description = Helpers.GetExcelColumnName(i + 1).ToLower(),
                    Value = Helpers.GetExcelColumnName(i + 1).ToLower(),
                };
                if (Helpers.CheckIfIsAValidDecimal(value))
                {
                    variableData.FunctionValue = value.ConvertToDecimal();
                    _problem.Variables.Add(variableData);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private bool GetRestrictions(List<string[]> lines)
        {
            for (int lineIndex = 1; lineIndex < lines.Count; lineIndex++)
            {
                var line = lines[lineIndex];

                var value = line[line.Length-1];
                var type = GetValidRestriction(line[line.Length - 2]);

                if (!Helpers.CheckIfIsAValidDecimal(value) || type == RestrictionType.Default)
                    return false;

                var restr = Create.RestrictionFunctionData();
                restr.RestrictionType = type;
                restr.RestrictionValue = Convert.ToDecimal(value);

                for (int columnIndex = 0; columnIndex < line.Length-2; columnIndex++)
                {
                    var variableValue = line[columnIndex];
                    var variable =
                        _problem.Variables.FirstOrDefault(v => v.Value == Helpers.GetExcelColumnName(columnIndex + 1).ToLower());
                    if (Helpers.CheckIfIsAValidDecimal(variableValue) && variable != null)
                    {
                        restr.RestrictionData.Add(new RestrictionVariableData
                        {
                            RestrictionValue = Convert.ToDecimal(variableValue),
                            RestrictionVariable = variable
                        });
                    }
                    else
                        return false;
                }

                _problem.Restrictions.Add(restr);
            }

            return true;
        }

        private RestrictionType GetValidRestriction(string value)
        {
            if (value == "<")
                return RestrictionType.LessThan;
            if(value == ">")
                return RestrictionType.MoreThan;
            if(value == "=")
                return RestrictionType.MoreThan;
            return RestrictionType.Default;
        }
    }
}
