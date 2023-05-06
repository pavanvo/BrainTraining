using BrainTraining.Model.Tasks;
using System;
using System.Windows.Forms;

namespace BrainTraining {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            MaximizeBox = false;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void MainForm_Load(object sender, EventArgs e) {

            var selectTask = new SelectTask(this);
            selectTask.Setup();

            //Content.BackColor = Color.Pink;
            //Footer.BackColor = Color.Blue;
        }

        private void MainForm_Shown(object sender, EventArgs e) {

        }
    }
}
