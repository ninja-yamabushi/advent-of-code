using AdventOfCode.Common;
using System;
using System.Windows.Forms;

namespace AdventOfCode.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();

            using (this.UseCursor(Cursors.WaitCursor))
            {
                IPuzzle solver = new PuzzleFactory().Create(CurrentYear, CurrentDay, CurrentProblem);
                txtOutput.Text = solver.Solve(txtInputs.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboYear.DataSource = PuzzleFactory.GetYears();
            cboYear.SelectedIndex = 0;
            cboDay.DataSource = PuzzleFactory.GetDays();
            cboDay.SelectedIndex = 0;
            cboProblem.DataSource = PuzzleFactory.GetProblems();
            cboProblem.SelectedIndex = 0;
        }


        private string CurrentYear { get { return cboYear.SelectedItem.ToString(); } }
        private string CurrentDay { get { return cboDay.SelectedItem.ToString(); } }
        private string CurrentProblem { get { return cboProblem.SelectedItem.ToString(); } }
    }
}
