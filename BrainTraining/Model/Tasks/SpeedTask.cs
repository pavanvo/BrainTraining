using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class SpeedTask : BaseTask {

        SelectTask Menu { get; set; }

        static readonly int TableColomns = 6;
        static readonly int TableRows = 6;


        static readonly int NubresCount = TableColomns * TableRows;
        TableLayoutPanel Table { get; set; } = ControlHelper.GetTable(TableColomns, TableRows);

        private ManualResetEvent mre;

        private int CurrentNumber = 0;

        public SpeedTask(SelectTask menu) : base(menu.MainForm) {
            Menu = menu;

            Setup = async () => {
                Table.Hide();
                MainForm.SetupTask(this);
                await Start();
            };
        }

        private async Task Start() {
            var result = await SetLevel();
            if (result)
                Sound.Play(SoundType.Win);
            else {
                //TODO restart
            }
        }

        private async Task<bool> SetLevel() {
            CurrentNumber = 0;
            var points = GenerateLevel();

            FillTable(points);
            Table.Show();

            mre = new ManualResetEvent(false);
            await Task.Run(() => mre.WaitOne());

            return CurrentNumber == NubresCount;
        }

        private Dictionary<Point, int> GenerateLevel() {
            var random = new Random();

            var points = new Dictionary<Point, int>();
            for (int x = 0; x < TableColomns; x++) {
                for (int y = 0; y < TableRows; y++) {
                    var point = new Point(x, y);

                    var number = 0; // cringe
                    do {
                        if (points.ContainsKey(point)) break;

                        number = random.Next(1, NubresCount + 1);
                        if (!points.ContainsValue(number))
                            points.Add(point, number);

                    } while (points.ContainsValue(number));
                }
            }
            return points;
        }

        private void FillTable(Dictionary<Point, int> points) {
            Table.SuspendLayout();
            Table.Controls.Clear();
            foreach (var point in points) {
                var button = new Button {
                    BackColor = Color.WhiteSmoke,
                    FlatStyle = FlatStyle.Flat,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(1),
                    Tag = point.Value,
                    Text = point.Value.ToString(),
                    Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                };
                button.MouseDown += Button_MouseDown;
                button.MouseUp += Button_MouseUp;
                Table.Controls.Add(button, point.Key.X, point.Key.Y);
            }
            Table.ResumeLayout();
        }

        private void Button_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            var button = (Button)sender;
            var number = (int)button.Tag;

            if (number - 1 == CurrentNumber) {
                CurrentNumber = number;
                Sound.Play(SoundType.Good);
                button.BackColor = ControlHelper.Blue;
                if (CurrentNumber == NubresCount) mre.Set();
            }
            else {
                Sound.Play(SoundType.Error);
                mre.Set();
            }
        }

        private void Button_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            var button = (Button)sender;
            button.BackColor = Color.WhiteSmoke;
        }

        override protected Panel getHeader() {
            var result = base.getHeader();

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

        override protected Button getButtonBack() {
            var result = base.getButtonBack();

            result.Text = "В меню";
            result.Click += (o, e) => { Sound.Play(SoundType.Button); Menu.Setup(); };

            return result;
        }
    }
}
