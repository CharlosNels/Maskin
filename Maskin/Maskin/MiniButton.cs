using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
     partial class MiniButton : Control
    {
        public MiniButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor,true);
            BackColor = Color.Transparent;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Size = new Size(23, 23);
            ToolTip t = new ToolTip();
            t.UseAnimation = true;
            t.UseFading = true;
            t.InitialDelay = 400;
            t.SetToolTip(this, "最小化");
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
            if (isMouseDown||isMouseIn)
            {
                g.DrawLine(new Pen(Color.GhostWhite, lineWidth), new Point(5, 13), new Point(18, 13));
            }else
            {
                g.DrawLine(new Pen(Color.FromArgb(85,85,85), lineWidth), new Point(5, 13), new Point(18, 13));
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

    }
}
