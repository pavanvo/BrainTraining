using BrainTraining.Helpers;
using BrainTraining.Model.UI;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class SelectTask : BaseTask {
        public override int HeaderHeight { get; protected set; } = 25;
        public override int ContentHeight { get; protected set; } = 75;
        public override int FooterHeight { get; protected set; } = 0;

        public SelectTask(MainForm mainForm) : base(mainForm) {
            Setup = () => MainForm.SetupTask(this);

        }

        private Button getButton(Control parent) {
            var result = new Button {
                Height = parent.Height / 8,
                Width = parent.Width / 2,
                Font = new Font("Microsoft Sans Serif", 25.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
            };
            result.Click += (o, e) => Sound.Play(SoundType.Button);
            return result;
        }

        override protected Panel getHeader() {
            var result = base.getHeader();

            var label1 = new Label {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 25.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Text = "Главное меню"
            };
            var label2 = new Label {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Text = "Brain Training"
            };


            result.Controls.Add(label1);
            result.Controls.Add(label2);

            label1.Move2Centr(60);
            label2.Move2Centr(25);

            return result;
        }

        override protected Panel getContent() {
            var result = base.getContent();

            var memoryTask = new MemoryTask(this);
            var memoryButton = getButton(result);
            memoryButton.Text = "Память";
            memoryButton.Click += (o, e) => { Sound.Play(SoundType.Button); memoryTask.Setup(); };
            result.Controls.Add(memoryButton);
            memoryButton.Move2Centr(0);

            var agileTask = new AgileTask(this);
            var agile = getButton(result);
            agile.Text = "Гибкость";
            agile.Click += (o, e) => { Sound.Play(SoundType.Button); agileTask.Setup(); };
            result.Controls.Add(agile);
            agile.Move2Centr(25);

            var speedTask = new SpeedTask(this);
            var speedButton = getButton(result);
            speedButton.Text = "Скорость";
            speedButton.Click += (o, e) => { Sound.Play(SoundType.Button); speedTask.Setup(); };
            result.Controls.Add(speedButton);
            speedButton.Move2Centr(50);

            var rulesTask = new RulesTask(this);
            var rulesButton = getButton(result);
            rulesButton.Text = "Правила";
            rulesButton.Click += (o, e) => { Sound.Play(SoundType.Button); rulesTask.Setup(); };
            result.Controls.Add(rulesButton);
            rulesButton.Move2Centr(75);

            return result;
        }

        override protected Button getButtonBack() {
            var result = base.getButtonBack();

            result.Text = "Выход";
            result.Click += (o, e) => Application.Exit();

            return result;
        }
    }
}
