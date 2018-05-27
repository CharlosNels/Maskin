using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinPlayerPlayButton : PlayerButtonAppearance
    {
        public MaskinPlayerPlayButton()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            t.UseAnimation = true;
            t.UseFading = true;
            t.InitialDelay = 600;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            t.SetToolTip(this, Text);
        }

        ToolTip t = new ToolTip();
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            if (PlayState == NativeData.PlayState.Pause)
            {
                if (isMouseDown)
                {
                    g.FillPath(new SolidBrush(downColor), Tools.CreateTriangle(new Point((int)( Width / 3.0d), (int)( Height * 0.25d)), new Point((int)( Width / 4.0d * 3.0d), (int)( Height * 0.5d)), new Point((int)( Width / 3.0d), (int)( Width / 4.0d * 3.0d))));
                }
                else if (isMouseIn)
                {
                    g.FillPath(new SolidBrush(onLineColor), Tools.CreateTriangle(new Point((int)( Width / 3.0d), (int)( Height * 0.25d)), new Point((int)( Width / 4.0d * 3.0d), (int)( Height * 0.5d)), new Point((int)( Width / 3.0d), (int)( Width / 4.0d * 3.0d))));
                }
                else
                {
                    g.FillPath(new SolidBrush(lineColor), Tools.CreateTriangle(new Point((int)( Width / 3.0d), (int)( Height * 0.25d)), new Point((int)( Width / 4.0d * 3.0d), (int)( Height * 0.5d)), new Point((int)( Width / 3.0d), (int)( Width / 4.0d * 3.0d))));
                }
            }
            else
            {
                if (isMouseDown)
                {
                    g.FillPath(new SolidBrush(downColor),Tools.CreateRadianRectangle(new Rectangle((int)( Width/3.0d), (int)( Height*0.25d),(int)( Width/8.0d),(int)( Height*0.5d)), (int)( Width/16.0d)));
                    g.FillPath(new SolidBrush(downColor), Tools.CreateRadianRectangle(new Rectangle((int)(( Width -1)*2.0d/3.0d)- (int)( Width / 8.0d), (int)( Height * 0.25d), (int)( Width / 8.0d), (int)( Height * 0.5d)), (int)( Width / 16.0d)));
                }
                else if (isMouseIn)
                {
                    g.FillPath(new SolidBrush(onLineColor), Tools.CreateRadianRectangle(new Rectangle((int)( Width /3.0d), (int)( Height * 0.25d), (int)( Width / 8.0d), (int)( Height * 0.5d)), (int)( Width / 16.0d)));
                    g.FillPath(new SolidBrush(onLineColor), Tools.CreateRadianRectangle(new Rectangle((int)(( Width-1) *2.0d/3.0d)- (int)( Width / 8.0d), (int)( Height * 0.25d), (int)( Width / 8.0d), (int)( Height * 0.5d)), (int)( Width / 16.0d)));
                }
                else
                {
                    g.FillPath(new SolidBrush(lineColor), Tools.CreateRadianRectangle(new Rectangle((int)( Width /3.0d), (int)( Height * 0.25d), (int)( Width / 8.0d), (int)( Height * 0.5d)), (int)( Width / 16.0d)));
                    g.FillPath(new SolidBrush(lineColor), Tools.CreateRadianRectangle(new Rectangle((int)(( Width-1)*2.0d/3.0d)- (int)( Width / 8.0d), (int)( Height * 0.25d), (int)( Width / 8.0d), (int)( Height * 0.5d)), (int)( Width / 16.0d)));
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            if (isMouseDown||isMouseIn)
            {
                g.DrawArc(new Pen(OnLineColor, 1),new Rectangle(0,0,  Width-1, Height-1), 0, 360);
            }
            else
            {
                g.DrawEllipse(new Pen(LineColor, 1),new Rectangle(0, 0,  Width - 1,  Height - 1));
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
        private NativeData.PlayState plyState=NativeData.PlayState.Pause;
        
        public NativeData.PlayState PlayState
        {
            set
            {
                plyState = value;
                Refresh();
            }
            get
            {
                return plyState;
            }
        }
    }
}
