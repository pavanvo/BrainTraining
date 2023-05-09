namespace BrainTraining.Controls {
    /// <summary>
    /// A panel which redraws its surface using a secondary buffer to reduce or prevent flicker.
    /// </summary>
    public class TableLayoutPanelDoubleBuffered : System.Windows.Forms.TableLayoutPanel {
        public TableLayoutPanelDoubleBuffered()
            : base() {
            this.DoubleBuffered = true;
        }
    }

}
