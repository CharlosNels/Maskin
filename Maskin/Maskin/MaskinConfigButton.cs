using System;
using System.Drawing;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinConfigButton : PlayerButtonAppearance
    {
        public MaskinConfigButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            ToolTip t = new ToolTip();
            t.InitialDelay = 400;
            t.SetToolTip(this, "设置");
        }

        private Color normalColor = Color.Black;
        private Color onColor = Color.FromArgb(85,85,85);

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isMouseDown || isMouseIn)
            {
                g.DrawArc(new Pen(Color.Beige, 3), new Rectangle(7, 7, Width - 14, Height - 14), 0, 360);
            }
            else
            {
                g.DrawArc(new Pen(Color.FromArgb(75,75,75), 3), new Rectangle(7, 7, Width - 14, Height - 14), 0, 360);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isMouseDown)
            {
                g.FillRectangle(new SolidBrush(downColor), new Rectangle(0, 0, Width, Height));
            }
            else if (isMouseIn)
            {
                g.FillRectangle(new SolidBrush(onColor), new Rectangle(0, 0, Width, Height));
            }
        }
        private bool isMouseIn, isMouseDown;
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isMouseIn = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseIn = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
            Invalidate();
        }
    }
}