using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using OSC.Problem_Classes;

namespace OSC.Classes
{
    public class SimplexMethod
    {
        public SimplexData SimplexData;
        public int Stage = 1;
        public int Step = -1;

        public SimplexMethod(ProblemData problem)
        {
            SimplexData = new SimplexData
            {
                Problem = problem
            };

            ExecuteSimplex();
        }

        public SimplexMethod(int step)
        {
            GeraProblemaParaTeste();
            Step = step;
            ExecuteSimplex();
        }
        
        public SimplexMethod(ProblemData problem, int step)
        {
            SimplexData.Problem = problem;
            Step = step;
            ExecuteSimplex();
        }

        private void GeraProblemaParaTeste()
        {
            SimplexData = new SimplexData();
            // Gera um problema para testes (igual ao do slide)
            SimplexData.Problem = new ProblemData();
            SimplexData.Problem.Variables.Add(new VariableData
            {
                Description = "x1".SubscriptNumber(),
                Value = "x1".SubscriptNumber(),
                FunctionValue = 80
            });
            SimplexData.Problem.Variables.Add(new VariableData
            {
                Description = "x2".SubscriptNumber(),
                Value = "x2".SubscriptNumber(),
                FunctionValue = 60
            });
            SimplexData.Problem.Function = new FunctionData()
            {
                Maximiza = false
            };
            SimplexData.Problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.MoreThan,
                RestrictionValue = 24,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 4,
                        RestrictionVariable = SimplexData.Problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 6,
                        RestrictionVariable = SimplexData.Problem.Variables[1]
                    }
                }
            });
            SimplexData.Problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.LessThan,
                RestrictionValue = 16,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 4,
                        RestrictionVariable = SimplexData.Problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 2,
                        RestrictionVariable = SimplexData.Problem.Variables[1]
                    }
                }
            });
            SimplexData.Problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.LessThan,
                RestrictionValue = 3,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 0,
                        RestrictionVariable = SimplexData.Problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 1,
                        RestrictionVariable = SimplexData.Problem.Variables[1]
                    }
                }
            });
        }

        private void ExecuteSimplex()
        {
            if (Step == -1)
            {
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
                SimplexData.SimplexGridArray = GetProblemSimplexGrid();
            }
            else if (Stage == 1 && Step == 1)
            {
                // Verifica se existe membro livre negativpo
                if (SimplexSteps.FirstStageCheckForTheEnd(SimplexData.SimplexGridArray) != 0)
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
                SimplexData.AllowedColumn = SimplexSteps.FirstStageGetAllowedColumn(SimplexData.SimplexGridArray);
                if (SimplexData.AllowedColumn != 0)
                {
                    Step++;
                }
                else
                {
                    Helpers.ShowErrorMessage("Solução permissível inexistente!");
                    return;
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
                // Independe de etapa
                // Preenche célular inferiores
                SimplexSteps.FirstStageFillInferiorCells(ref SimplexData);
                Step++;
            }
            else if (Step == 5)
            {
                // Troca variáveis básicas com não básicas
                SimplexSteps.FirstStageUpdateHeaders(ref SimplexData);
                Step++;
                var aux = new SimplexGrid(this);
                aux.ShowDialog();
            }
            else if (Step == 6)
            {
                // Preenche novamente células superiores
                SimplexSteps.FirstStageReposition(ref SimplexData);
                Step = 1;
                Stage = 1;
            }
            else if (Stage == 2 && Step == 1)
            {
                // Verifica se existe variável com valor de função positiva
                if (SimplexSteps.SecondStageNegativeValueInFunction(SimplexData.SimplexGridArray) != 0)
                {
                    // Caso tenha vai para o próximo passo
                    Step++;
                }
                else
                {
                    // Solução ÓTIMA
                var aux = new SimplexGrid(this);
                aux.ShowDialog();
                }
            }
            else if (Stage == 2 && Step == 2)
            {
                // Pega coluna permitida
                SimplexData.AllowedColumn = SimplexSteps.SecondStageGetAllowedColumn(SimplexData.SimplexGridArray);
                if (SimplexData.AllowedColumn != 0)
                {
                    Step++;
                }
                else
                {
                    Helpers.ShowErrorMessage("Solução permissível inexistente! Infinita.");
                    return;
                }
            }
            else if (Stage == 2 && Step == 3)
            {
                // Pega linha permitida
                SimplexData.AllowedRow = SimplexSteps.SecondStageGetAllowedRow(SimplexData);
                Step++;
            }
            ExecuteSimplex();
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
            if (Step <= 0)
                Step++;
            ExecuteSimplex();
        }
    }
}
