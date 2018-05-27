using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinLabel : PlayerButtonAppearance
    {
        public MaskinLabel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("微软雅黑", 9, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SizeF  siz= g.MeasureString(text,Font);
            g.DrawString(text, Font, new SolidBrush(Color.Beige), new Point((int)((Width-siz.Width)/2),(int)((Height-siz.Height)/2)));
        }

        private string text;

        public override string Text
        {
            set
            {
                text = value;
                Invalidate();
            }
            get
            {
                return text;
            }
        }

    }
}
