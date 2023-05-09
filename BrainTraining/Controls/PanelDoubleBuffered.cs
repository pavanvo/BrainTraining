using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainTraining.Controls {
    /// <summary>
    /// A panel which redraws its surface using a secondary buffer to reduce or prevent flicker.
    /// </summary>
    public class PanelDoubleBuffered : System.Windows.Forms.Panel {
        public PanelDoubleBuffered()
            : base() {
            this.DoubleBuffered = true;
        }
    }
}
