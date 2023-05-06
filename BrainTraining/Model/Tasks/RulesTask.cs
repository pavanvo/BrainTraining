using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class RulesTask : BaseTask {

        SelectTask Menu { get; set; }

        public RulesTask(SelectTask menu) : base(menu.MainForm) {
            Menu = menu;

            Setup = () => MainForm.SetupTask(this);

        }

        override protected Panel getHeader() {
            var result = base.getHeader();

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

        override protected Button getButtonBack() {
            var result = base.getButtonBack();

            result.Text = "В меню";
            result.Click += (o, e) => Menu.Setup();

            return result;
        }
    }
}
