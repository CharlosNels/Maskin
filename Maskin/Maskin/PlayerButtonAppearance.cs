using System;
using System.Drawing;
using System.Windows.Forms;

namespace Maskin
{
    public class PlayerButtonAppearance :Control
    {
        protected PlayerButtonAppearance()
        {

        }
        protected Color lineColor = Color.LightGray;
        protected Color onLineColor = Color.Silver;
        protected Color downColor = Color.Black;
        public virtual Color LineColor
        {
            set
            {
                lineColor = value;
                Invalidate();
            }
            get
            {
                return lineColor;
            }
        }
        public virtual Color OnLineColor
        {
            set
            {
                onLineColor = value;
                Invalidate();
            }
            get
            {
                return onLineColor;
            }
        }
        public virtual Color DownColor
        {
            set
            {
                downColor = value;
                Invalidate();
            }
            get
            {
                return downColor;
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (TopLevelControl is MaskinForm)
            {
                ((MaskinForm)TopLevelControl).SetMouseEnter(e);
            }
        }
    }
}
