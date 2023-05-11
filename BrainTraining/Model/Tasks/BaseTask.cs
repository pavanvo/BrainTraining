using BrainTraining.Controls;
using BrainTraining.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace BrainTraining.Model.Tasks {
    /// <summary>
    /// Базовый абстрактный класс Задача
    /// </summary>
    internal abstract class BaseTask : ITask {

        public abstract string Name { get; }
        public abstract string Description { get; }

        public virtual int HeaderHeight { get; protected set; } = 15;

        public virtual int ContentHeight { get; protected set; } = 70;

        public virtual int FooterHeight { get; protected set; } = 15;

        public MainForm MainForm { get; protected set; }

        public RoundButton ButtonBack { get; protected set; }

        public Panel Header { get; protected set; }

        public Panel Content { get; protected set; }

        public Panel Footer { get; protected set; }

        public abstract Task Setup();

        protected int Minimal { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        protected BaseTask(MainForm mainForm) {
            MainForm = mainForm;
            Minimal = MainForm.Size.Height < MainForm.Size.Width ? MainForm.Size.Height : MainForm.Size.Width;
            // приостановка отрисови
            mainForm.SuspendLayout();
            // Установка компонентов
            ButtonBack = getButtonBack();
            Header = getHeader();
            Content = getContent();
            Footer = getFooter();

            // Фон компонентов прозкачный
            Header.BackColor = Color.Transparent;
            Content.BackColor = Color.Transparent;
            Footer.BackColor = Color.Transparent;
            //Возобновление отрисови
            mainForm.ResumeLayout(false);
        }

        /// <summary>
        /// Получение шапки
        /// </summary>
        protected virtual Panel getHeader() {
            var header = Convert.ToInt32(Minimal / 100d * HeaderHeight);
            var result = new PanelDoubleBuffered() {
                Size = new Size(Minimal, header)
            };

            result.Controls.Add(ButtonBack);
            return result;
        }

        /// <summary>
        /// Получение Содержания
        /// </summary>
        protected virtual Panel getContent() {
            var content = Convert.ToInt32(Minimal / 100d * ContentHeight);
            var result = new PanelDoubleBuffered() {
                Size = new Size(Minimal, content),
            };

            return result;
        }

        /// <summary>
        /// Получение Подвала
        /// </summary>
        protected virtual Panel getFooter() {
            var footer = Convert.ToInt32(Minimal / 100d * FooterHeight);
            var result = new PanelDoubleBuffered() {
                Size = new Size(Minimal, footer)
            };

            return result;
        }

        /// <summary>
        /// Получение Кнопки возврата
        /// </summary>
        protected virtual RoundButton getButtonBack() {
            var result = new RoundButton {
                Location = new Point(13, 13),
                Size = new Size(150, 150),
                Font = ControlHelper.BigFont,
                ButtonBorderColor = ControlHelper.Blue,
                Text = "Назад",
            };
            return result;
        }
    }
}
