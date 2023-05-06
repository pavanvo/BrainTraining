using System;
using System.Windows.Forms;

namespace BrainTraining.Helpers {
    internal static class ControlHelper {
        public static void Move2Centr(this Control control, float topPercent) {
            control.Left = (control.Parent.Width - control.Width) / 2;
            control.Top = Convert.ToInt32(control.Parent.Height / 100f * topPercent);
        }
    }
}
