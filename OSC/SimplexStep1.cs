using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSC
{
    public partial class SimplexStep1 : Form
    {
        public SimplexStep1(string[] columnHeader, string[] rowHeader, Tuple<decimal, decimal>[,] table)
        {
            InitializeComponent();
            DataTable tbl = new DataTable();
            foreach (var ch in columnHeader)
                tbl.Columns.Add(ch);
            
            for (int r = 0; r < rowHeader.Length; r++)
            {
                DataRow row = tbl.NewRow();
                for (int c = 0; c < columnHeader.Length; c++)
                {
                    if (table[c, r] != null)
                    {
                        var tblCol = tbl.Columns[c];
                        row[tblCol.ColumnName] = table[c, r].Item1 + table[c, r].Item2;
                    }
                }
                tbl.Rows.Add(row);
            }

            gridView.DataSource = tbl;
        }

        private void SimplexStep1_Load(object sender, EventArgs e)
        {

        }
    }
}
