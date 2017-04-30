using OSC.Problem_Classes;

namespace OSC.Classes
{
    class SimplexMethod
    {
        public ProblemData _problem;

        public SimplexMethod(ProblemData problem)
        {
            _problem = problem;
            CreateRestrictionLeftover();
        }

        public void CreateRestrictionLeftover()
        {
            for (int i = 0; i < _problem.Restrictions.Count; i++)
            {
                var restr = _problem.Restrictions[i];
                restr.RestrictionLeftOver = new VariableData
                {
                    Value = "R" + i,
                    Description = "Folga Restrição"
                };
                if (restr.RestrictionType == RestrictionType.EqualTo)
                {
                    restr.RestrictionLeftOver.FunctionValue = 0;
                }
                else if (restr.RestrictionType == RestrictionType.MoreThan)
                {
                    restr.RestrictionLeftOver.FunctionValue = -1;
                }
                else if (restr.RestrictionType == RestrictionType.LessThan)
                {
                    restr.RestrictionLeftOver.FunctionValue = 1;
                }

                restr.RestrictionType = RestrictionType.EqualTo;
            }
        }

    }
}
