using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinPlayerLoopButton : PlayerButtonAppearance
    {
        public MaskinPlayerLoopButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        private ToolTip t = new ToolTip();
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            t.UseAnimation = true;
            t.UseFading = true;
        }

        public event EventHandler LoopWayChanged;
        

        public NativeData.LoopState Loop
        {
            get
            {
                return loop;
            }

            set
            {
                loop = value;
                Refresh();
                if (LoopWayChanged!=null)
                {
                    LoopWayChanged(this, new EventArgs());
                }
            }
        }

        private NativeData.LoopState loop=NativeData.LoopState.ListCycle;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SizeF siz = g.MeasureString("1", new Font("微软雅黑", 15));
            switch (loop)
            {
                case NativeData.LoopState.SingleCycle:
                    if (isMouseDown)
                    {
                        g.DrawLines(new Pen(downColor, 2), new Point[] { new Point( Width - 3,  Height / 2 - 3), new Point( Width - 3, 3), new Point(3, 3), new Point(3,  Height / 2 - 3) });
                        g.FillPath(new SolidBrush(downColor), Tools.CreateTriangle(new Point(1,  Height / 2 - 3), new Point(5,  Height / 2 - 3), new Point(2,  Height / 2+2)));
                        g.DrawLines(new Pen(downColor, 2), new Point[] { new Point(3, Height/2+3),new Point(3, Height-3),new Point( Width-3, Height-3),new Point( Width-3, Height/2+3)});
                        g.FillPath(new SolidBrush(downColor), Tools.CreateTriangle(new Point( Width - 1,  Height / 2 + 3), new Point( Width - 5,  Height / 2 + 3), new Point( Width - 3,  Height / 2 - 2)));
                        g.DrawString("1", new Font("微软雅黑", 10), new SolidBrush(downColor), new Point((int)( Width/2-siz.Width/2)+4,(int)( Height/2-siz.Height/2)+4));
                    }
                    else if(isMouseIn)
                    {
                        g.DrawLines(new Pen(onLineColor, 2), new Point[] { new Point( Width - 3,  Height / 2 - 3), new Point( Width - 3, 3), new Point(3, 3), new Point(3,  Height / 2 - 3) });
                        g.FillPath(new SolidBrush(onLineColor), Tools.CreateTriangle(new Point(1,  Height / 2 - 3), new Point(5,  Height / 2 - 3), new Point(2,  Height / 2 + 2)));
                        g.DrawLines(new Pen(onLineColor, 2), new Point[] { new Point(3,  Height / 2 + 3), new Point(3,  Height - 3), new Point( Width - 3,  Height - 3), new Point( Width - 3,  Height / 2 + 3) });
                        g.FillPath(new SolidBrush(onLineColor), Tools.CreateTriangle(new Point( Width - 1,  Height / 2 + 3), new Point( Width - 5,  Height / 2 + 3), new Point( Width - 3,  Height / 2 - 2)));
                        g.DrawString("1", new Font("微软雅黑", 10), new SolidBrush(onLineColor), new Point((int)( Width / 2 - siz.Width / 2)+4, (int)( Height / 2 - siz.Height / 2)+4));
                    }
                    else
                    {
                        g.DrawLines(new Pen(lineColor, 2), new Point[] { new Point( Width - 3,  Height / 2 - 3), new Point( Width - 3, 3), new Point(3, 3), new Point(3,  Height / 2 - 3) });
                        g.FillPath(new SolidBrush(lineColor), Tools.CreateTriangle(new Point(1,  Height / 2 - 3), new Point(5,  Height / 2 - 3), new Point(2,  Height / 2 + 2)));
                        g.DrawLines(new Pen(lineColor, 2), new Point[] { new Point(3,  Height / 2 + 3), new Point(3,  Height - 3), new Point( Width - 3,  Height - 3), new Point( Width - 3,  Height / 2 + 3) });
                        g.FillPath(new SolidBrush(lineColor), Tools.CreateTriangle(new Point( Width - 1,  Height / 2 + 3), new Point( Width - 5,  Height / 2 + 3), new Point( Width - 3,  Height / 2 - 2)));
                        g.DrawString("1", new Font("微软雅黑", 10), new SolidBrush(lineColor), new Point((int)( Width / 2 - siz.Width / 2)+4, (int)( Height / 2 - siz.Height / 2)+4));
                    }
                    break;
                case NativeData.LoopState.ListCycle:
                    if (isMouseDown)
                    {
                        g.DrawLines(new Pen(downColor, 2), new Point[] { new Point( Width - 3,  Height / 2 - 3), new Point( Width - 3, 3), new Point(3, 3), new Point(3,  Height / 2 - 3) });
                        g.FillPath(new SolidBrush(downColor), Tools.CreateTriangle(new Point(1,  Height / 2 - 3), new Point(5,  Height / 2 - 3), new Point(2,  Height / 2 + 2)));
                        g.DrawLines(new Pen(downColor, 2), new Point[] { new Point(3,  Height / 2 + 3), new Point(3,  Height - 3), new Point( Width - 3,  Height - 3), new Point( Width - 3,  Height / 2 + 3) });
                        g.FillPath(new SolidBrush(downColor), Tools.CreateTriangle(new Point( Width - 1,  Height / 2 + 3), new Point( Width - 5,  Height / 2 + 3), new Point( Width - 3,  Height / 2 - 2)));
                    }
                    else if (isMouseIn)
                    {
                        g.DrawLines(new Pen(onLineColor, 2), new Point[] { new Point( Width - 3,  Height / 2 - 3), new Point( Width - 3, 3), new Point(3, 3), new Point(3,  Height / 2 - 3) });
                        g.FillPath(new SolidBrush(onLineColor), Tools.CreateTriangle(new Point(1,  Height / 2 - 3), new Point(5,  Height / 2 - 3), new Point(2,  Height / 2 + 2)));
                        g.DrawLines(new Pen(onLineColor, 2), new Point[] { new Point(3,  Height / 2 + 3), new Point(3,  Height - 3), new Point( Width - 3,  Height - 3), new Point( Width - 3,  Height / 2 + 3) });
                        g.FillPath(new SolidBrush(onLineColor), Tools.CreateTriangle(new Point( Width - 1,  Height / 2 + 3), new Point( Width - 5,  Height / 2 + 3), new Point( Width - 3,  Height / 2 - 2)));
                    }
                    else
                    {
                        g.DrawLines(new Pen(lineColor, 2), new Point[] { new Point( Width - 3,  Height / 2 - 3), new Point( Width - 3, 3), new Point(3, 3), new Point(3,  Height / 2 - 3) });
                        g.FillPath(new SolidBrush(lineColor), Tools.CreateTriangle(new Point(1,  Height / 2 - 3), new Point(5,  Height / 2 - 3), new Point(2,  Height / 2 + 2)));
                        g.DrawLines(new Pen(lineColor, 2), new Point[] { new Point(3,  Height / 2 + 3), new Point(3,  Height - 3), new Point( Width - 3,  Height - 3), new Point( Width - 3,  Height / 2 + 3) });
                        g.FillPath(new SolidBrush(lineColor), Tools.CreateTriangle(new Point( Width - 1,  Height / 2 + 3), new Point( Width - 5,  Height / 2 + 3), new Point( Width - 3,  Height / 2 - 2)));
                    }
                    break;
                case NativeData.LoopState.RandomPlay:
                    if (isMouseDown)
                    {
                        g.DrawLines(new Pen(downColor, 2), new Point[] { new Point(2, Height/3),new Point( Width/2, Height/3),new Point( Width-2, Height*2/3) });
                        g.DrawLines(new Pen(downColor, 2), new Point[] { new Point(2,  Height * 2 / 3), new Point( Width / 2,  Height * 2 / 3), new Point( Width - 2,  Height / 3) });
                    }
                    else if (isMouseIn)
                    {
                        g.DrawLines(new Pen(onLineColor, 2), new Point[] { new Point(2,  Height / 3), new Point( Width / 2,  Height / 3), new Point( Width - 2,  Height * 2 / 3) });
                        g.DrawLines(new Pen(onLineColor, 2), new Point[] { new Point(2,  Height * 2 / 3), new Point( Width / 2,  Height * 2 / 3), new Point( Width - 2,  Height / 3) });
                    }
                    else
                    {
                        g.DrawLines(new Pen(lineColor, 2), new Point[] { new Point(2,  Height / 3), new Point( Width / 2,  Height / 3), new Point( Width -2,  Height * 2 / 3 ) });
                        g.DrawLines(new Pen(lineColor, 2), new Point[] { new Point(2,  Height * 2 / 3), new Point( Width / 2,  Height * 2 / 3), new Point( Width -2,  Height / 3 ) });
                    }
                    break;
            }
        }
        private bool isMouseDown;
        private bool isMouseIn;
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
