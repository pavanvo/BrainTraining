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
            // Настройка формы на весь экран
            MaximizeBox = false;
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            // Установка меню
            var selectTask = new SelectTask(this);
            selectTask.Setup();
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);
            // Установка градиента
            e.Graphics.FillRectangle(new LinearGradientBrush(Bounds, ControlHelper.Blue, Color.Black, Angle), Bounds);
        }
    }
}
