using System.Collections.Generic;
using System.Windows.Forms;
using OSC.SimplexApi;

namespace OSC.Classes
{
    public class SimplexMethod
    {
        public SimplexData SimplexData;
        public int Stage = 1;
        public int Step = -1;

        public SimplexMethod(ProblemData problem, bool stepbystep)
        {
            SimplexData = Create.SimplexData();
            SimplexData.Problem = problem;
            SimplexData.Status = SimplexStatus.Pending;

            if (stepbystep)
                ExecuteSimplex();
            else
            {
                var simplexApi = new ServiceSoapClient();
                SimplexData = simplexApi.ExecuteSimplex(SimplexData);
                ShowStepForm();
            }
        }

        private void ExecuteSimplex()
        {
            var simplexApi = new ServiceSoapClient();
            if (Step == -1)
            {
                SimplexData = simplexApi.TransformFunction(SimplexData);
                SimplexData.Problem = simplexApi.CreateRestrictionLeftover(SimplexData.Problem);
                Step++;
            }
            else if (Step == 0)
            {
                SimplexData.Problem = simplexApi.IsolateTheLeftOver(SimplexData.Problem);
                Step++;

                // Monta um array na ordem das variáveis básicas e não básica
                // Preenche um grid com as informações corretas
                SimplexData.NonBasicVariables = GetColumnsHeaderArray();
                SimplexData.BasicVariables = GetRowsHeaderArray();
                SimplexData.GridArray = GetProblemSimplexGrid();
            }
            else if (Stage == 1 && Step == 1)
            {
                // Verifica se existe membro livre negativpo
                if (simplexApi.FirstStageCheckForTheEnd(SimplexData.GridArray) != 0)
                {
                    // Caso tenha vai para o próximo passo
                    Step++;
                }
                else
                {
                    // Caso não tenha, vai para o próximo estágio
                    Stage++;
                    Step = 1;
                }
            }
            else if (Stage == 1 && Step == 2)
            {
                // Pega primeira coluna com valor negativo
                SimplexData.AllowedColumn = simplexApi.FirstStageGetAllowedColumn(SimplexData.GridArray);
                if (SimplexData.AllowedColumn != 0)
                {
                    // Se coluna permitida existe vai para o próximo passo
                    Step++;
                }
                else
                {
                    // Se não tem, é um caso de região permissiva inexistente
                    SimplexData.Status = SimplexStatus.Impossible;
                }
            }
            else if (Stage == 1 && Step == 3)
            {
                // Pega linha com menor quocite do ML pela celula superior da coluna permitida
                SimplexData.AllowedRow = simplexApi.FirstStageGetAllowedRow(SimplexData);
                Step++;
            }
            else if (Step == 4)
            {
                // Preenche célular inferiores do grid
                SimplexData = simplexApi.FirstStageFillInferiorCells(SimplexData);
                Step++;
            }
            else if (Step == 5)
            {
                // Troca coluna da variáveis básicas com não básicas
                SimplexData = simplexApi.FirstStageUpdateHeaders(SimplexData);
                Step++;
            }
            else if (Step == 6)
            {
                // Preenche novamente células superiores e volta à verificação do primeiro passo
                SimplexData = simplexApi.FirstStageReposition(SimplexData);
                Step = 1;
                Stage = 1;
            }
            else if (Stage == 2 && Step == 1)
            {
                // Verifica se existe variável com valor de função positiva
                SimplexData.AllowedColumn = simplexApi.SecondStageGetAllowedColumn(SimplexData.GridArray);
                if (SimplexData.AllowedColumn != 0)
                {
                    // Caso tenha vai para o próximo passo
                    Step++;
                }
                else
                {
                    // Solução ÓTIMA encontrada
                    SimplexData.Status = SimplexStatus.Sucess;
                    SimplexData = simplexApi.FillSucessData(SimplexData);
                }
            }
            else if (Stage == 2 && Step == 2)
            {
                // Pega coluna permitida
                var positive = simplexApi.SecondStageCheckIfValid(SimplexData.GridArray);
                if (positive)
                {
                    // Caso tenha vai para o próximo passo
                    Step++;
                }
                else
                {
                    // Se não tem, é um caso de região permissiva impossível
                    SimplexData.Status = SimplexStatus.Infinite;
                }
            }
            else if (Stage == 2 && Step == 3)
            {
                // Pega linha permitida
                SimplexData.AllowedRow = simplexApi.SecondStageGetAllowedRow(SimplexData);
                Step++;
            }

            if (SimplexData.Status != SimplexStatus.Pending || StepHasVisualChange())
                ShowStepForm();
            else if (SimplexData.Status == SimplexStatus.Pending)
                ExecuteSimplex();
        }

        private void ShowStepForm()
        {
            var openedGrid = Application.OpenForms["StepByStepForm"];
            if (openedGrid != null)
            {
                var stepByStepForm = openedGrid as StepByStepForm;
                stepByStepForm?.Restart(this);
            }
            else
            {
                var gridForm = new StepByStepForm(this);
                gridForm.Show();
            }
        }

        private bool StepHasVisualChange()
        {
            if (((Step == 0 || Step == 1) && SimplexData.AllowedColumn == 0) || Step == 2||Step == 5 || Step == 6 || SimplexData.Status != SimplexStatus.Pending)
                return true;
            return false;
        }

        private ArrayOfString GetColumnsHeaderArray()
        {
            // Monta o cabeçalho das variáveis não básicas
            var columnHeaderArray = new ArrayOfString();
            columnHeaderArray.Insert(0, "ML");
            for (int i = 1; i <= SimplexData.Problem.Variables.Count; i++)
            {
                columnHeaderArray.Add(SimplexData.Problem.Variables[i - 1].Value);
            }

            return columnHeaderArray;
        }

        private ArrayOfString GetRowsHeaderArray()
        {
            // Monta o cabeçalho das variáveis básicas
            var rowsHeaderArray = new ArrayOfString();
            rowsHeaderArray.Insert(0, "f(x)");
            for (int i = 1; i <= SimplexData.Problem.Restrictions.Count; i++)
                rowsHeaderArray.Add(SimplexData.Problem.Restrictions[i - 1].RestrictionLeftOver.LeftOverVariable.Value);

            return rowsHeaderArray;
        }

        private List<List<GridCell>> GetProblemSimplexGrid()
        {
            // Preenche o grid com o valor das variáveis na função
            // E o valor das variáveis na função referente à variável de folga
            var simplexGrid = Create.GridArray(SimplexData.Problem.Variables.Count,
                SimplexData.Problem.Restrictions.Count);

            for (int i = 0; i < SimplexData.Problem.Variables.Count; i++)
            {
                simplexGrid[i + 1][0] = Create.GridCell(SimplexData.Problem.Variables[i].FunctionValue, 0);
            }
            for (int i = 0; i < SimplexData.Problem.Restrictions.Count; i++)
            {
                var restr = SimplexData.Problem.Restrictions[i].RestrictionLeftOver;

                simplexGrid[0][i + 1] = Create.GridCell(restr.FreeMember, 0);
                for (int j = 0; j < restr.RestrictionVariables.Count; j++)
                {
                    simplexGrid[j + 1][i + 1] = Create.GridCell(restr.RestrictionVariables[j].RestrictionValue, 0);
                }
            }

            return simplexGrid;
        }

        public void NextStep()
        {
            ExecuteSimplex();
        }
    }
}
