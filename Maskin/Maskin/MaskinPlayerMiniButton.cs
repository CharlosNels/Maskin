using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    partial class MaskinPlayerMiniButton : Control
    {
        public MaskinPlayerMiniButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ToolTip t = new ToolTip();
            Size = new Size(23, 23);
            t.UseAnimation = true;
            t.UseFading = true;
            t.SetToolTip(this, "mini模式");
        }
        private bool isMouseIn, isMouseDown;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = true;
            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
            Refresh();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isMouseIn = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseIn = false;
            Refresh();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            if (isMouseDown)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(75, 75, 75)), pevent.ClipRectangle);
            }
            else if (isMouseIn)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128)), pevent.ClipRectangle);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isMouseDown || isMouseIn)
            {
                g.DrawLines(new Pen(Color.GhostWhite, 1), new Point[] { new Point(5, 10), new Point(5, 5), new Point(17, 5), new Point(17, 17), new Point(12, 17) });
                g.FillPath(new SolidBrush(Color.GhostWhite), Tools.CreateTriangle());
                g.DrawLine(new Pen(Color.GhostWhite, 1), new Point(15, 7), new Point(7, 15));
            }
            else
            {
                g.DrawLines(new Pen(Color.FromArgb(85,85,85), 1), new Point[] { new Point(5, 10), new Point(5, 5), new Point(17, 5), new Point(17, 17), new Point(12, 17) });
                g.FillPath(new SolidBrush(Color.FromArgb(85,85,85)), Tools.CreateTriangle());
                g.DrawLine(new Pen(Color.FromArgb(85,85,85), 1), new Point(15, 7), new Point(7, 15));
            }
        }
    }
}
