using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Maskin
{
    public partial class MaskinCircleTrackBar : PlayerButtonAppearance
    {
        public MaskinCircleTrackBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Size = new Size(100, 100);
            BackColor = Color.Transparent;
        }
        public event EventHandler ValueChanged;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawArc(new Pen(downColor,5), new Rectangle(2,2,Width-5,Height-5),180, value*360);
            g.DrawArc(new Pen(lineColor, 5), new Rectangle(2, 2, Width - 5, Height - 5),value*360+180, 360-value*360);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(lineColor, 2), new Point(Width/5, Height / 2), new Point(Width - Width/5, Height / 2));
            SizeF siz = g.MeasureString(UpText, new Font("微软雅黑", 12, FontStyle.Regular));
            SizeF siz1 = g.MeasureString(downText, new Font("微软雅黑", 12, FontStyle.Regular));
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            g.DrawString(UpText, new Font("微软雅黑", 12, FontStyle.Regular),new SolidBrush(onLineColor),new Point((Width-(int)siz.Width)/2,Height/4));
            g.DrawString(DownText, new Font("微软雅黑", 12, FontStyle.Regular), new SolidBrush(onLineColor), new Point((Width - (int)siz1.Width) / 2, Height * 3 / 4-(int)siz1.Height));
        }

        private float value;

        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                if (0 <= value && value <= 1)
                {
                    this.value = value;
                    Refresh();
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, new EventArgs());
                    }
                }
                else
                {
                    if (value < 0)
                    {
                        this.value = 0;
                        Refresh();
                        if (ValueChanged != null)
                        {
                            ValueChanged(this, new EventArgs());
                        }
                    }
                    else
                    {
                        this.value = 1;
                        Refresh();
                        if (ValueChanged != null)
                        {
                            ValueChanged(this, new EventArgs());
                        }
                    }
                }
            }
        }
        public string UpText
        {
            get
            {
                return upText;
            }

            set
            {
                upText = value;
                Refresh();
            }
        }

        public string DownText
        {
            get
            {
                return downText;
            }

            set
            {
                downText = value;
                Refresh();
            }
        }

        private bool isMouseDown;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = true;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Location.X<=Width/4||e.Location.X>=Width*3/4||e.Y<=Height/4||e.Y>=Height*3/4)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
            if (isMouseDown &&Cursor==Cursors.Hand)
            {
                if (e.X>=Width/2&&e.Y>=Height/2)//3
                {
                    Value = (float)((Tools.RadianToDegree(Math.Atan2(e.Y-Height/2,e.X-Width/2))+180)/360d);
                }
                else if (e.X<=Width/2&&e.Y>Height/2)           //4
                {
                    Value = (float)((360-Tools.RadianToDegree(Math.Atan2(e.Y - Height / 2d, Width / 2d - e.X)))/360d);
                }
                else if (e.X<=Width/2&&e.Y<=Height/2)                     //1
                {
                    Value = (float)(Tools.RadianToDegree(Math.Atan2(Height / 2 - e.Y, Width / 2 - e.X)) /360d);
                }
                else                               //2
                {
                    Value = (float)((180-Tools.RadianToDegree(Math.Atan2(Height / 2 - e.Y, e.X - Width / 2)))/360d);
                }
            }
        }
        private string upText,downText;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (isMouseDown && Cursor == Cursors.Hand)
            {
                if (e.X >= Width / 2 && e.Y >= Height / 2)//3
                {
                    Value = (float)((Tools.RadianToDegree(Math.Atan2(e.Y - Height / 2, e.X - Width / 2)) + 180) / 360d);
                }
                else if (e.X <= Width / 2 && e.Y > Height / 2)           //4
                {
                    Value = (float)((360 - Tools.RadianToDegree(Math.Atan2(e.Y - Height / 2d, Width / 2d - e.X))) / 360d);
                }
                else if (e.X <= Width / 2 && e.Y <= Height / 2)                     //1
                {
                    Value = (float)(Tools.RadianToDegree(Math.Atan2(Height / 2 - e.Y, Width / 2 - e.X)) / 360d);
                }
                else                               //2
                {
                    Value = (float)((180 - Tools.RadianToDegree(Math.Atan2(Height / 2 - e.Y, e.X - Width / 2))) / 360d);
                }
            }
        }
    }
}
