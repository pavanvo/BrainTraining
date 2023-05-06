using BrainTraining.Helpers;
using BrainTraining.Model;

namespace BrainTraining {
    public partial class MainForm {

        public void SetupTask(ITask task) {
            SuspendLayout();

            Controls.Remove(Footer);
            Controls.Remove(Content);
            Controls.Remove(Header);
            Controls.Remove(ButtonBack);

            Header = task.Header;
            Content = task.Content;
            Footer = task.Footer;
            ButtonBack = task.ButtonBack;

            Controls.Add(Footer);
            Controls.Add(Content);
            Controls.Add(Header);
            Controls.Add(ButtonBack);

            Header.Move2Centr(0);
            Content.Move2Centr(task.HeaderHeight);
            Footer.Move2Centr(task.HeaderHeight + task.ContentHeight);

            ResumeLayout(false);
        }
    }
}
