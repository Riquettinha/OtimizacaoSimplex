using System.Collections.Generic;
using OSC.Problem_Classes;

namespace OSC.Classes
{
    class SimplexMethod
    {
        public ProblemData _problem;

        public SimplexMethod(ProblemData problem)
        {
            _problem = problem;

            ExecuteSimplex();
        }

        public SimplexMethod()
        {
            GeraProblemaParaTeste();
            ExecuteSimplex();
        }

        private void GeraProblemaParaTeste()
        {
            // Gera um problema para testes (igual ao do slide)
            _problem = new ProblemData();
            _problem.Variables.Add(new VariableData
            {
                Description = "x1".SubscriptNumber(),
                Value = "x1".SubscriptNumber(),
                FunctionValue = 1
            });
            _problem.Variables.Add(new VariableData
            {
                Description = "x2".SubscriptNumber(),
                Value = "x2".SubscriptNumber(),
                FunctionValue = 2
            });
            _problem.Function = new FunctionData()
            {
                Maximiza = false
            };
            _problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.MoreThan,
                RestrictionValue = 24,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 4,
                        RestrictionVariable = _problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 6,
                        RestrictionVariable = _problem.Variables[1]
                    }
                }
            });
            _problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.LessThan,
                RestrictionValue = 16,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 4,
                        RestrictionVariable = _problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 2,
                        RestrictionVariable = _problem.Variables[1]
                    }
                }
            });
            _problem.Restrictions.Add(new RestrictionFunctionData
            {
                RestrictionType = RestrictionType.MoreThan,
                RestrictionValue = 24,
                RestrictionData = new List<RestrictionVariableData>
                {
                    new RestrictionVariableData
                    {
                        RestrictionValue = 0,
                        RestrictionVariable = _problem.Variables[0]
                    },
                    new RestrictionVariableData
                    {
                        RestrictionValue = 1,
                        RestrictionVariable = _problem.Variables[1]
                    }
                }
            });
        }

        private void ExecuteSimplex()
        {
            CreateRestrictionLeftover();
            TransformFunction();
            IsolateTheLeftOver();
            var aux = new SimplexStep0(_problem);
            aux.ShowDialog();
        }

        public void CreateRestrictionLeftover()
        {
            // Esse método cria a variável de folga das restrições
            for (int i = 0; i < _problem.Restrictions.Count; i++)
            {
                var restr = _problem.Restrictions[i];
                restr.RestrictionLeftOver.LeftOverVariable = new VariableData
                {
                    Value = "R" + (i+1).ToString().SubscriptNumber(),
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

        public void TransformFunction()
        {
            // Negativa zera a função para achar o ponto 0,0.
            // Ou seja, f(a) = 2x + 3y se transforma em 0 = 2x + 3y que é o mesmo que
            // f(a) = 0 - 2x - 3y. Resumindo, é só trocar o símbolo.
            foreach (var variable in _problem.Variables)
                variable.FunctionValue *= -1;
        }

        public void IsolateTheLeftOver()
        {
            // TODO: Devo positiva o membro livre :)
            foreach (var restriction in _problem.Restrictions)
            {
                // O membro livre é o valor da restrição multiplicado pelo valor do leftover
                // O valor do leftover é definido em "CreateRestrictionLeftOver"
                var restLeftOver = restriction.RestrictionLeftOver;
                restLeftOver.FreeMember = restriction.RestrictionValue * restLeftOver.LeftOverVariable.FunctionValue;

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
                            DifferentSymbol(restrictionVariables.RestrictionValue, restLeftOver.FreeMember) ?
                            restrictionVariables.RestrictionValue * -1 :
                            restrictionVariables.RestrictionValue
                    });
                }
            }
        }

        private bool DifferentSymbol(decimal value1, decimal value2)
        {
            return value1.IsNegative()  != value2.IsNegative();
        }
    }
}
