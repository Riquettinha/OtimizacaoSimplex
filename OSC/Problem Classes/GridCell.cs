namespace OSC.Problem_Classes
{
    public class GridCell
    {
        public decimal Superior { get; set; }
        public decimal Inferior { get; set; }

        public GridCell(decimal sup, decimal inf)
        {
            Superior = sup;
            Inferior = inf;
        }
    }
}
