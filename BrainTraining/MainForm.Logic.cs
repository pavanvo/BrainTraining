using BrainTraining.Helpers;
using BrainTraining.Model;

namespace BrainTraining {
    public partial class MainForm {

        public void SetupTask(ITask task) {
            SuspendLayout();

            Controls.Remove(Footer);
            Controls.Remove(Content);
            Controls.Remove(Header);

            Header = task.Header;
            Content = task.Content;
            Footer = task.Footer;

            Controls.Add(Footer);
            Controls.Add(Content);
            Controls.Add(Header);

            Header.Move2Centr(0);
            Content.Move2Centr(task.HeaderHeight);
            Footer.Move2Centr(task.HeaderHeight + task.ContentHeight);

            ResumeLayout(false);
        }
    }
}
