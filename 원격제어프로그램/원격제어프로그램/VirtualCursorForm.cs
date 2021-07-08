using System;
using System.Drawing;
using System.Windows.Forms;
using 이미지_캡쳐_전송클라이언트;

namespace 원격제어프로그램
{
    public partial class VirtualCursorForm : Form
    {
        public VirtualCursorForm()
        {
            InitializeComponent();
        }

        private void VirtualCursorForm_Load(object sender, EventArgs e)
        {
            Size = new Size(10, 10);
            Remote.Singleton._RecvedKMEvent += Singleton_RecvedKMEvent;
        }

        delegate void ChangeLocationDelegate(Point now, MsgType mt);
        void ChangeLocation(Point now,MsgType mt)
        {
            if(mt == MsgType.MT_MOUSE_MOVE)
            {
                Location = new Point(now.X + 3, now.Y + 3);
            }
        }

        private void Singleton_RecvedKMEvent(object sender, RecvKMEEventArgs e)
        {
            if(this.InvokeRequired) // 크로쓰레드 문제 해결용
            {
                object[] objs = new object[] { e.Now, e.MT };
                this.Invoke(new ChangeLocationDelegate(ChangeLocation),objs);
            }
            else
            {
                ChangeLocation(e.Now, e.MT);
            }
        }
    }
}
