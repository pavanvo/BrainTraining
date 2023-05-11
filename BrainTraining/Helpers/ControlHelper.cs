using BrainTraining.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrainTraining.Helpers {
    /// <summary>
    /// Варианты Фигур
    /// </summary>
    public enum Figure {
        Trigangle,
        Square,
        Circle,
    }

    /// <summary>
    /// Статический класс для работы с элементами управления
    /// </summary>
    internal static class ControlHelper {

        private static PrivateFontCollection pfc = AddFontFromMemory();

        private static PrivateFontCollection AddFontFromMemory() {
            var result = new PrivateFontCollection();
            var fontBytes = Resources.AlumniSansPinstripe_Regular;
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            result.AddMemoryFont(fontData, fontBytes.Length);
            return result;
        }

        /// <summary>
        /// Константы
        /// </summary>
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

        /// <summary>
        /// Словарь Фигур
        /// </summary>
        private static readonly Dictionary<Figure, Action<PaintEventArgs, Color>> Figures = new Dictionary<Figure, Action<PaintEventArgs, Color>>() {
            { Figure.Trigangle, (e,c) => e.FillTriangle(c) },
            { Figure.Square,(e, c) => e.FillSquare(c) },
            { Figure.Circle,(e, c) => e.FillCircle(c) },
        };

        /// <summary>
        /// Отрисовка фигуры
        /// </summary>
        public static void DrawFigure(this Control control, Figure figura, Color color) {
            control.Paint += (o, e) => Figures[figura](e, color);
            control.Refresh();
        }

        /// <summary>
        /// переместить элементам управления на центр
        /// </summary>
        public static void Move2Centr(this Control control, float topPercent) {
            control.Left = (control.Parent.Width - control.Width) / 2;
            control.Top = Convert.ToInt32(control.Parent.Height / 100f * topPercent);
        }

        /// <summary>
        /// переместить элементам управления на центр
        /// </summary>
        public static void Move2Centr(this Control control) {
            control.Left = (control.Parent.Width - control.Width) / 2;
            control.Top = (control.Parent.Height - control.Height) / 2;
        }

        /// <summary>
        /// Получить Таблицу
        /// </summary>
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
