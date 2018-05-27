using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class TextButton : PlayerButtonAppearance
    {
        public TextButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("微软雅黑", 26);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics ;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SizeF siz = g.MeasureString(Text, Font);
            PointF p = new PointF((Width - siz.Width) / 2, (Height - siz.Height));
            if (isMouseDown)
            {
                g.DrawString(Text, Font, new SolidBrush(downColor), p);
            }
            else if (isMouseEnter)
            {
                g.DrawString(Text, Font, new SolidBrush(onLineColor), p);
            }
            else
            {
                g.DrawString(Text, Font, new SolidBrush(lineColor), p);
            }
        }

        private bool isMouseDown, isMouseEnter;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isMouseEnter = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseEnter = false;
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
