using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{

    partial class MaskinListBoxItem : PlayerButtonAppearance
    {
        public MaskinListBoxItem(Color line,Color onLine,Color down,string mainText,string content,object info,int width)
        {
            InitializeComponent();
            DoubleBuffered = true;              //双缓冲
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);                                 //支持透明
            Size = new Size(width,30);
            BackColor = Color.Transparent;
            lineColor = line;
            onLineColor = onLine;
            downColor = down;
            this.mainText = mainText;
            this.content = content;
            this.info = info;
            fadeIn.Interval = 15;               //动画的速度
            fadeOut.Interval = 15;              //同上
            fadeIn.Tick += fadeIn_Tick;
            fadeOut.Tick += fadeOut_Tick;
            Font = new Font("微软雅黑", 8, FontStyle.Regular);
        }

        private object info;
        
        internal object Info
        {
            get
            {
                return info;
            }
        }

        public string MainText
        {
            get
            {
                return mainText;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }
        }

        private void fadeIn_Tick(object sender,EventArgs e)
        {
            if (MainTextPoint.X<20)
            {
                MainTextPoint = new Point(MainTextPoint.X + 1, MainTextPoint.Y);
            }
            else
            {
                fadeIn.Stop();
            }
        }

        private void fadeOut_Tick(object sender,EventArgs e)
        {
            if (Selected)
            {
                return;

            }
            if (MainTextPoint.X>3)
            {
                MainTextPoint = new Point(MainTextPoint.X - 1, MainTextPoint.Y);
            }
            else
            {
                fadeOut.Stop();
            }
        }

        private string mainText="MainText", content="Content";

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            if (Selected)
            {
                g.FillRectangle(new SolidBrush(downColor), pe.ClipRectangle);
            }
            else if(isMouseIn)
            {
                g.FillRectangle(new SolidBrush(onLineColor), pe.ClipRectangle);
            }
            else
            {
                g.FillRectangle(new SolidBrush(lineColor), pe.ClipRectangle);
            }

            SizeF siz = g.MeasureString(content, Font);
            g.DrawString(mainText, Font, Brushes.Beige, mainTextPoint);
            g.DrawString(content, Font, Brushes.Azure, new Point((int)(Width - siz.Width - 3), 4));
        }
        private bool selected, isMouseIn;               //是否被选中，是否鼠标进入

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isMouseIn = true;
            fadeIn.Start();
            if (fadeOut.Enabled)
            {
                fadeOut.Stop();
            }
            if (TopLevelControl is MaskinForm)
            {
                ((MaskinForm)TopLevelControl).SetMouseEnter(e);
            }
            (Parent as MaskinListBox).setMouseEnter(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ((MaskinListBox)Parent).ItemClked(this);
            if (e.Button==MouseButtons.Right)
            {
                ((MaskinListBox)Parent).ItemRitClked(this);
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            ((MaskinListBox)Parent).ItemDoubleClked(this);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseIn = false;
            fadeOut.Start();
            if (fadeIn.Enabled)
            {
                fadeIn.Stop();
            }
        }

        Timer fadeIn = new Timer();
        Timer fadeOut = new Timer();

        public override Color LineColor
        {
            get
            {
                return lineColor;
            }

            set
            {
                lineColor = value;
                Invalidate();
            }
        }

        public Point MainTextPoint
        {
            get
            {
                return mainTextPoint;
            }

            set
            {
                mainTextPoint = value;
                Invalidate();
            }
        }

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
                Invalidate();
            }
        }
        private Point mainTextPoint = new Point(3, 4);
    }
}
