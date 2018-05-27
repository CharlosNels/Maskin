using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinItem : Control
    {
        public MaskinItem()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }
        Graphics g;

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            g = pe.Graphics;
            SizeF siz= g.MeasureString(Text, Font);
            StringFormat sf = new StringFormat();
            g.DrawString(Text, Font, Brushes.Black, new Point((int)(0.5*(Width-siz.Width)),(int)(0.5 * (Height - siz.Height))),sf);
        }
        /// <summary>
        /// 移除背景图片，使Item为单色模式
        /// </summary>
        public void RemoveBackGroundImage()
        {
            Maskin_backgroundImage.Dispose();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
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
        
        private bool isMouseDown;
        private bool isMouseIn;
        private void DrawBorder()
        {
            switch (_borderStyle)
            {
                case NativeData.MaskinBorderStyle.MaskinStyle:
                    g.DrawPath(new Pen(Brushes.Black), Tools.CreateRadianRectangle(new Rectangle(0, 0, Width-1, Height-1), radius));
                    break;
                case NativeData.MaskinBorderStyle.NormalStyle:
                    g.DrawRectangle(new Pen(Brushes.Black), new Rectangle(0, 0, Width-1, Height-1));
                    break;
            }
        }
        private int radius=5;

        [Description("圆角模式的半径")]
        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                if (value<Width/2)
                {
                    radius = value;
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            if (BorderStyle == NativeData.MaskinBorderStyle.MaskinStyle)
            {
                if (_maskin_backgroundImage == null)
                {
                    if (isMouseDown)
                    {
                        g.FillPath(new SolidBrush(_maskin_Down_Color), Tools.CreateRadianRectangle(pevent.ClipRectangle, radius));
                        DrawBorder();
                    }
                    else if (isMouseIn)
                    {
                        g.FillPath(new SolidBrush(_maskin_On_Color), Tools.CreateRadianRectangle(pevent.ClipRectangle, radius));
                        DrawBorder();
                    }
                    else
                    {
                        g.FillPath(new SolidBrush(_maskin_Normal_Color), Tools.CreateRadianRectangle(pevent.ClipRectangle, radius));
                    }
                }
                else
                {
                    if (isMouseDown)
                    {
                        g.DrawImage(_maskin_backgroundImage, 1, 1, Width - 2, Height - 2);
                        DrawBorder();
                    }
                    else if (isMouseIn)
                    {
                        g.DrawImage(_maskin_backgroundImage, 0, 0, Width, Height);
                        DrawBorder();
                    }
                    else
                    {
                        g.DrawImage(_maskin_backgroundImage, 0, 0, Width, Height);
                    }
                }
            }
            else
            {
                if (_maskin_backgroundImage == null)
                {
                    if (isMouseDown)
                    {
                        g.FillRectangle(new SolidBrush(_maskin_Down_Color), pevent.ClipRectangle);
                        DrawBorder();
                    }
                    else if (isMouseIn)
                    {
                        g.FillRectangle(new SolidBrush(_maskin_On_Color), pevent.ClipRectangle);
                        DrawBorder();
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(_maskin_Normal_Color), pevent.ClipRectangle);
                    }
                }
                else
                {
                    if (isMouseDown)
                    {
                        g.DrawImage(_maskin_backgroundImage, 1, 1, Width - 2, Height - 2);
                        DrawBorder();
                    }
                    else if (isMouseIn)
                    {
                        g.DrawImage(_maskin_backgroundImage, 0, 0, Width, Height);
                        DrawBorder();
                    }
                    else
                    {
                        g.DrawImage(_maskin_backgroundImage, 0, 0, Width, Height);
                    }
                }
            }
        }

        private int _id;
        
        private NativeData.MaskinBorderStyle _borderStyle=NativeData.MaskinBorderStyle.NormalStyle;

        private Image _maskin_backgroundImage;

        private Color _maskin_On_Color=Color.FromArgb(0,174,219);

        [Description("鼠标移到Item上的颜色")]
        public Color Maskin_On_Color
        {
            get
            {
                return _maskin_On_Color;
            }
            set
            {
                _maskin_On_Color = value;
                Refresh();
            }
        }

        private Color _maskin_Down_Color= Color.FromArgb(0, 198, 247);
        private Color _maskin_Normal_Color= Color.FromArgb(0, 121, 151);
        [Description("窗体边框,MaskinStyle为圆角模式,NormalStyle为矩形模式")]
        public NativeData.MaskinBorderStyle BorderStyle
        {
            get
            {
                return _borderStyle;
            }

            set
            {
                _borderStyle = value;
                Refresh();
            }
        }
        [Description("鼠标按下时的颜色")]
        public Color Maskin_Down_Color
        {
            get
            {
                return _maskin_Down_Color;
            }

            set
            {
                _maskin_Down_Color = value;
                Refresh();
            }
        }
        [Description("常态颜色")]
        public Color Maskin_Normal_Color
        {
            get
            {
                return _maskin_Normal_Color;
            }

            set
            {
                _maskin_Normal_Color = value;
                Refresh();
            }
        }
        [Description("Item的背景图片")]
        public Image Maskin_backgroundImage
        {
            get
            {
                return _maskin_backgroundImage;
            }

            set
            {
                _maskin_backgroundImage = value;
                Refresh();
            }
        }
        [Description("Item的ID"),Browsable(false)]
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
        }
    }
}
