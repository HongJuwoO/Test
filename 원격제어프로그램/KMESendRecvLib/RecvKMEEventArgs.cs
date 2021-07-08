using System;
using System.Drawing;
using 이미지_캡쳐_전송클라이언트;

namespace 원격제어프로그램
{
    public delegate void RecvKMEEventHandler(object sender, RecvKMEEventArgs e);
    public class RecvKMEEventArgs:EventArgs
    {
        public Meta Meta
        {
            get;
            private set;
        }

        public int Key
        {
            get
            {
                return Meta.Key;
            }
        }

        public Point Now
        {
            get
            {
                return Meta.Now;
            }
        }

        public MsgType MT
        {
            get
            {
                return Meta.MT;
            }
        }
        public RecvKMEEventArgs(Meta meta)
        {
            Meta = meta;
        }
    }
}
