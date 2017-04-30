using System.Collections.Generic;
using OSC.Problem_Classes;

namespace OSC.Classes
{
    class SimplexMethod
    {
        private ProblemData _problem;
        public int _step = -1;

        public SimplexMethod(ProblemData problem)
        {
            _problem = problem;

            ExecuteSimplex();
        }

        public SimplexMethod(int step)
        {
            GeraProblemaParaTeste();
            _step = step;
            ExecuteSimplex();
        }
        
        public SimplexMethod(ProblemData problem, int step)
        {
            _problem = problem;
            _step = step;
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
                FunctionValue = 80
            });
            _problem.Variables.Add(new VariableData
            {
                Description = "x2".SubscriptNumber(),
                Value = "x2".SubscriptNumber(),
                FunctionValue = 60
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
                RestrictionType = RestrictionType.LessThan,
                RestrictionValue = 3,
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
            if (_step == -1)
            {
                SimplexSteps.CreateRestrictionLeftover(ref _problem);
                SimplexSteps.TransformFunction(ref _problem);
            }
            else if (_step == 0)
            {
                SimplexSteps.IsolateTheLeftOver(ref _problem);
            }
            else if (_step == 1)
            {
                SimplexSteps.FirstStep(ref _problem);
            }

            var aux2 = new SimplexStep0(_problem, _step);
            aux2.ShowDialog();
        }
    }
}
