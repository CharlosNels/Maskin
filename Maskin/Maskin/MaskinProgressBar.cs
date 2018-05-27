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
    public partial class MaskinProgressBar : PlayerButtonAppearance
    {
        public MaskinProgressBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            LinearGradientBrush lgb = new LinearGradientBrush(new Point(0,Height/2),new Point(value*Width/100,Height/2),lineColor,onLineColor);
            g.FillRectangle(lgb, new Rectangle(0, 0, value * Width / 100, Height));
            g.FillRectangle(new SolidBrush(lineColor), new Rectangle(value * Width / 100, 0, Width - value * Width / 100, Height));
        }
        
        private int value = 50;
        public int Value
        {
            set
            {
                if (0<=value&&value<=100)
                {
                    this.value = value;
                }
                else
                {
                    if (value<0)
                    {
                        this.value = 0;
                    }
                    else
                    {
                        this.value = 100;
                    }
                }
                Refresh();
            }
            get
            {
                return value;
            }
        }
    }
}
