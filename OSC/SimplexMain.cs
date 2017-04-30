using OSC.Classes;
using System;
using OSC.Problem_Classes;
using System.Windows.Forms;

namespace OSC
{
    public partial class SimplexMain : Form
    {
        ProblemData _problem;
        public SimplexMain(ProblemData problem)
        {
            InitializeComponent();
            _problem = problem;
        }
    
        public SimplexMain()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Helpers.BackForm())
            {
                Application.OpenForms["Restriction"].Show();
                Hide();
            }
        }

        private void btnSetpByStep_Click(object sender, EventArgs e)
        {
            var simplexMethos = new SimplexMethod(_problem);
        }

        private void btnSimplesExecution_Click(object sender, EventArgs e)
        {
            var simplexMethos = new SimplexMethod();

        }
    }
}
