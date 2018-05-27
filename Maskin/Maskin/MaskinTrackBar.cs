using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinTrackBar : PlayerButtonAppearance
    {
        public MaskinTrackBar()
        {
            InitializeComponent();
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        [Description("value改变之后发生")]
        public event EventHandler ValueChanged;

        private float value=0.5f;

        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                if (0<=value&&value<=1)
                {
                    this.value = value;
                    Refresh();
                    if (ValueChanged!=null)
                    {
                        ValueChanged(this, new EventArgs());
                    }
                }
                else
                {
                    if (value<0)
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

        public int LineWidth
        {
            get
            {
                return lineWidth;
            }

            set
            {
                lineWidth = value;
                Refresh();
            }
        }
        

        public bool ShowPoint
        {
            get
            {
                return showPoint;
            }

            set
            {
                showPoint = value;
                Refresh();
            }
        }
        
        private int lineWidth;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(OnLineColor, lineWidth), new PointF(0,  Height / 2), new PointF( value *  Width,  Height / 2));
            if (ShowPoint)
            {
                g.FillPath(new SolidBrush(downColor), Tools.CreateCircle(new Rectangle((int)(value *  Width - 2),  Height / 2 - 2, 4, 4)));
            }
        }

        private bool showPoint = true;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g= pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(LineColor, lineWidth), new PointF(value *  Width,  Height / 2), new PointF( Width,  Height / 2));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button==MouseButtons.Left)
            {
                isMouseDown = true;
            }
        }
        private bool isMouseDown=false;
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMouseDown)
            {
                Value =(float)e.X/ Width;
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Value = (float)e.X / Width;
        }
    }
}
