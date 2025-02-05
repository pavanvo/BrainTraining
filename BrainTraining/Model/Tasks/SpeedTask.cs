﻿using BrainTraining.Controls;
using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class SpeedTask : BaseTask {

        ITask Menu { get; set; }

        public override string Name => "Скорость";
        public override string Description => $"Задание «{Name}» - выберите числа от наименьшего значения к большему.";

        /// <summary>
        /// Параметры Таблицы
        /// </summary>
        static readonly int TableColomns = 6;
        static readonly int TableRows = 6;
        static readonly int NubresCount = TableColomns * TableRows;

        /// <summary>
        /// Таблица
        /// </summary>
        TableLayoutPanel Table { get; set; } = ControlHelper.GetTable(TableColomns, TableRows);

        private Waiter Waiter = new Waiter();

        private int CurrentNumber = 0;

        /// <summary>
        /// Лэйбл для Следующего
        /// </summary>
        Label LabelNext = new GrowLabel {
            Font = ControlHelper.BiggerFont,
            AutoSize = true,
            ForeColor = ControlHelper.Orange,
        };

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SpeedTask(ITask menu) : base(menu.MainForm) {
            Menu = menu;
            Waiter.TimeFormat = @"mm\:ss\:ff";
        }

        public override async Task Setup() {
            Table.Hide();
            Waiter.LabelTime.Hide();
            MainForm.SetupTask(this);
            await Start();
        }

        /// <summary>
        /// Метод запуска задачи
        /// </summary>
        private async Task Start() {
            var result = await SetLevel();
            if (result) {
                Sound.Play(SoundType.Win);
            }
            var text = result ? ControlHelper.RESULT_GOOD : ControlHelper.RESULT_BAD;
            var score = $"{text} {CurrentNumber} за {Waiter.LabelTime.Text}";

            var restarttask = new RestartTask(Menu, this, score);//TODO get Scorefrom Level
            await restarttask.Setup();
        }

        /// <summary>
        /// Метод Установки уровня
        /// </summary>
        private async Task<bool> SetLevel() {
            LabelNext.Text = string.Empty;
            CurrentNumber = 0;
            var points = GenerateLevel();

            FillTable(points);
            Waiter.LabelTime.Show();
            Table.Show();

            await Waiter.Wait(label: true);

            return CurrentNumber == NubresCount;
        }

        /// <summary>
        /// Метод генерации уровня
        /// </summary>
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


        /// <summary>
        /// Метод заполнения таблицы
        /// </summary>
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
                    Font = ControlHelper.SmallFont,
                };
                button.MouseDown += Button_MouseDown;
                button.MouseUp += Button_MouseUp;
                Table.Controls.Add(button, point.Key.X, point.Key.Y);
            }
            Table.ResumeLayout();
        }

        /// <summary>
        /// Метод Срабатывающий на событие Нажатия кнопки мыши
        /// </summary>
        private void Button_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            var button = (Button)sender;
            var number = (int)button.Tag;

            if (number - 1 == CurrentNumber) {
                CurrentNumber = number;
                Sound.Play(SoundType.Good);
                button.BackColor = ControlHelper.Blue;

                if (CurrentNumber == NubresCount) Waiter.Go();
                else {
                    LabelNext.Text = $"Найдите следующее число: {number + 1}";
                    LabelNext.Move2Centr(0);
                }
            }
            else {
                Sound.Play(SoundType.Error);
                Waiter.Go();
            }
        }

        /// <summary>
        /// Метод Срабатывающий на событие Отпускания кнопки мыши
        /// </summary>
        private void Button_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            var button = (Button)sender;
            button.BackColor = Color.WhiteSmoke;
        }

        override protected Panel getHeader() {
            var result = base.getHeader();

            result.Controls.Add(Waiter.LabelTime);

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

            result.Controls.Add(LabelNext);

            return result;
        }

        override protected RoundButton getButtonBack() {
            var result = base.getButtonBack();

            result.Text = "В меню";
            result.Click += (o, e) => { Waiter.Stop(); Sound.Play(SoundType.Button); Menu.Setup(); };

            return result;
        }
    }
}
