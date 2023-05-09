using BrainTraining.Model.Tasks;
using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using BrainTraining.Helpers;

namespace BrainTraining {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            var angle = new Random().Next(0, 180);
            this.Paint += (o, e) => e.Graphics.FillRectangle(new LinearGradientBrush(Bounds, ControlHelper.Blue, Color.FromArgb(0, 0, 0), angle), Bounds);
            MaximizeBox = false;
            //TopMost = true;
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void MainForm_Load(object sender, EventArgs e) {

            var selectTask = new SelectTask(this);
            selectTask.Setup();

            //Content.BackColor = Color.Pink;
            //Footer.BackColor = Color.Blue;
        }
    }
}
