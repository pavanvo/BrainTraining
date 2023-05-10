using BrainTraining.Controls;
using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class MemoryTask : BaseTask {
        ITask Menu { get; set; }

        public override string Name => "Память";
        public override string Description => $"Задание «{Name}» – запомните расположение цветных блоков, затем выберите их. ";

        static readonly int TableColomns = 6;
        static readonly int TableRows = 6;
        static readonly int MemoryPause = 2000;


        TableLayoutPanel Table { get; set; } = ControlHelper.GetTable(TableColomns, TableRows);

        private readonly Dictionary<int, int> Levels = new Dictionary<int, int> {
            { 1, 2 },
            { 2, 3 },
            { 3, 3 },
            { 4, 4 },
            { 5, 4 },
            { 6, 5 },
            { 7, 5 },
            { 8, 6 },
            { 9, 6 },
            { 10, 7 },
            { 11, 7 },
            { 12, 7 },
            { 13, 8 },
            { 14, 8 },
            { 15, 8 },
            { 16, 8 },
            { 17, 9 },
            { 18, 9 },
            { 19, 9 },
            { 20, 9 },
            { 21, 9 },
            { 22, 10 },
            { 23, 10 },
            { 24, 10 },
            { 25, 11 },
        };

        private ManualResetEvent mre;

        private int CurrentBlue = 0;
        private int LavelBlue = 0;

        Label LabelScore = new GrowLabel {
            Font = ControlHelper.BiggerFont,
            AutoSize = true,
            ForeColor = ControlHelper.Orange,
        };

        public MemoryTask(ITask menu) : base(menu.MainForm) {
            Menu = menu;

            Setup = async () => {
                Table.Hide();
                MainForm.SetupTask(this);
                await Start();
            };
        }

        private async Task Start() {
            var result = false;
            foreach (var lvl in Levels) {

                result = await SetLevel(lvl);
                if (!result) {
                    break;
                }
            }
            if (result) {
                Sound.Play(SoundType.Win);
            }
            var text = result ? ControlHelper.RESULT_GOOD : ControlHelper.RESULT_BAD;
            var score = $"{text} {LabelScore.Text}";

            var restarttask = new RestartTask(Menu, this, score);//TODO get Scorefrom Level
            restarttask.Setup();
        }

        private async Task<bool> SetLevel(KeyValuePair<int, int> lvl) {
            LavelBlue = lvl.Value;
            LabelScore.Text = lvl.Key + "";
            LabelScore.Move2Centr();
            CurrentBlue = 0;
            var points = GenerateLevel(LavelBlue);

            FillTable0(points);
            Table.Show();

            await Task.Delay(MemoryPause);

            Table.Hide();
            FillTable1(points);
            Table.Show();

            mre = new ManualResetEvent(false);
            await Task.Run(() => mre.WaitOne());

            return CurrentBlue == LavelBlue;
        }

        private Dictionary<Point, bool> GenerateLevel(int count) {
            var bluePoints = new List<Point>();
            var random = new Random();

            do {
                var point = new Point(random.Next(0, TableColomns), random.Next(0, TableRows));
                if (!bluePoints.Contains(point))
                    bluePoints.Add(point);

            } while (bluePoints.Count != count);

            var points = new Dictionary<Point, bool>();
            for (int x = 0; x < TableColomns; x++) {
                for (int y = 0; y < TableRows; y++) {
                    var point = new Point(x, y);
                    var blue = bluePoints.Contains(point);
                    points.Add(point, blue);
                }
            }
            return points;
        }

        private void FillTable0(Dictionary<Point, bool> points) {
            Table.SuspendLayout();
            Table.Controls.Clear();
            foreach (var point in points) {
                var pictureBox = new PictureBox {
                    BackColor = point.Value ? ControlHelper.Blue : Color.WhiteSmoke,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(1),
                    Tag = point.Value,
                };
                Table.Controls.Add(pictureBox, point.Key.X, point.Key.Y);
            }
            Table.ResumeLayout();
        }

        private void FillTable1(Dictionary<Point, bool> points) {
            Table.SuspendLayout();
            Table.Controls.Clear();
            foreach (var point in points) {
                var pictureBox = new PictureBox {
                    BackColor = Color.WhiteSmoke,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(1),
                    Tag = point.Value,
                };
                pictureBox.Click += PictureBox_Click;
                Table.Controls.Add(pictureBox, point.Key.X, point.Key.Y);
            }
            Table.ResumeLayout();
        }

        private void PictureBox_Click(object sender, EventArgs e) {
            var pictureBox = (PictureBox)sender;
            var blue = (bool)pictureBox.Tag;

            if (blue) {
                if (pictureBox.BackColor != ControlHelper.Blue) {
                    CurrentBlue++;
                    Sound.Play(SoundType.Good);
                    pictureBox.BackColor = ControlHelper.Blue;
                    if (CurrentBlue == LavelBlue) mre.Set();
                }
            }
            else {
                Sound.Play(SoundType.Error);
                mre.Set();
            }
        }

        override protected Panel getHeader() {
            var result = base.getHeader();

            result.Controls.Add(LabelScore);

            return result;
        }

        override protected Panel getContent() {
            var result = base.getContent();

            result.Controls.Add(Table);

            Table.Move2Centr();

            return result;
        }

        override protected Panel getFooter() {
            var result = base.getFooter();

            return result;
        }

        override protected RoundButton getButtonBack() {
            var result = base.getButtonBack();

            result.Text = "В меню";
            result.Click += (o, e) => { Sound.Play(SoundType.Button); Menu.Setup(); };

            return result;
        }
    }
}
