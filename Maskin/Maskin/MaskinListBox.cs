using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Maskin
{
    public partial class MaskinListBox : Panel
    {
        public MaskinListBox()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);
            BackColor = Color.Transparent;
            Size = new Size(220,400);
        }
        private Color maskinListBoxItemLineColor=Color.FromArgb(170,Color.LightBlue);                   //the normal color
        private Color maskinListBoxItemOnLineColor=Color.FromArgb(170,Color.Turquoise);                 //the color when mouse is on Item
        private Color maskinListBoxItemDownColor=Color.FromArgb(170,Color.DarkTurquoise);                   //the color when Item is selected
        private Dictionary<int, MaskinListBoxItem> Items = new Dictionary<int, MaskinListBoxItem>();                    //the collection of Items

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (TopLevelControl is MaskinForm)
            {
                ((MaskinForm)TopLevelControl).SetMouseEnter(e);             //触发父窗口的鼠标进入事件
            }
            Focus();
        }

        internal void setMouseEnter(EventArgs e)
        {
            OnMouseEnter(e);            //把私有的方法公有化
        }

        public Color MaskinListBoxItemLineColor
        {
            get
            {
                return maskinListBoxItemLineColor;
            }

            set
            {
                maskinListBoxItemLineColor = value;
                foreach (var item in Items)
                {
                    item.Value.LineColor = value;                                                                                   //遍历每个列表项目，改变其常态颜色
                }
                Refresh();
            }
        }

        public Color MaskinListBoxItemOnLineColor
        {
            get
            {
                return maskinListBoxItemOnLineColor;
            }

            set
            {
                maskinListBoxItemOnLineColor = value;
                foreach (var item in Items)
                {
                    item.Value.OnLineColor = value;                     //遍历每个列表项目，改变其鼠标停顿颜色
                }
                Refresh();
            }
        }

        public Color MaskinListBoxItemDownColor
        {
            get
            {
                return maskinListBoxItemDownColor;
            }

            set
            {
                maskinListBoxItemDownColor = value;
                foreach (var item in Items)
                {
                    item.Value.DownColor = value;               //遍历每个列表项目，改变其选中时颜色
                }
                Refresh();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            foreach (var item in Items)
            {
                item.Value.Width = Width-19;                        //当列表大小改变时，改变子列表的宽度，留下滚动条的位置
            }
        }

        public void AddItem(string  mainText,string content,object info)
        {
            MaskinListBoxItem mskitm = new MaskinListBoxItem(maskinListBoxItemLineColor, maskinListBoxItemOnLineColor, MaskinListBoxItemDownColor, mainText, content,info, Width-19);
            Items.Add(H / 30+1, mskitm);
            Controls.Add(mskitm);
            mskitm.Location = new Point(1, H);
            H += 30;                //新项目加入列表时的宽度
        }
        public delegate void ItemClick(itemArgs e);
        public event ItemClick ItemClicked;
        public event ItemClick ItemDoubleClicked;
        public event ItemClick ItemRightClicked;

        internal void ItemRitClked(MaskinListBoxItem msi)
        {
            ItemRightClicked?.Invoke(new itemArgs(msi.MainText, msi.Content, msi.Info, GetIDbyItem(msi)));
        }

        internal void ItemClked(MaskinListBoxItem msi)
        {
            if (!msi.Equals(selectedItem))
            {
                msi.Selected = true;
                if (selectedItem!=null)
                {
                    selectedItem.Selected = false;
                }
                selectedItem = msi;
            }
            ItemClicked?.Invoke(new itemArgs(msi.MainText, msi.Content, msi.Info, GetIDbyItem(msi)));
        }
        private MaskinListBoxItem selectedItem = null;
        internal void ItemDoubleClked(MaskinListBoxItem msi)
        {
            ItemDoubleClicked?.Invoke(new itemArgs(msi.MainText, msi.Content, msi.Info, GetIDbyItem(msi)));
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public void RemoveAll()
        {
            Items.Clear();
            Controls.Clear();
            H = 1;
        }
        public object GetInfoByID(int id)
        {
            MaskinListBoxItem m;
            Items.TryGetValue(id, out m);
            return m.Info;
        }

        public bool ContainItem(string info)
        {
            foreach (var item in Controls)
            {
                if (((MaskinListBoxItem)item).Info.Equals(info))
                {
                    return true;
                }
            }
            return false;
        }

        public  string[] getMsg(int id)
        {
            MaskinListBoxItem ms;
            Items.TryGetValue(id, out ms);
            try
            {
                string[] strs = new string[] { ms.MainText, ms.Content, ms.Info as string};
                return strs;
            }
            catch
            {
                return new string[] { "0", "0", "0" };
            }
        }

        private int GetIDbyItem(MaskinListBoxItem m)
        {
            foreach (var item in Items)
            {
                if (item.Value.Equals(m))
                {
                    return item.Key;
                }
            }
            return -1;
        }
        private int H=1;
    }
}
