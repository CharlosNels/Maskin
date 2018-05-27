using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinPlayerVolumeButton : PlayerButtonAppearance
    {
        public MaskinPlayerVolumeButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ToolTip t = new ToolTip();
            t.UseAnimation = true;
            t.UseFading = true;
            t.SetToolTip(this, "声音");
        }
        private GraphicsPath icon;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            icon = GetVolumeIcon(pe.ClipRectangle);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (isMouseDown)
            {
                g.FillPath(new SolidBrush(downColor), icon);
                if (silence)
                {
                    g.DrawLine(new Pen(downColor, 1), new Point( Width * 4 / 5,  Height / 3), new Point( Width,  Height * 2 / 3));
                    g.DrawLine(new Pen(downColor, 1), new Point( Width * 4 / 5,  Height * 2 / 3), new Point( Width,  Height / 3));
                }
            }
            else if (isMouseIn)
            {
                g.FillPath(new SolidBrush(onLineColor), icon);
                if (silence)
                {
                    g.DrawLine(new Pen(onLineColor, 1), new Point( Width * 4 / 5,  Height / 3), new Point( Width,  Height * 2 / 3));
                    g.DrawLine(new Pen(onLineColor, 1), new Point( Width * 4 / 5,  Height * 2 / 3), new Point( Width,  Height / 3));
                }
            }
            else
            {
                g.FillPath(new SolidBrush(lineColor), icon);
                if (silence)
                {
                    g.DrawLine(new Pen(lineColor, 1), new Point( Width * 4 / 5,  Height / 3), new Point( Width,  Height * 2 / 3));
                    g.DrawLine(new Pen(lineColor, 1), new Point( Width * 4 / 5,  Height * 2 / 3), new Point( Width,  Height / 3));
                }
            }
        }

        private GraphicsPath GetVolumeIcon(Rectangle rect)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(new Point[] { new Point(rect.Width/4,rect.Height/3),new Point(rect.Width/4, rect.Height*2/3),new Point(rect.Width/2, rect.Height*2/3),new Point(rect.Width*3/4,rect.Height*4/5),new Point(rect.Width*3/4, rect.Height/5),new Point(rect.Width/2, rect.Height/3),new Point(rect.Width /4, rect.Height/3) });
            return gp;
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

        public bool Silence
        {
            get
            {
                return silence;
            }

            set
            {
                silence = value;
                Refresh();
            }
        }

        private bool silence = false;
    }
}
