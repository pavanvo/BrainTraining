using BrainTraining.Controls;
using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class RulesTask : BaseTask {

        ITask Menu { get; set; }
        public override string Name => "Правила";
        public override string Description => $"{Name} Игры";

        public override int HeaderHeight { get; protected set; } = 25;
        public override int ContentHeight { get; protected set; } = 75;
        public override int FooterHeight { get; protected set; } = 0;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public RulesTask(ITask menu) : base(menu.MainForm) {
            Menu = menu;
        }

        public override Task Setup() {
            var tasks = (Menu as SelectTask).Tasks.Where(x => x != this);
            var height = 0;

            foreach (var task in tasks) {
                var label = new GrowLabel {
                    Height = Content.Height / 8,
                    Width = Content.Width,
                    Font = ControlHelper.BigFont,
                    ForeColor = ControlHelper.Orange,
                };
                label.Text = task.Description;
                Content.Controls.Add(label);
                label.Move2Centr(height);
                height += 100 / tasks.Count();
            }

            MainForm.SetupTask(this);

            return Task.CompletedTask;
        }

        override protected Panel getHeader() {
            var result = base.getHeader();
            var label1 = new GrowLabel {
                AutoSize = true,
                Font = ControlHelper.BigFont,
                ForeColor = ControlHelper.Orange,
                Text = Description,
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
