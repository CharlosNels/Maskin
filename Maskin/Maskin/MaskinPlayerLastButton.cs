using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinPlayerLastButton : PlayerButtonAppearance
    {
        public MaskinPlayerLastButton()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ToolTip t = new ToolTip();
            t.UseAnimation = true;
            t.UseFading = true;
            t.InitialDelay = 600;
            t.SetToolTip(this, "上一首");
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            if (isMouseDown)
            {
                g.FillPath(new SolidBrush(downColor), Tools.CreateTriangle(new Point((int)( Width- Width * 0.25)-2, (int)( Height * 0.25)), new Point((int)( Width- Width * 2.0d / 3.0d)-2, (int)( Height * 0.5)), new Point((int)( Width- Width * 0.25)-2, (int)( Height * 0.75))));
                g.FillPath(new SolidBrush(downColor), Tools.CreateRadianRectangle(new Rectangle((int)( Width- Width * 2.0d / 3.0d)-4, (int)( Height * 0.25), (int)( Width * 0.75 -  Width * 2.0 / 3.0), (int)( Height * 0.5d)), (int)(( Width * 0.75 -  Width * 2.0 / 3.0) * 0.5d)));
            }
            else if (isMouseIn)
            {
                g.FillPath(new SolidBrush(onLineColor), Tools.CreateTriangle(new Point((int)( Width -  Width * 0.25)-2, (int)( Height * 0.25)), new Point((int)( Width -  Width * 2.0d / 3.0d)-2, (int)( Height * 0.5)), new Point((int)( Width -  Width * 0.25)-2, (int)( Height * 0.75))));
                g.FillPath(new SolidBrush(onLineColor), Tools.CreateRadianRectangle(new Rectangle((int)( Width -  Width * 2.0d / 3.0d)-4, (int)( Height * 0.25), (int)( Width * 0.75 -  Width * 2.0 / 3.0), (int)( Height * 0.5d)), (int)(( Width * 0.75 -  Width * 2.0 / 3.0) * 0.5d)));
            }
            else
            {
                g.FillPath(new SolidBrush(lineColor), Tools.CreateTriangle(new Point((int)( Width -  Width * 0.25)-2, (int)( Height * 0.25)), new Point((int)( Width -  Width * 2.0d / 3.0d)-2, (int)( Height * 0.5)), new Point((int)( Width -  Width * 0.25)-2, (int)( Height * 0.75))));
                g.FillPath(new SolidBrush(lineColor), Tools.CreateRadianRectangle(new Rectangle((int)(Width - Width * 2.0d / 3.0d)-4, Height /4, Width * 3/4 - Width * 2 / 3, Height /2), (Width * 3/4 - Width * 2 / 3) /2));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            if (isMouseDown || isMouseIn)
            {
                g.DrawEllipse(new Pen(onLineColor, 1), new Rectangle(0, 0,  Width - 1,  Height - 1));
            }
            else
            {
                g.DrawEllipse(new Pen(lineColor, 1), new Rectangle(0, 0,  Width - 1,  Height - 1));
            }
        }

        private bool isMouseDown, isMouseIn;
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
        
    }
}
