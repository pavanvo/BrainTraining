using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrainTraining.Helpers {
    public static class GraphicsHelper {


        public static void FillSquare(this PaintEventArgs e, Color color) {
            var rect = e.ClipRectangle;
            var minimal = rect.Height < rect.Width ? rect.Height : rect.Width;
            e.Graphics.FillRectangle(new SolidBrush(color), (rect.Width - minimal)/ 2F, (rect.Height - minimal) / 2F, minimal, minimal);
        }

        public static void FillTriangle(this PaintEventArgs e, Color color) {
            var rect = e.ClipRectangle;
            var minimal = rect.Height < rect.Width ? rect.Height : rect.Width;
            FillTriangle(e, new Point(rect.Width / 2, 0), minimal, color);
        }

        public static void FillTriangle(this PaintEventArgs e, Point p, int size, Color color) {
            e.Graphics.FillPolygon(new SolidBrush(color), new Point[] { p, new Point(p.X - size, p.Y + (int)(size * Math.Sqrt(3))), new Point(p.X + size, p.Y + (int)(size * Math.Sqrt(3))) });
        }

        public static void FillCircle(this PaintEventArgs e, Color color) {
            var rect = e.ClipRectangle;
            var minimal = rect.Height < rect.Width ? rect.Height : rect.Width;
            e.Graphics.FillCircle(new SolidBrush(color), rect.Width / 2F, rect.Height / 2F, minimal / 2F);
        }

        public static void DrawCircle(this Graphics g, Pen pen,
                                  float centerX, float centerY, float radius) {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius) {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }
    }
}
