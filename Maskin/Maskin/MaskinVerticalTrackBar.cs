using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinVerticalTrackBar : PlayerButtonAppearance
    {
        public MaskinVerticalTrackBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        [Description("value改变之后发生")]
        public event EventHandler ValueChanged;
        public event EventHandler LoadValueChanged;

        private float value = 0.5f;
        private float loadValue = 0.6f;

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
                    ValueChanged?.Invoke(this, new EventArgs());
                }
                else
                {
                    if (value < 0)
                    {
                        this.value = 0;
                        Refresh();
                        ValueChanged?.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        this.value = 1;
                        Refresh();
                        ValueChanged?.Invoke(this, new EventArgs());
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

        public float LoadValue
        {
            get
            {
                return loadValue;
            }

            set
            {
                if (0 <= value && value <= 1)
                {
                    loadValue = value;
                    Refresh();
                    LoadValueChanged?.Invoke(this, new EventArgs());
                }
                else
                {
                    if (value < 0)
                    {
                        loadValue = 0;
                        Refresh();
                        LoadValueChanged?.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        loadValue = 1;
                        Refresh();
                        LoadValueChanged?.Invoke(this, new EventArgs());
                    }
                }
            }
        }
        

        private int lineWidth=1;
        private bool showPoint=true;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(loadProgressColor, lineWidth), new PointF(Width / 2, 0), new PointF(Width / 2, loadValue * Height));
            g.DrawLine(new Pen(OnLineColor, lineWidth), new PointF(Width/2, 0), new PointF(Width/2, value*Height));
            if (ShowPoint)
            {
                g.FillPath(new SolidBrush(downColor), Tools.CreateCircle(new Rectangle( Width / 2 - 2, (int)(value * Height - 2), 4, 4)));
            }
        }

        private Color loadProgressColor;

        public Color LoadProgressColor
        {
            set
            {
                loadProgressColor = value;
                Invalidate();
            }
            get
            {
                return loadProgressColor;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(LineColor, lineWidth), new PointF(Width/2,value*Height), new PointF( Width / 2,Height));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
            }
        }
        private bool isMouseDown = false;
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
                Value = (float)e.Y / Height;
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Value = (float)e.Y / Height;
        }
    }
}
