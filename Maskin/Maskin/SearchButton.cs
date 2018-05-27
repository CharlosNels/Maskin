using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class SearchButton : PlayerButtonAppearance
    {
        public SearchButton()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isMouseDown)
            {
                g.DrawArc(new Pen(downColor, 2), new Rectangle((int)(0.25*Width+1),(int)(0.25*Height+1), Width / 2, Height / 2 ), 0, 360);
                g.DrawLine(new Pen(downColor, 2), new Point((int)(0.75*Width),(int)(0.75*Height)), new Point(Width,Height));
            }
            else if (isMouseIn)
            {
                g.DrawArc(new Pen(onLineColor, 2), new Rectangle((int)(0.25 * Width + 1), (int)(0.25 * Height + 1), Width / 2, Height / 2), 0, 360);
                g.DrawLine(new Pen(onLineColor, 2), new Point((int)(0.75 * Width), (int)(0.75 * Height)), new Point(Width, Height));
            }
            else
            {
                g.DrawArc(new Pen(lineColor, 2), new Rectangle((int)(0.25 * Width + 1), (int)(0.25 * Height + 1), Width / 2, Height / 2), 0, 360);
                g.DrawLine(new Pen(lineColor, 2), new Point((int)(0.75 * Width), (int)(0.75 * Height)), new Point(Width, Height));
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
