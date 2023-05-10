using BrainTraining.Properties;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrainTraining.Helpers {
    internal static class ControlHelper {

        static PrivateFontCollection pfc = AddFontFromMemory();

        static private PrivateFontCollection AddFontFromMemory() {
            var result = new PrivateFontCollection();
            var fontBytes =Resources.AlumniSansPinstripe_Regular;
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            result.AddMemoryFont(fontData, fontBytes.Length);
            return result;
        }

        public static readonly Color Blue = Color.FromArgb(64, 176, 255);
        public static readonly Color Orange = Color.OrangeRed;
        public static readonly Font SmallFont = new Font(pfc.Families[0], 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        public static readonly Font BigFont = new Font(pfc.Families[0], 25.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
        public static readonly Font BiggerFont = new Font(pfc.Families[0], 30.25F, FontStyle.Regular, GraphicsUnit.Point, 204);

        public const int DEFAULT_WIDTH = 500;
        public const string APP_NAME = "Brain Training";
        public const string RESULT_GOOD = "Поздравляем, вы прошли задание. \r\n" + RESULT;
        public const string RESULT_BAD = "Ответ неверный. \r\n" + RESULT;
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
