using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinLyricsPanel :PlayerButtonAppearance
    {
        public MaskinLyricsPanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("微软雅黑", 19);
            BackColor = Color.Transparent;
        }

        private  string[] lyrics=new string[] { "Maskin Player"};

        public string[] Lyrics
        {
            set
            {
                lyrics = value;
            }

            get
            {
                return lyrics;
            }

        }

        private int selectedlyric=0;

        public int SelectedLyric
        {
            set
            {
                if (0<=value&&value<=lyrics.Length-1)
                {
                    selectedlyric = value;
                }
                else if(value<0)
                {
                    selectedlyric = 0;
                }else
                {
                    selectedlyric = lyrics.Length - 1;
                }
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SizeF siz = g.MeasureString(lyrics[selectedlyric],Font);
            PointF p = new PointF((Width - siz.Width) / 2,(Height - siz.Height));
            g.DrawString(lyrics[selectedlyric],Font,new SolidBrush(downColor),p);
        }
    }
}
