using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Helpers {

    /// <summary>
    /// Статический класс для отрисовкина элементах управления
    /// </summary>
    public static class GraphicsHelper {

        /// <summary>
        /// Заливка квадрата
        /// </summary>
        public static void FillSquare(this PaintEventArgs e, Color color) {
            var rect = e.ClipRectangle;
            var minimal = rect.Height < rect.Width ? rect.Height : rect.Width;
            e.Graphics.FillRectangle(new SolidBrush(color), (rect.Width - minimal) / 2F, (rect.Height - minimal) / 2F, minimal, minimal);
        }

        /// <summary>
        /// Заливка Треугольника
        /// </summary>
        public static void FillTriangle(this PaintEventArgs e, Color color) {
            var rect = e.ClipRectangle;
            var minimal = rect.Height < rect.Width ? rect.Height : rect.Width;
            FillTriangle(e, new Point(rect.Width / 2, 0), minimal, color);
        }

        /// <summary>
        /// Заливка Треугольника
        /// </summary>
        public static void FillTriangle(this PaintEventArgs e, Point p, int size, Color color) {
            e.Graphics.FillPolygon(new SolidBrush(color), new Point[] { p, new Point(p.X - size, p.Y + (int)(size * Math.Sqrt(3))), new Point(p.X + size, p.Y + (int)(size * Math.Sqrt(3))) });
        }

        /// <summary>
        /// Заливка Круга
        /// </summary>
        public static void FillCircle(this PaintEventArgs e, Color color) {
            var rect = e.ClipRectangle;
            var minimal = rect.Height < rect.Width ? rect.Height : rect.Width;
            e.Graphics.FillCircle(new SolidBrush(color), rect.Width / 2F, rect.Height / 2F, minimal / 2F);
        }

        /// <summary>
        /// Отрисовка Круга
        /// </summary>
        public static void DrawCircle(this Graphics g, Pen pen,
                                  float centerX, float centerY, float radius) {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        /// <summary>
        /// Заливка Круга
        /// </summary>
        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius) {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }
    }
}
