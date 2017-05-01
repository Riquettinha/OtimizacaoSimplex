namespace OSC.Problem_Classes
{
    public class SimplexData
    {
        public ProblemData Problem { get; set; }
        public GridCell[,] SimplexGridArray { get; set; }
        public int AllowedColumn { get; set; }
        public int AllowedRow { get; set; }
        public string[] NonBasicVariables { get; set; }
        public string[] BasicVariables { get; set; }
    }
}
