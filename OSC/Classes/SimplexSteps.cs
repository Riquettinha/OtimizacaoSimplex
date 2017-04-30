using System;
using OSC.Problem_Classes;

namespace OSC.Classes
{
    public static class SimplexSteps
    {
        public static void CreateRestrictionLeftover(ref ProblemData problem)
        {
            // Esse método cria a variável de folga das restrições
            for (int i = 0; i < problem.Restrictions.Count; i++)
            {
                var restr = problem.Restrictions[i];
                restr.RestrictionLeftOver.LeftOverVariable = new VariableData
                {
                    Value = "x" + (i + 1 + problem.Variables.Count).ToString().SubscriptNumber(),
                    Description = "Folga Restrição"
                };

                if (restr.RestrictionType == RestrictionType.EqualTo)
                {
                    // Caso a restrição seja do tipo igual, então a folga deve sempre ser 0
                    restr.RestrictionLeftOver.LeftOverVariable.FunctionValue = 0;
                }
                else if (restr.RestrictionType == RestrictionType.MoreThan)
                {
                    // Caso a restrição exija que seja maior ou igual ao valor passado,
                    // então negativa o valor da folga
                    restr.RestrictionLeftOver.LeftOverVariable.FunctionValue = -1;
                }
                else if (restr.RestrictionType == RestrictionType.LessThan)
                {
                    // Caso a restrição exija que seja maior ou igual ao valor passado,
                    // então positiva o valor da folga
                    restr.RestrictionLeftOver.LeftOverVariable.FunctionValue = 1;
                }

                // Após a adição da variável de folga a restrição + o "FuncionValue"
                // da folga sempre deve ser igual ao RestrictionValue
                restr.RestrictionType = RestrictionType.EqualTo;
            }
        }

        public static void IsolateTheLeftOver(ref ProblemData problem)
        {
            foreach (var restriction in problem.Restrictions)
            {
                // O membro livre é o valor da restrição multiplicado pelo valor do leftover
                // O valor do leftover é definido em "CreateRestrictionLeftOver"
                var restLeftOver = restriction.RestrictionLeftOver;
                restLeftOver.FreeMember = restriction.RestrictionValue * restLeftOver.LeftOverVariable.FunctionValue;

                // Se a variável de sobra for negativa, positiva ela e troca sinal do membro livre
                if (restLeftOver.LeftOverVariable.FunctionValue.IsNegative())
                {
                    restLeftOver.LeftOverVariable.FunctionValue *= -1;
                }

                foreach (var restrictionVariables in restriction.RestrictionData)
                {
                    // Define o valor de cada variável inicial para a função do membro livre
                    // O símbolo das variáveis sempre é igual ao do membro nível
                    // Ex: -10 + 2x + 3y tem que se transformar em -10-(-2x-3y), que matemáticamente é a mesma coisa
                    // O símbolo de menos intermediário será contabilizado no fim do método
                    restLeftOver.RestrictionVariables.Add(new RestrictionVariableData
                    {
                        RestrictionVariable = restrictionVariables.RestrictionVariable,
                        RestrictionValue =
                            DifferentSymbol(restrictionVariables.RestrictionValue, restLeftOver.FreeMember)
                                ? restrictionVariables.RestrictionValue * -1
                                : restrictionVariables.RestrictionValue
                    });
                }
            }
        }

        private static bool DifferentSymbol(decimal value1, decimal value2)
        {
            return value1.IsNegative() != value2.IsNegative();
        }

        public static int FirstStageCheckForTheEnd(GridCell[,] simplexGrid)
        {
            // Procura por um membro livre negativo
            for (int i = 1; i < simplexGrid.GetLength(1); i++)
                if (simplexGrid[0, i].Superior.IsNegative())
                    return i;
            return 0;
        }

