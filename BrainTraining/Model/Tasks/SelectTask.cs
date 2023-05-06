using BrainTraining.Helpers;
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
            return new Button {
                Height = parent.Height / 8,
                Width = parent.Width / 2,
                Font = new Font("Microsoft Sans Serif", 25.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
            };
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

            var momoryTask = new MemoryTask(this);
            var memory = getButton(result);
            memory.Text = "Память";
            memory.Click += (o, e) => momoryTask.Setup();
            result.Controls.Add(memory);
            memory.Move2Centr(0);

            var agile = getButton(result);
            agile.Text = "Гибкость";
            agile.Click += (o, e) => momoryTask.Setup();
            result.Controls.Add(agile);
            agile.Move2Centr(25);

            var speed = getButton(result);
            speed.Text = "Скорость";
            speed.Click += (o, e) => momoryTask.Setup();
            result.Controls.Add(speed);
            speed.Move2Centr(50);

            var rules = getButton(result);
            rules.Text = "Правила";
            rules.Click += (o, e) => momoryTask.Setup();
            result.Controls.Add(rules);
            rules.Move2Centr(75);

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
