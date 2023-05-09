using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BrainTraining.Controls {
    [System.Flags]
    public enum Corners {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }


    public enum CustomButtonState {
        Normal = 1,
        Hot,
        Pressed,
        Disabled,
        Focused
    }


    public class RoundButton : Control {
        public Corners Corners { get; set; }
        public Color BackColor2 { get; set; }
        public Color ButtonBorderColor { get; set; }

        public bool Toggle { get; set; }
        public bool Checked {
            get { return IsChecked; }
            set { IsChecked = value; this.Invalidate(); }
        }
        public int ButtonRoundRadius { get; set; }
        public int ButtonBorderWidth { get; set; }

        public Color ButtonHighlightColor { get; set; }
        public Color ButtonHighlightColor2 { get; set; }
        public Color ButtonHighlightForeColor { get; set; }

        public Color ButtonPressedColor { get; set; }
        public Color ButtonPressedColor2 { get; set; }
        public Color ButtonPressedForeColor { get; set; }

        private bool IsHighlighted;
        private bool IsPressed;
        private bool IsChecked;

        public RoundButton() {
            Size = new Size(100, 40);
            ButtonRoundRadius = 7;
            ButtonBorderWidth = 7;
            Toggle = false;
            IsChecked = false;
            Corners = Corners.All;
            BackColor = Color.Gainsboro;
            BackColor2 = Color.Silver;
            ButtonBorderColor = Helpers.ControlHelper.Blue;
            ButtonHighlightColor = Color.Orange;
            ButtonHighlightColor2 = Color.OrangeRed;
            ButtonHighlightForeColor = Color.Black;

            ButtonPressedColor = Color.Red;
            ButtonPressedColor2 = Color.Maroon;
            ButtonPressedForeColor = Color.White;
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return createParams;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) {

        }

        protected override void OnPaint(PaintEventArgs e) {
            try {
                base.OnPaint(e);
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                var current = Toggle ? IsChecked : IsPressed;

                var foreColor = current ? ButtonPressedForeColor : IsHighlighted ? ButtonHighlightForeColor : ForeColor;
                var backColor = current ? ButtonPressedColor : IsHighlighted ? ButtonHighlightColor : BackColor;
                var backColor2 = current ? ButtonPressedColor2 : IsHighlighted ? ButtonHighlightColor2 : BackColor2;

                if (!this.Enabled) {
                    backColor = Color.Silver;
                    backColor2 = Color.Silver;
                }
                var rect = ClientRectangle;
                rect.Inflate(-4, -4);

                using (var pen = new Pen(ButtonBorderColor, ButtonBorderWidth))
                    e.Graphics.DrawPath(pen, Path);

                using (var brush = new LinearGradientBrush(ClientRectangle, backColor, backColor2, LinearGradientMode.Vertical))
                    e.Graphics.FillPath(brush, Path);

                using (var brush = new SolidBrush(foreColor)) {
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    e.Graphics.DrawString(Text, Font, brush, rect, sf);
                }
                if (BackgroundImage != null) {
                    var minimal = Height < Width ? Height : Width;
                    var image = new Bitmap(BackgroundImage, new Size(minimal, minimal));
                    var point = new PointF((Width - minimal) / 2F, (Height - minimal) / 2F);
                    e.Graphics.DrawImage(image, point);
                }
            } catch { }
        }

        protected override void OnMouseEnter(EventArgs e) {
            try {
                base.OnMouseEnter(e);
                IsHighlighted = true;
                Parent.Invalidate(Bounds, false);
                Invalidate();
            } catch { }
        }

        protected override void OnMouseLeave(EventArgs e) {
            try {
                base.OnMouseLeave(e);
                IsHighlighted = false;
                IsPressed = false;
                Parent.Invalidate(Bounds, false);
                Invalidate();
            } catch { }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            try {
                base.OnMouseDown(e);
                Parent.Invalidate(Bounds, false);
                if (!IsChecked) IsChecked = true;
                Invalidate();
                IsPressed = true;
            } catch { }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            try {
                base.OnMouseUp(e);
                Parent.Invalidate(Bounds, false);
                Invalidate();
                IsPressed = false;
            } catch { }
        }

        protected GraphicsPath Path {
            get {
                var rect = ClientRectangle;
                rect.Inflate(-3, -ButtonBorderWidth);
                return GetRoundedRectangle(rect, Corners, ButtonRoundRadius);
            }
        }

        public static GraphicsPath GetRoundedRectangle(Rectangle rect, Corners corn, int roundradius) {
            var gp = new GraphicsPath();

            if (corn.HasFlag(Corners.TopLeft)) {
                gp.AddArc(rect.X, rect.Y, roundradius, roundradius, 180, 90);
            }
            else { gp.AddLine(rect.X, rect.Y, rect.X + rect.Width, rect.Y); }

            if (corn.HasFlag(Corners.TopRight)) {
                gp.AddArc(rect.X + rect.Width - roundradius, rect.Y, roundradius, roundradius, 270, 90);
            }
            else { gp.AddLine(rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height); }

            if (corn.HasFlag(Corners.BottomRight)) {
                gp.AddArc(rect.X + rect.Width - roundradius, rect.Y + rect.Height - roundradius, roundradius, roundradius, 0, 90);
            }
            else { gp.AddLine(rect.X + rect.Width, rect.Y + rect.Height, rect.X, rect.Y + rect.Height); }

            if (corn.HasFlag(Corners.BottomLeft)) {
                gp.AddArc(rect.X, rect.Y + rect.Height - roundradius, roundradius, roundradius, 90, 90);
            }
            else { gp.AddLine(rect.X, rect.Y + rect.Height, rect.X, rect.Y); }

            gp.CloseFigure();

            return gp;
        }
    }
}
