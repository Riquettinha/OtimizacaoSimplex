using System.Linq;
using System.Net.Mime;
using System.Windows.Forms;
using OSC.Problem_Classes;

namespace OSC.Classes
{
    public class SimplexMethod
    {
        public SimplexData SimplexData;
        public int Stage = 1;
        public int Step = -1;
        readonly bool _stepbystep;

        public SimplexMethod(ProblemData problem, bool stepbystep)
        {
            SimplexData = new SimplexData
            {
                Problem = problem,
                Status = SimplexStatus.Pending
            };

            _stepbystep = stepbystep;
            ExecuteSimplex();
        }

        private void ExecuteSimplex()
        {
            if (Step == -1)
            {
                SimplexSteps.TransformFunction(ref SimplexData);
                SimplexSteps.CreateRestrictionLeftover(ref SimplexData);
                Step++;
            }
            else if (Step == 0)
            {
                SimplexSteps.IsolateTheLeftOver(ref SimplexData);
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
                if (SimplexSteps.FirstStageCheckForTheEnd(SimplexData.GridArray) != 0)
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
                SimplexData.AllowedColumn = SimplexSteps.FirstStageGetAllowedColumn(SimplexData.GridArray);
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
                SimplexData.AllowedRow  = SimplexSteps.FirstStageGetAllowedRow(SimplexData);
                Step++;
            }
            else if (Step == 4)
            {
                // Preenche célular inferiores do grid
                SimplexSteps.FirstStageFillInferiorCells(ref SimplexData);
                Step++;
            }
            else if (Step == 5)
            {
                // Troca coluna da variáveis básicas com não básicas
                SimplexSteps.FirstStageUpdateHeaders(ref SimplexData);
                Step++;
            }
            else if (Step == 6)
            {
                // Preenche novamente células superiores e volta à verificação do primeiro passo
                SimplexSteps.FirstStageReposition(ref SimplexData);
                Step = 1;
                Stage = 1;
            }
            else if (Stage == 2 && Step == 1)
            {
                // Verifica se existe variável com valor de função positiva
                SimplexData.AllowedColumn = SimplexSteps.SecondStageGetAllowedColumn(SimplexData.GridArray);
                if (SimplexData.AllowedColumn != 0)
                {
                    // Caso tenha vai para o próximo passo
                    Step++;
                }
                else
                {
                    // Solução ÓTIMA encontrada
                    SimplexData.Status = SimplexStatus.Sucess;
                    SimplexSteps.FillSucessData(ref SimplexData);
                }
            }
            else if (Stage == 2 && Step == 2)
            {
                // Pega coluna permitida
                var positive = SimplexSteps.SecondStageCheckIfValid(SimplexData.GridArray);
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
                SimplexData.AllowedRow = SimplexSteps.SecondStageGetAllowedRow(SimplexData);
                Step++;
            }

            if (SimplexData.Status != SimplexStatus.Pending || _stepbystep && StepHasVisualChange())
                ShowStepForm();
            else if (SimplexData.Status == SimplexStatus.Pending)
                ExecuteSimplex();
        }

        private void ShowStepForm()
        {
            var openedGrid = Application.OpenForms["StepByStepForm"];
            if (openedGrid != null)
                (openedGrid as StepByStepForm).Restart(this);
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

        private string[] GetColumnsHeaderArray()
        {
            // Monta o cabeçalho das variáveis não básicas
            var columnsHeaderArray = new string[SimplexData.Problem.Variables.Count + 1];
            columnsHeaderArray[0] = "ML";
            for (int i = 1; i <= SimplexData.Problem.Variables.Count; i++)
                columnsHeaderArray[i] = SimplexData.Problem.Variables[i - 1].Value;

            return columnsHeaderArray;
        }

        private string[] GetRowsHeaderArray()
        {
            // Monta o cabeçalho das variáveis básicas
            var rowsHeaderArray = new string[SimplexData.Problem.Restrictions.Count + 1];
            rowsHeaderArray[0] = "f(x)";
            for (int i = 1; i <= SimplexData.Problem.Restrictions.Count; i++)
                rowsHeaderArray[i] = SimplexData.Problem.Restrictions[i - 1].RestrictionLeftOver.LeftOverVariable.Value;

            return rowsHeaderArray;
        }

        private GridCell[,] GetProblemSimplexGrid()
        {
            // Preenche o grid com o valor das variáveis na função
            // E o valor das variáveis na função referente à variável de folga
            var simplexGrid = new GridCell[SimplexData.NonBasicVariables.Length, SimplexData.BasicVariables.Length];
            simplexGrid[0, 0] = new GridCell(0, 0);
            for (int i = 0; i < SimplexData.Problem.Variables.Count; i++)
            {
                simplexGrid[i + 1, 0] = new GridCell(SimplexData.Problem.Variables[i].FunctionValue, 0);
            }
            for (int i = 0; i < SimplexData.Problem.Restrictions.Count; i++)
            {
                var rtrLeftOver = SimplexData.Problem.Restrictions[i].RestrictionLeftOver;

                simplexGrid[0, i + 1] = new GridCell(rtrLeftOver.FreeMember, 0);
                for (int j = 0; j < rtrLeftOver.RestrictionVariables.Count; j++)
                {
                    simplexGrid[j + 1, i + 1] = new GridCell(rtrLeftOver.RestrictionVariables[j].RestrictionValue, 0);
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
