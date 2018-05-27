using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinForm : Form
    {
        public MaskinForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            cb.MouseEnter += Cb_MouseEnter;
        }

        private void Cb_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        public void SetMouseEnter(EventArgs e)
        {
            OnMouseEnter(e);
        }

        private bool closeOrHide = true;

        public bool CloseOrHide
        {
            set
            {
                closeOrHide = value;
            }
            get
            {
                return closeOrHide;
            }
        }

        private bool canMax=true;

        public bool CanMax
        {
            set
            {
                canMax = value;
            }
            get
            {
                return canMax;
            }
        }

        CloseButton cb = new CloseButton();
        MiniButton minibtn = new MiniButton();
        MaxButton maxbtn = new MaxButton();
        MaskinPlayerMiniButton mpm = new MaskinPlayerMiniButton();
        private bool maskinMiniBox=true;

        public int LinesWidth
        {
            set
            {
                cb.LineWidth = value;
                maxbtn.LineWidth = value;
                minibtn.LineWidth = value;
            }
            get
            {
                return cb.LineWidth;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Controls.Add(cb);
            this.Load += MaskinForm_Load;
            cb.Location = new Point(Width - 23-borderLineWidth, borderLineWidth);
            cb.Click += Cb_Click;
            gridenCenterPoint = new Point(Width / 2, Height / 2);
        }

        private void MaskinForm_Load(object sender, EventArgs e)
        {
            if (MaximizeBox)
            {
                Controls.Add(maxbtn);
                maxbtn.Location = new Point(Width - 46 - borderLineWidth, borderLineWidth);
                maxbtn.Click += Maxbtn_Click;
                if (MinimizeBox)
                {
                    Controls.Add(minibtn);
                    minibtn.Location = new Point(Width - 69 - borderLineWidth, borderLineWidth);
                    minibtn.Click += Minibtn_Click;
                    if (MaskinMiniBox)
                    {
                        Controls.Add(mpm);
                        mpm.Location = new Point(Width - 92 - borderLineWidth, borderLineWidth);
                    }
                }
                else
                {
                    if (maskinMiniBox)
                    {
                        Controls.Add(mpm);
                        mpm.Location = new Point(Width - 69 - borderLineWidth, borderLineWidth);
                    }
                }
            }
            else
            {
                if (MinimizeBox)
                {
                    Controls.Add(minibtn);
                    minibtn.Location = new Point(Width - 46 - borderLineWidth, borderLineWidth);
                    minibtn.Click += Minibtn_Click;
                    if (maskinMiniBox)
                    {
                        Controls.Add(mpm);
                        mpm.Location = new Point(Width - 69 - borderLineWidth, borderLineWidth);
                    }
                }
                else
                {
                    if (maskinMiniBox)
                    {
                        Controls.Add(mpm);
                        mpm.Location = new Point(Width - 46 - borderLineWidth, borderLineWidth);
                    }
                }
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (canMax)
            {
                Maxbtn_Click(this, e);
            }
        }

        private void Maxbtn_Click(object sender, EventArgs e)
        {
            if (WindowState==FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                maxbtn.IsMaximized = false;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                maxbtn.IsMaximized = true;
            }
            Refresh();
        }
        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        private void Minibtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Cb_Click(object sender, EventArgs e)
        {
            if (closeOrHide)
            {
                Close();
            }else
            {
                Hide();
            }
        }

        private bool linearBackColor=false;

        private int radius;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (linearBackColor)
            {
                if (gridenStyle==NativeData.GridenStyles.Normal)
                {
                    LinearGradientBrush lgb = new LinearGradientBrush(new Point(0, 0), new Point(Width, Height), startColor, endColor);
                    g.FillRectangle(lgb, e.ClipRectangle);
                    lgb.Dispose();
                }
                else
                {
                    LinearGradientBrush lgb1 = new LinearGradientBrush(new Point(0, 0), gridenCenterPoint, startColor, endColor);
                    g.FillRectangle(lgb1, lgb1.Rectangle);
                    lgb1.Dispose();
                    LinearGradientBrush lgb2 = new LinearGradientBrush(new Point(Width,0), gridenCenterPoint, startColor, endColor);
                    g.FillRectangle(lgb2, lgb2.Rectangle);
                    lgb2.Dispose();
                    LinearGradientBrush lgb3 = new LinearGradientBrush(new Point(0, Height), gridenCenterPoint, startColor, endColor);
                    g.FillRectangle(lgb3, lgb3.Rectangle);
                    lgb3.Dispose();
                    LinearGradientBrush lgb4 = new LinearGradientBrush(new Point(Width, Height), gridenCenterPoint, startColor, endColor);
                    g.FillRectangle(lgb4, lgb4.Rectangle);
                    lgb4.Dispose();
                }
            }
            if (ShowIcon)
            {
                g.DrawIcon(Icon, new Rectangle(12, 24, 18, 18));
                g.DrawString(Text, new Font("微软雅黑", 15, FontStyle.Regular),new SolidBrush(ForeColor), new Point(30, 17));
            }
            else
            {
                g.DrawString(Text,new  Font("微软雅黑",15,FontStyle.Regular), new SolidBrush(ForeColor), new Point(15, 15));
            }
            if (drawBorder)
            {
                DrawBorder(g, new Rectangle(0,0,Width,Height));
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            DrawBorder(Graphics.FromHwnd(Handle), new Rectangle(0, 0, Width, Height));
        }

        private void DrawBorder(Graphics g,Rectangle rect)
        {
            g.DrawRectangle(new Pen(BorderColor,borderLineWidth), new Rectangle(borderLineWidth-1,borderLineWidth-1,rect.Width-borderLineWidth,rect.Height-borderLineWidth));
        }

        private int borderLineWidth=1;

        private bool isMouseDown;
        private int X, Y;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button==MouseButtons.Left)
            {
                isMouseDown = true;
                X = -e.X;
                Y = -e.Y;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMouseDown = false;
            Refresh();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMouseDown)
            {
                if (moveable)
                {
                    Location = PointToScreen(new Point(e.X + X, e.Y + Y));
                }
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            cb.Location = new Point(Width - 23-borderLineWidth, borderLineWidth);
            if (MaximizeBox)
            {
                if (!Controls.Contains(maxbtn))
                {
                    Controls.Add(maxbtn);
                }
                maxbtn.Location = new Point(Width - 46-borderLineWidth, borderLineWidth);
                if (MinimizeBox)
                {
                    if (!Controls.Contains(minibtn))
                    {
                        Controls.Add(minibtn);
                    }
                    minibtn.Location = new Point(Width - 69-borderLineWidth, borderLineWidth);
                    if (maskinMiniBox)
                    {
                        if (!Controls.Contains(mpm))
                        {
                            Controls.Add(mpm);
                        }
                        mpm.Location = new Point(Width - 92 - borderLineWidth, borderLineWidth);
                    }
                    else
                    {
                        if (Controls.Contains(mpm))
                        {
                            Controls.Remove(mpm);
                        }
                    }
                }
                else
                {
                    if (maskinMiniBox)
                    {
                        if (!Controls.Contains(mpm))
                        {
                            Controls.Add(mpm);
                        }
                        mpm.Location = new Point(Width - 69 - borderLineWidth, borderLineWidth);
                    }
                    else
                    {
                        if (Controls.Contains(mpm))
                        {
                            Controls.Remove(mpm);
                        }
                    }
                }
            }
            else
            {
                if (Controls.Contains(maxbtn))
                {
                    Controls.Remove(maxbtn);
                }
                if (MinimizeBox)
                {
                    if (!Controls.Contains(minibtn))
                    {
                        Controls.Add(minibtn);
                    }
                    minibtn.Location = new Point(Width - 46-borderLineWidth, borderLineWidth);
                    if (maskinMiniBox)
                    {
                        if (!Controls.Contains(mpm))
                        {
                            Controls.Add(mpm);
                        }
                        mpm.Location = new Point(Width - 69 - borderLineWidth, borderLineWidth);
                    }
                    else
                    {
                        if (Controls.Contains(mpm))
                        {
                            Controls.Remove(mpm);
                        }
                    }
                }
                else
                {
                    if (Controls.Contains(minibtn))
                    {
                        Controls.Remove(minibtn);
                    }
                    if (maskinMiniBox)
                    {
                        if (!Controls.Contains(mpm))
                        {
                            Controls.Add(mpm);
                        }
                        mpm.Location = new Point(Width - 46 - borderLineWidth, borderLineWidth);
                    }
                    else
                    {
                        if (Controls.Contains(mpm))
                        {
                            Controls.Remove(mpm);
                        }
                    }
                }
            }
        }
        
        public void SetMaskinMiniButtonClick(EventHandler mskclk)
        {
            mpm.Click += mskclk;
        }
        private Color _borderColor=Color.Black;

        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }

            set
            {
                _borderColor = value;
            }
        }

        public int BorderLineWidth
        {
            get
            {
                return borderLineWidth;
            }

            set
            {
                borderLineWidth = value;
            }
        }


        [Description("是否有迷你按钮")]
        public bool MaskinMiniBox
        {
            get
            {
                return maskinMiniBox;
            }

            set
            {
                maskinMiniBox = value;
                Refresh();
            }
        }

        public Color StartColor
        {
            get
            {
                return startColor;
            }

            set
            {
                startColor = value;
                if (LinearBackColor)
                {
                    Refresh();
                }
            }
        }

        public Color EndColor
        {
            get
            {
                return endColor;
            }

            set
            {
                endColor = value;
                if (LinearBackColor)
                {
                    Refresh();
                }
            }
        }

        public bool LinearBackColor
        {
            get
            {
                return linearBackColor;
            }

            set
            {
                linearBackColor = value;
                Refresh();
            }
        }

        private Color startColor;
        private Color endColor;

        private NativeData.GridenStyles gridenStyle;
        public NativeData.GridenStyles GridenStyle
        {
            set
            {
                gridenStyle = value;
                Refresh();
            }
            get
            {
                return gridenStyle;
            }
        }

        public Point GridenCenterPoint
        {
            get
            {
                return gridenCenterPoint;
            }

            set
            {
                gridenCenterPoint = value;
                if (gridenStyle==NativeData.GridenStyles.Center)
                {
                    Refresh();
                }
            }
        }

        public bool ShouldDrawBorder
        {
            get
            {
                return drawBorder;
            }

            set
            {
                drawBorder = value;
                Refresh();
            }
        }

        private Point gridenCenterPoint;

        private bool drawBorder=true;
        private bool moveable = true;
        public bool Moveable
        {
            set
            {
                moveable = value;
            }
            get
            {
                return moveable;
            }
        }

        public int Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
                Invalidate();
            }
        }
    }
}
