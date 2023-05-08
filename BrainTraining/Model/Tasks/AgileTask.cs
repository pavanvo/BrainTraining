using BrainTraining.Model.UI;
using System.Windows.Forms;

namespace BrainTraining.Model.Tasks {
    internal class AgileTask : BaseTask {
        SelectTask Menu { get; set; }

        public AgileTask(SelectTask menu) : base(menu.MainForm) {
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
            result.Click += (o, e) => { Sound.Play(SoundType.Button); Menu.Setup(); };

            return result;
        }
    }
}
