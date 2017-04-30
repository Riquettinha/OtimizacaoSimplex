using System;
using System.Linq;
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

        public static void TransformFunction(ref ProblemData problem)
        {
            // Negativa zera a função para achar o ponto 0,0.
            // Ou seja, f(a) = 2x + 3y se transforma em 0 = 2x + 3y que é o mesmo que
            // f(a) = 0 - 2x - 3y. Resumindo, é só trocar o símbolo.
            foreach (var variable in problem.Variables)
                variable.FunctionValue *= -1;
        }

        public static void IsolateTheLeftOver(ref ProblemData problem)
        {
            foreach (var restriction in problem.Restrictions)
            {
                // O membro livre é o valor da restrição multiplicado pelo valor do leftover
                // O valor do leftover é definido em "CreateRestrictionLeftOver"
                var restLeftOver = restriction.RestrictionLeftOver;
                restLeftOver.LockedMember = restriction.RestrictionValue * restLeftOver.LeftOverVariable.FunctionValue;

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
                            DifferentSymbol(restrictionVariables.RestrictionValue, restLeftOver.LockedMember)
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

        public static bool FirstStep(ref ProblemData problem)
        {
            var columnsHeaderArray = new string[problem.Variables.Count + 1];
            columnsHeaderArray[0] = "ML";
            for (int i = 1; i <= problem.Variables.Count; i++)
                columnsHeaderArray[i] = problem.Variables[i - 1].Value;

            var rowsHeaderArray = new string[problem.Restrictions.Count + 1];
            rowsHeaderArray[0] = "f(x)";
            for (int i = 1; i <= problem.Restrictions.Count; i++)
                rowsHeaderArray[i] = problem.Restrictions[i - 1].RestrictionLeftOver.LeftOverVariable.Value;
            
            var simplexGrid = new Tuple<decimal, decimal>[columnsHeaderArray.Length,rowsHeaderArray.Length];
            simplexGrid[0, 0] = new Tuple<decimal, decimal>(0, 0);
            for (int i = 0; i < problem.Variables.Count; i++)
            {
                simplexGrid[i + 1, 0] = new Tuple<decimal, decimal>(problem.Variables[i].FunctionValue, 0);
            }
            for (int i = 0; i < problem.Restrictions.Count; i++)
            {
                simplexGrid[0, i+1] = new Tuple<decimal, decimal>(problem.Restrictions[i].RestrictionValue, 0);
                for (int j = 0; j < problem.Restrictions[i].RestrictionData.Count; j++)
                {
                    simplexGrid[j+1, i + 1] = new Tuple<decimal, decimal>(problem.Restrictions[i].RestrictionData[j].RestrictionValue, 0);
                }
            }
            int lockedMemberCollum = 0;

            var teste = new SimplexStep1(columnsHeaderArray, rowsHeaderArray, simplexGrid);
            teste.Show();
            return true;
        }
    }
}
