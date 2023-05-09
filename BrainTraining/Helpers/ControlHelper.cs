using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Helpers {
    internal static class ControlHelper {

        public static readonly Color Blue = Color.FromArgb(64, 176, 255);
        public static readonly Font SmallFont = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        public static readonly Font BigFont = new Font("Microsoft Sans Serif", 25.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        public static readonly int DefaultWidth = 500;

        public static void Move2Centr(this Control control, float topPercent) {
            control.Left = (control.Parent.Width - control.Width) / 2;
            control.Top = Convert.ToInt32(control.Parent.Height / 100f * topPercent);
        }

        public static void Move2Centr(this Control control) {
            control.Left = (control.Parent.Width - control.Width) / 2;
            control.Top = (control.Parent.Height - control.Height) / 2;
        }

        public static TableLayoutPanel GetTable(int colomns, int rows) {
            var table = new TableLayoutPanel {
                ColumnCount = colomns,
                RowCount = rows,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
            };
            for (int i = 0; i < colomns; i++) {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / colomns));
            }
            for (int i = 0; i < rows; i++) {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rows));
            }
            table.Size = new Size(DefaultWidth, DefaultWidth);

            return table;
        }
    }
}
