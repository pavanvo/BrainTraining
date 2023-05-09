using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Helpers {
    internal static class ControlHelper {

        public static readonly Color Blue = Color.FromArgb(64, 176, 255);
        public static readonly Color Orange = Color.OrangeRed;
        public static readonly Font SmallFont = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        public static readonly Font BigFont = new Font("Segoe Script", 25.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
        public static readonly Font BiggerFont = new Font("Segoe UI", 30.25F, FontStyle.Regular, GraphicsUnit.Point, 204);

        public const int DEFAULT_WIDTH = 500;
        public const string APP_NAME = "Brain Training";
        public const string RESULT_GOOD = "Поздравляем, вы прошли задание. " + RESULT;
        public const string RESULT_BAD = "Ответ неверный. " + RESULT;
        public const string RESULT = "Результат: ";

        public static void Move2Centr(this Control control, float topPercent) {
            control.Left = (control.Parent.Width - control.Width) / 2;
            control.Top = Convert.ToInt32(control.Parent.Height / 100f * topPercent);
        }

        public static void Move2Centr(this Control control) {
            control.Left = (control.Parent.Width - control.Width) / 2;
            control.Top = (control.Parent.Height - control.Height) / 2;
        }

        public static TableLayoutPanel GetTable(int colomns, int rows) {
            var table = new Controls.TableLayoutPanelDoubleBuffered {
                ColumnCount = colomns,
                RowCount = rows,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble,
            };
            for (int i = 0; i < colomns; i++) {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / colomns));
            }
            for (int i = 0; i < rows; i++) {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rows));
            }
            table.Size = new Size(DEFAULT_WIDTH, DEFAULT_WIDTH);

            return table;
        }
    }
}
