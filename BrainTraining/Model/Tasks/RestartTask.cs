using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class RestartTask : BaseTask {
        private ITask Menu { get; set; }

        private ITask PreviousTask { get; set; }

        public override string Name => "Попробуйте ещё раз";
        public override string Description => $"{Name}";

        Label labelRestart = new Controls.GrowLabel {
            AutoSize = true,
            Font = ControlHelper.BiggerFont,
        };

        Label labelResult = new Controls.GrowLabel {
            AutoSize = true,
            Font = ControlHelper.BiggerFont,

        };

        public RestartTask(ITask menu, ITask previousTask, string score) : base(menu.MainForm) {
            Menu = menu;
            PreviousTask = previousTask;

            Setup = () => {
                labelRestart.Text = Name;
                labelRestart.Move2Centr();

                labelResult.Text = score;
                labelResult.Move2Centr();

                MainForm.SetupTask(this);
            };

        }

        override protected Panel getHeader() {
            var result = base.getHeader();

            result.Controls.Add(labelRestart);

            return result;
        }

        override protected Panel getContent() {
            var result = base.getContent();

            result.Controls.Add(labelResult);

            return result;
        }

        override protected Panel getFooter() {
            var result = base.getFooter();

            var previous = new Button {
                Width = result.Width / 2,
                Dock = DockStyle.Right,
                Font = ControlHelper.BigFont,
                Text = "Заново",
                BackColor = Color.White,
            };
            previous.Click += (o, e) => { Sound.Play(SoundType.Button); PreviousTask.Setup(); };

            var toMenu = new Button {
                Width = result.Width / 2,
                Dock = DockStyle.Left,
                Font = ControlHelper.BigFont,
                Text = "В меню",
                BackColor = Color.White,
            };
            toMenu.Click += (o, e) => { Sound.Play(SoundType.Button); Menu.Setup(); };

            result.Controls.Add(previous);
            result.Controls.Add(toMenu);

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
