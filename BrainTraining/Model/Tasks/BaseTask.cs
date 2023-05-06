﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal abstract class BaseTask : ITask {
        public MainForm MainForm { get; protected set; }

        public Action Setup { get; protected set; } = () => { };

        public Button ButtonBack { get; protected set; }

        public Panel Header { get; protected set; }

        public Panel Content { get; protected set; }

        public Panel Footer { get; protected set; }


        protected int minimal { get; set; }

        public virtual int HeaderHeight { get; protected set; } = 25;

        public virtual int ContentHeight { get; protected set; } = 50;

        public virtual int FooterHeight { get; protected set; } = 25;


        protected BaseTask(MainForm mainForm) {
            MainForm = mainForm;
            minimal = MainForm.Size.Height < MainForm.Size.Width ? MainForm.Size.Height : MainForm.Size.Width;

            Header = getHeader();
            Content = getContent();
            Footer = getFooter();
            ButtonBack = getButtonBack();


        }

        protected virtual Panel getHeader() {
            var header = Convert.ToInt32(minimal / 100d * HeaderHeight);
            var result = new Panel {
                Size = new Size(minimal, header)
            };

            return result;
        }

        protected virtual Panel getContent() {
            var content = Convert.ToInt32(minimal / 100d * ContentHeight);
            var result = new Panel {
                Size = new Size(minimal, content),
            };

            return result;
        }

        protected virtual Panel getFooter() {
            var footer = Convert.ToInt32(minimal / 100d * FooterHeight);
            var result = new Panel {
                Size = new Size(minimal, footer)
            };

            return result;
        }

        protected virtual Button getButtonBack() {
            var result = new Button {
                Bounds = MainForm.ButtonBack.Bounds,
            };

            return result;
        }
    }
}
