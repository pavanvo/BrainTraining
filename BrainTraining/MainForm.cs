using BrainTraining.Helpers;
using BrainTraining.Model.Tasks;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BrainTraining {
    public partial class MainForm : Form {
        private readonly int Angle = new Random().Next(0, 180);

        public MainForm() {
            DoubleBuffered = true;

            InitializeComponent();

            MaximizeBox = false;
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void MainForm_Load(object sender, EventArgs e) {

            var selectTask = new SelectTask(this);
            selectTask.Setup();
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);
            e.Graphics.FillRectangle(new LinearGradientBrush(Bounds, ControlHelper.Blue, Color.FromArgb(0, 0, 0), Angle), Bounds);
        }
    }
}
