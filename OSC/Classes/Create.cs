using System.Collections.Generic;
using OSC.SimplexApi;

namespace OSC.Classes
{
    static class Create
    {
        public static FunctionData FunctionData(bool maximiza)
        {
            return new FunctionData {Maximiza = maximiza};
        }

        public static GridCell GridCell(decimal superior, decimal inferior)
        {
            return new GridCell
            {
                Superior = superior,
                Inferior = inferior
            };
        }

        public static LeftOverData LeftOverData()
        {
            return new LeftOverData
            {
                LeftOverVariable = VariableData(),
                RestrictionVariables = new List<RestrictionVariableData>()
            };
        }

        public static ProblemData ProblemData()
        {
            return new ProblemData
            {
                Variables = new List<VariableData>(),
                Restrictions = new List<RestrictionFunctionData>(),
                Function = FunctionData(false)
            };
        }

        public static RestrictionFunctionData RestrictionFunctionData()
        {
            return new RestrictionFunctionData
            {
                RestrictionLeftOver = LeftOverData(),
                RestrictionData = new List<RestrictionVariableData>()
            };
        }

        public static RestrictionVariableData RestrictionVariableData()
        {
            return new RestrictionVariableData
            {
                RestrictionVariable = VariableData()
            };
        }

        public static SimplexData SimplexData()
        {
            return new SimplexData
            {
                BasicVariables = new ArrayOfString(),
                NonBasicVariables = new ArrayOfString(),
                GridArray = new List<List<GridCell>>(),
                Problem = ProblemData(),
                Status = SimplexStatus.Pending
            };
        }

        public static VariableData VariableData()
        {
            return new VariableData();
        }

        public static List<List<GridCell>> GridArray(int x, int y)
        {
            var grid = new List<List<GridCell>>();

            var gridFunctionRow = new List<GridCell> { GridCell(0, 0) };
            for (int j = 0; j < y; j++)
                gridFunctionRow.Add(GridCell(0, 0));
            grid.Add(gridFunctionRow);

            for (int i = 0; i < x; i++)
            {
                var gridRow = new List<GridCell>
                {
                    GridCell(0, 0)
                };

                for (int j = 0; j < y; j++)
                    gridRow.Add(GridCell(0, 0));

                grid.Add(gridRow);

            }

            return grid;
        }
    }
}
