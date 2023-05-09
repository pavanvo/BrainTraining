﻿using BrainTraining.Controls;
using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using BrainTraining.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BrainTraining.Model.Tasks {
    internal class AgileTask : BaseTask {
        SelectTask Menu { get; set; }

        public override string Name => "Гибкость";

        Dictionary<Color, string> Colors = new Dictionary<Color, string>() {
            { Color.Red, "Красный"},
            { Color.Green, "Зелёный"},
            { Color.Blue, "Синий"},
            { Color.Black, "Чёрный"},
            { Color.Brown, "Коричневый"},
        };

        Dictionary<int, int> Levels = new Dictionary<int, int>() {
            { 1, 15},
            { 2, 14},
            { 3, 14},
            { 4, 14},
            { 5, 13},
            { 6, 13},
            { 7, 12},
            { 8, 12},
            { 9, 10},
            { 10, 10},
            { 11, 9},
            { 12, 9},
            { 13, 9},
            { 14, 8},
            { 15, 8},
            { 16, 8},
            { 17, 7},
            { 18, 7},
            { 19, 7},
            { 20, 6},
            { 21, 6},
            { 22, 6},
            { 23, 5},
            { 24, 5},
            { 25, 4},
            { 26, 4},
            { 27, 3},
            { 28, 3},
            { 29, 2},
            { 30, 2},

        };

        enum Figura {
            Trigangle,
            Square,
            Circle,
        }

        readonly Dictionary<Figura, Action<PaintEventArgs, Color>> Figures = new Dictionary<Figura, Action<PaintEventArgs, Color>>() {
            { Figura.Trigangle, (e,c) => e.FillTriangle(c) },
            { Figura.Square,(e, c) => e.FillSquare(c) },
            { Figura.Circle,(e, c) => e.FillCircle(c) },
        };

        static readonly int TableColomns = 1;
        static readonly int TableRows = 3;

        TableLayoutPanel Table { get; set; } = ControlHelper.GetTable(TableColomns, TableRows);
        Label LabelScore = new Label {
            Font = ControlHelper.BiggerFont,
            TextAlign = ContentAlignment.MiddleCenter,
            Height = 50,
            Width = ControlHelper.DefaultWidth,
            ForeColor = Color.DarkRed,
        };


        Waiter Waiter = new Waiter();
        private bool? Answer = null;

        public AgileTask(SelectTask menu) : base(menu.MainForm) {
            Menu = menu;

            Setup = async () => {
                Table.Hide();
                Table.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
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
            else {

            }
            // При неправильном выборе высвечивается окно, в котором написано – 
            //Ответ неверный. И ниже предложено начать заново или войти в главное меню.
        }

        private async Task<bool> SetLevel(KeyValuePair<int, int> lvl) {
            var speed = lvl.Value * 1000;
            LabelScore.Text = lvl.Key + "";
            Answer = null;

            var random = new Random();

            var figura = Figures.ElementAt(random.Next(0, Figures.Count));
            var color1 = Colors.ElementAt(random.Next(0, Colors.Count));
            var color2 = Colors.ElementAt(random.Next(0, Colors.Count));
            var color3 = Colors.ElementAt(random.Next(0, Colors.Count));
            // Чтобы жизнь скучной не казалась
            if (random.Next(0, 10) > 5) color1 = color3;

            var answerBox = FillTable(color1.Value, color2.Key, color3.Key, figura.Value);
            Table.Show();

            await Waiter.Wait(timeout: speed);

            if (Answer == null) return false;

            var result = (color1.Key == color3.Key) == Answer;
            //Show result
            if (result) {
                Sound.Play(SoundType.Good);
                answerBox.BackgroundImage = Resources.greenCheck;
            }
            else {
                Sound.Play(SoundType.Error);
                answerBox.BackgroundImage = Resources.redCross;
            }
            await Task.Delay(500);

            return result;
        }

        private PictureBox FillTable(string color1, Color color2, Color color3, Action<PaintEventArgs, Color> action) {
            Table.SuspendLayout();
            Table.Controls.Clear();

            var label = new Label {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = color1,
                ForeColor = color2,
                Font = ControlHelper.BigFont,
            };
            Table.Controls.Add(label, 0, 0);

            var pictureBox = new PictureBox {
                Dock = DockStyle.Fill
            };
            pictureBox.Paint += (o, e) => action(e, color3);
            Table.Controls.Add(pictureBox, 0, 1);

            var answerbox = new PictureBox {
                Dock = DockStyle.Fill,
                BackgroundImageLayout = ImageLayout.Zoom,
            };
            Table.Controls.Add(answerbox, 0, 2);

            Table.ResumeLayout();
            return answerbox;
        }


        private void Yes_Click(object sender, EventArgs e) {
            Answer = true;
            Waiter.Go();
        }
        private void No_Click(object sender, EventArgs e) {
            Answer = false;
            Waiter.Go();
        }

        override protected Panel getHeader() {
            var result = base.getHeader();

            result.Controls.Add(LabelScore);
            LabelScore.Move2Centr(0);

            result.Controls.Add(Waiter.ProgressBar);
            Waiter.ProgressBar.Move2Centr(50);

            //result.Controls.Add(Waiter.LabelTime);
            //Waiter.LabelTime.Move2Centr(80);

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

            var yes = new Button {
                Width = result.Width / 2,
                Dock = DockStyle.Right,
                BackgroundImage = Resources.greenCheck,
                BackgroundImageLayout = ImageLayout.Zoom,
                BackColor = Color.White,
            };
            yes.Click += Yes_Click;

            var no = new Button {
                Width = result.Width / 2,
                Dock = DockStyle.Left,
                BackgroundImage = Resources.redCross,
                BackgroundImageLayout = ImageLayout.Zoom,
                BackColor = Color.White,
            };
            no.Click += No_Click;

            result.Controls.Add(yes);
            result.Controls.Add(no);

            return result;
        }

        override protected Button getButtonBack() {
            var result = base.getButtonBack();

            result.Text = "В меню";
            result.Click += (o, e) => { Waiter.Stop(); Sound.Play(SoundType.Button); Menu.Setup(); };

            return result;
        }
    }
}