        public static int FirstStageGetAllowedColumn(GridCell[,] simplexGrid)
        {
            // Procura por variáveis negativas na linha do membro livre negativo
            var rowWithNegativeFreeNumber = FirstStageCheckForTheEnd(simplexGrid);
            for (int n = 1; n <= simplexGrid.GetLength(1); n++)
            {
                if (simplexGrid[n, rowWithNegativeFreeNumber].Superior.IsNegative())
                    return n;
            }
            return 0;
        }

        public static int FirstStageGetAllowedRow(GridCell[,] simplexGrid, int allowedColumns)
        {
            // Procura pelo menor quociente de ML / variável da coluna permitidda
            int allowedRow = 0;
            decimal minorNumber = -1;
            for (int n = 1; n <= simplexGrid.GetLength(0); n++)
            {
                if (simplexGrid[allowedColumns, n].Superior != 0)
                {
                    var quoc = simplexGrid[0, n].Superior / simplexGrid[allowedColumns, n].Superior;
                    if (quoc < minorNumber || minorNumber == -1)
                    {
                        minorNumber = quoc;
                        allowedRow = n;
                    }
                }
            }
            return allowedRow;
        }

        public static void SecondStageFillInferiorCells(ref GridCell[,] simplexGrid, int allowedColumn, int allowedRow)
        {
            var allowedMember = simplexGrid[allowedColumn, allowedRow];
            allowedMember.Inferior = 1 / allowedMember.Superior;

            for (int c = 0; c < simplexGrid.GetLength(0); c++)
                if (c != allowedColumn)
                {
                    // Está na linha permitida, portanto múltiplica o superior pelo inferior do membro permitido
                    var editingMember = simplexGrid[c, allowedRow];
                    editingMember.Inferior = editingMember.Superior * allowedMember.Inferior;
                }

            for (int r = 0; r < simplexGrid.GetLength(1); r++)
                if (r != allowedRow)
                {
                    // Está na coluna permitida, portando múltiplica pelo negativo do inferior do membro permitido
                    var editingMember = simplexGrid[allowedColumn, r];
                    editingMember.Inferior = editingMember.Superior * (allowedMember.Inferior * -1);
                }

            for (int c = 0; c < simplexGrid.GetLength(0); c++)
                for (int r = 0; r < simplexGrid.GetLength(1); r++)
                    if (c != allowedColumn && r != allowedRow)
                    {
                        // Caso não esteja nem na linha e nem na coluna permitida, soma os valores da
                        // celula superior na linha atual e da coluna permitda com da celula superior da coluna atual com a linha permitida
                        simplexGrid[c, r].Inferior = simplexGrid[c, allowedRow].Superior *
                                                 simplexGrid[allowedColumn, r].Inferior;
                    }
        }

        public static void SecondStageUpdateHeaders(ref string[] nonBasicVariables, ref string[] basicVariables, int allowedColumn, int allowedRow)
        {
            // Troca a básica da linah permtida com a não básic da coluna permitida
            var oldColumnHeader = nonBasicVariables[allowedColumn];
            nonBasicVariables[allowedColumn] = basicVariables[allowedRow];
            basicVariables[allowedRow] = oldColumnHeader;
        }

        public static void SecondStageReposition(ref GridCell[,] simplexGrid, int allowedColumn, int allowedRow)
        {
            // Reescreve itens superiores
            for (int c = 0; c < simplexGrid.GetLength(0); c++)
            {
                for (int r = 0; r < simplexGrid.GetLength(1); r++)
                {
                    if (c == allowedColumn || r == allowedRow)
                    {
                        simplexGrid[c, r].Superior = simplexGrid[c, r].Inferior;
                        simplexGrid[c, r].Inferior = 0;
                    }
                    else
                    {
                        simplexGrid[c, r].Superior = simplexGrid[c, r].Superior+ simplexGrid[c, r].Inferior;
                        simplexGrid[c, r].Inferior = 0;
                    }
                }
            }
        }
    }
}
