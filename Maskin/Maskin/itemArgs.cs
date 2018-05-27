using System;
using System.Collections.Generic;
using System.Text;

namespace Maskin
{
    public class itemArgs
    {
        public itemArgs( string mainText,string content,object info,int id)
        {
            this.mainText = mainText;
            this.content = content;
            this.info = info;
            this.id = id;
        }

        private string mainText, content;
        private object info;
        private int id;
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
        public object Info
        {
            get
            {
                return info;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
        }
    }
}
