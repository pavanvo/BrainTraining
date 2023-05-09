using BrainTraining.Controls;
using BrainTraining.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal abstract class BaseTask : ITask {

        public abstract string Name { get; }
        public abstract string Description { get; }

        public virtual int HeaderHeight { get; protected set; } = 15;

        public virtual int ContentHeight { get; protected set; } = 70;

        public virtual int FooterHeight { get; protected set; } = 15;

        public MainForm MainForm { get; protected set; }

        public Action Setup { get; protected set; } = () => { };

        public RoundButton ButtonBack { get; protected set; }

        public Panel Header { get; protected set; }

        public Panel Content { get; protected set; }

        public Panel Footer { get; protected set; }


        protected int minimal { get; set; }


        protected BaseTask(MainForm mainForm) {
            MainForm = mainForm;
            minimal = MainForm.Size.Height < MainForm.Size.Width ? MainForm.Size.Height : MainForm.Size.Width;

            mainForm.SuspendLayout();

            ButtonBack = getButtonBack();
            Header = getHeader();
            Content = getContent();
            Footer = getFooter();


            Header.BackColor = Color.Transparent;
            Content.BackColor = Color.Transparent;
            Footer.BackColor = Color.Transparent;

            mainForm.ResumeLayout(false);
        }

        protected virtual Panel getHeader() {
            var header = Convert.ToInt32(minimal / 100d * HeaderHeight);
            var result = new PanelDoubleBuffered() {
                Size = new Size(minimal, header)
            };

            result.Controls.Add(ButtonBack);
            return result;
        }

        protected virtual Panel getContent() {
            var content = Convert.ToInt32(minimal / 100d * ContentHeight);
            var result = new PanelDoubleBuffered() {
                Size = new Size(minimal, content),
            };

            return result;
        }

        protected virtual Panel getFooter() {
            var footer = Convert.ToInt32(minimal / 100d * FooterHeight);
            var result = new PanelDoubleBuffered() {
                Size = new Size(minimal, footer)
            };

            return result;
        }

        protected virtual RoundButton getButtonBack() {
            var result = new RoundButton {
                Location = new Point(13, 13),
                Size = new Size(150, 150),
                Font = ControlHelper.BigFont,
                Text = "Назад",
            };
            return result;
        }
    }
}
