using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
     partial class MaxButton : Control
    {
        public MaxButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }
        ToolTip t = new ToolTip();
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Size = new Size(23, 23);
            t.UseAnimation = true;
            t.UseFading = true;
            t.InitialDelay = 400;
            t.SetToolTip(this, "最大化");
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
        private int lineWidth = 1;
        internal int LineWidth
        {
            set
            {
                lineWidth = value;
                Update();
            }
            get
            {
                return lineWidth;
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            if (isMaximized)
            {
                t.SetToolTip(this, "向下还原");
                if (isMouseDown||isMouseIn)
                {
                    g.DrawLines(new Pen(Color.GhostWhite, lineWidth), new Point[] { new Point(10, 9), new Point(10,7),new Point(16,7),new Point(16,13),new Point(14,13) });
                    g.DrawRectangle(new Pen(Color.GhostWhite, lineWidth), new Rectangle(8, 9, 6, 6));
                }
                else
                {
                    g.DrawLines(new Pen(Color.FromArgb(85,85,85), lineWidth), new Point[] { new Point(10, 9), new Point(10, 7), new Point(16, 7), new Point(16, 13), new Point(14, 13) });
                    g.DrawRectangle(new Pen(Color.FromArgb(85,85,85), lineWidth), new Rectangle(8, 9, 6, 6));
                }
            }
            else
            {
                t.SetToolTip(this, "最大化");
                if (isMouseDown||isMouseIn)
                {
                    g.DrawRectangle(new Pen(Color.GhostWhite, lineWidth), new Rectangle(6, 6, 10, 10));
                }
                else
                {
                    g.DrawRectangle(new Pen(Color.FromArgb(85, 85, 85), lineWidth), new Rectangle(6, 6, 10, 10));
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            if (isMouseDown)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(70, 70, 70)), pevent.ClipRectangle);
            }
            else if (isMouseIn)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128)), pevent.ClipRectangle);
            }
        }

        private bool isMaximized;

        public bool IsMaximized
        {
            get
            {
                return isMaximized;
            }

            set
            {
                isMaximized = value;
                Refresh();
            }
        }
    }
}
