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
        public ProblemData Problem;
        public int Stage = 1;
        public int Step = -1;
        public string[] NotBasicVariables;
        public string[] BasicVariables;
        public GridCell[,] SimplexTupleGrid;
        private int _allowedColumn;
        private int _allowedRow;

        public SimplexMethod(ProblemData problem)
        {
            Problem = problem;

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
            Problem = problem;
            Step = step;
            ExecuteSimplex();
        }

        private void GeraProblemaParaTeste()
        {
            // Gera um problema para testes (igual ao do slide)
            Problem = new ProblemData();
            Problem.Variables.Add(new VariableData
            {
                Description = "x1".SubscriptNumber(),
                Value = "x1".SubscriptNumber(),
                FunctionValue = 80
            });
            Problem.Variables.Add(new VariableData
            {
                Description = "x2".SubscriptNumber(),
                Value = "x2".SubscriptNumber(),
                FunctionValue = 60
            });
            Problem.Function = new FunctionData()
            {
                Maximiza = false
            };
            Problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.MoreThan,
                RestrictionValue = 24,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 4,
                        RestrictionVariable = Problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 6,
                        RestrictionVariable = Problem.Variables[1]
                    }
                }
            });
            Problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.LessThan,
                RestrictionValue = 16,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 4,
                        RestrictionVariable = Problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 2,
                        RestrictionVariable = Problem.Variables[1]
                    }
                }
            });
            Problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.LessThan,
                RestrictionValue = 3,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 0,
                        RestrictionVariable = Problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 1,
                        RestrictionVariable = Problem.Variables[1]
                    }
                }
            });
        }

        private void ExecuteSimplex()
        {
            if (Step == -1)
            {
                SimplexSteps.CreateRestrictionLeftover(ref Problem);
                Step++;
            }
            else if (Step == 0)
            {
                SimplexSteps.IsolateTheLeftOver(ref Problem);
                Step++;

                // Monta um array na ordem das variáveis básicas e não básica
                // Preenche um grid com as informações corretas
                NotBasicVariables = GetColumnsHeaderArray(Problem);
                BasicVariables = GetRowsHeaderArray(Problem);
                SimplexTupleGrid = GetProblemSimplexGrid(Problem, NotBasicVariables, BasicVariables);
            }
            else if (Stage == 1 && Step == 1)
            {
                // Verifica se existe membro livre negativpo
                if (SimplexSteps.FirstStageCheckForTheEnd(SimplexTupleGrid) != 0)
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
                _allowedColumn = SimplexSteps.FirstStageGetAllowedColumn(SimplexTupleGrid);
                if (_allowedColumn != 0)
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
                _allowedRow = SimplexSteps.FirstStageGetAllowedRow(SimplexTupleGrid, _allowedColumn);
                Step = 1;
                Stage++;
            }
            else if (Stage == 2 && Step == 1)
            {
                SimplexSteps.SecondStageFillInferiorCells(ref SimplexTupleGrid, _allowedColumn, _allowedRow);
                SimplexSteps.SecondStageUpdateHeaders(ref NotBasicVariables, ref BasicVariables, _allowedColumn, _allowedRow);
                SimplexSteps.SecondStageReposition(ref SimplexTupleGrid, _allowedColumn, _allowedRow);
                Stage = 1;
                Step = 1;
                var simplexGrid = new SimplexGrid(this);
                simplexGrid.ShowDialog();
            }

            ExecuteSimplex();
        }

        private static string[] GetColumnsHeaderArray(ProblemData problem)
        {
            // Monta o cabeçalho das variáveis não básicas
            var columnsHeaderArray = new string[problem.Variables.Count + 1];
            columnsHeaderArray[0] = "ML";
            for (int i = 1; i <= problem.Variables.Count; i++)
                columnsHeaderArray[i] = problem.Variables[i - 1].Value;

            return columnsHeaderArray;
        }

        private static string[] GetRowsHeaderArray(ProblemData problem)
        {
            // Monta o cabeçalho das variáveis básicas
            var rowsHeaderArray = new string[problem.Restrictions.Count + 1];
            rowsHeaderArray[0] = "f(x)";
            for (int i = 1; i <= problem.Restrictions.Count; i++)
                rowsHeaderArray[i] = problem.Restrictions[i - 1].RestrictionLeftOver.LeftOverVariable.Value;

            return rowsHeaderArray;
        }

        private GridCell[,] GetProblemSimplexGrid(ProblemData problem, string[] columnsHeaderArray, string[] rowsHeaderArray)
        {
            // Preenche o grid com o valor das variáveis na função
            // E o valor das variáveis na função referente à variável de folga
            var simplexGrid = new GridCell[columnsHeaderArray.Length, rowsHeaderArray.Length];
            simplexGrid[0, 0] = new GridCell(0, 0);
            for (int i = 0; i < problem.Variables.Count; i++)
            {
                simplexGrid[i + 1, 0] = new GridCell(problem.Variables[i].FunctionValue, 0);
            }
            for (int i = 0; i < problem.Restrictions.Count; i++)
            {
                var rtrLeftOver = problem.Restrictions[i].RestrictionLeftOver;

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
