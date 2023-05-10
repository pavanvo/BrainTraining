using BrainTraining.Controls;
using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class SelectTask : BaseTask {

        public override string Name => "Главное меню";
        public override string Description => $"{Name}";
        public override int HeaderHeight { get; protected set; } = 25;
        public override int ContentHeight { get; protected set; } = 75;
        public override int FooterHeight { get; protected set; } = 0;

        public SelectTask(MainForm mainForm) : base(mainForm) {
            Setup = () => MainForm.SetupTask(this);

        }

        public IList<ITask> Tasks { get; protected set; }

        override protected Panel getHeader() {
            var result = base.getHeader();

            var label1 = new GrowLabel {
                AutoSize = true,
                Font = ControlHelper.BigFont,
                ForeColor = ControlHelper.Orange,
                Text = Name,
            };
            var label2 = new GrowLabel {
                AutoSize = true,
                Font = ControlHelper.SmallFont,
                Text = ControlHelper.APP_NAME
            };


            result.Controls.Add(label1);
            result.Controls.Add(label2);

            label1.Move2Centr(60);
            label2.Move2Centr(25);

            return result;
        }

        override protected Panel getContent() {
            var result = base.getContent();

            Tasks = new List<ITask>() {
                new MemoryTask(this),
                new AgileTask(this),
                new SpeedTask(this),
                new RulesTask(this),
            };

            var height = 0;

            foreach (var task in Tasks) {

                var button = new RoundButton {
                    Height = result.Height / 8,
                    Width = result.Width / 2,
                    Font = ControlHelper.BigFont,
                };

                button.Text = task.Name;
                button.Click += (o, e) => { Sound.Play(SoundType.Button); task.Setup(); };
                result.Controls.Add(button);
                button.Move2Centr(height);
                height += 100 / Tasks.Count;
            }

            return result;
        }

        override protected RoundButton getButtonBack() {
            var result = base.getButtonBack();

            result.Text = "Выход";
            result.Click += (o, e) => { Sound.Play(SoundType.Button); Application.Exit(); };

            return result;
        }
    }
}
