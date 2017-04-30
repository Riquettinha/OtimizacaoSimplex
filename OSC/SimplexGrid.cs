using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OSC.Classes;

namespace OSC
{
    public partial class SimplexGrid : Form
    {
        private SimplexMethod _simplexMethodClass;
        public SimplexGrid(SimplexMethod simplexMethodClass)
        {
            InitializeComponent();
            _simplexMethodClass = simplexMethodClass;
            ConvertGridToDataTable();
        }

        public SimplexGrid(string text)
        {
            Text = text;
        }

        public void SelectedGridItem(int column, int row)
        {
            gridView.CurrentCell = gridView.Rows[row].Cells[column];
        }

        public void ConvertGridToDataTable()
        {
            // Transforma o grid e cabeçalhos em um datagridview legível
            DataTable tbl = new DataTable();
            tbl.Columns.Add("MB / MNB");
            foreach (var ch in _simplexMethodClass.NotBasicVariables)
                tbl.Columns.Add(ch);

            for (int r = 0; r < _simplexMethodClass.BasicVariables.Length; r++)
            {
                DataRow row = tbl.NewRow();
                row["MB / MNB"] = _simplexMethodClass.BasicVariables[r];
                for (int c = 0; c < _simplexMethodClass.NotBasicVariables.Length; c++)
                {
                    if (_simplexMethodClass.SimplexTupleGrid[c, r] != null)
                    {
                        var tblCol = tbl.Columns[c+1];
                        row[tblCol.ColumnName] = Math.Round(_simplexMethodClass.SimplexTupleGrid[c, r].Superior, 4) + " / " +
                                                  Math.Round(_simplexMethodClass.SimplexTupleGrid[c, r].Inferior, 4);
                    }
                }
                tbl.Rows.Add(row);
            }

            gridView.DataSource = tbl;
        }

        private void SimplexStep1_Load(object sender, EventArgs e)
        {
            MinimumSize = new Size(this.Width, this.Height);
            MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            gridView.MinimumSize = new Size(this.Width, this.Height);
            gridView.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);


            gridView.AutoSize = true;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            _simplexMethodClass.NextStep();
        }
    }
}
