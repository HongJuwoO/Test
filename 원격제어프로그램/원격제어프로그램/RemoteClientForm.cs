using System;
using System.Drawing;
using System.Windows.Forms;
using 이미지_캡쳐_전송클라이언트;
using 이미지이벤트;

namespace 원격제어프로그램
{
    public partial class RemoteClientForm : Form
    {

        bool _imagecheck;
        Size _clientsize;

        SendEventClient EventSC
        {
            get
            {
                return Controller.Singleton.SendEventClient;
            }
        }
        public RemoteClientForm()
        {
            InitializeComponent();
        }
        private void RemoteClientForm_Load(object sender, EventArgs e)
        {
            Controller.Singleton._RecvedImage += Singleton_RecvedImage;
        }

        private void Singleton_RecvedImage(object sender, RecvImageEventArgs e)
        {
            if(false == _imagecheck)
            {
                Controller.Singleton.StartEventClient();
                _imagecheck = true;
                _clientsize = e.Image.Size;
            }
            pbox_remote.Image = e.Image;
        }

        private void RemoteClientForm_KeyUp(object sender, KeyEventArgs e)
        {
            if(true == _imagecheck)
            {
                EventSC.SendKeyUp(e.KeyValue);
            }
        }

        private void RemoteClientForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (true == _imagecheck)
            {
                EventSC.SendKeyDown(e.KeyValue);
            }
        }

        private void pbox_remote_MouseDown(object sender, MouseEventArgs e)
        {
            if (true == _imagecheck)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseDown(e.Button);
            }
        }

        private void pbox_remote_MouseMove(object sender, MouseEventArgs e)
        {
            if (true == _imagecheck)
            {
                Point pt = ConvertPoint(e.X, e.Y);
                Text = e.Location.ToString();
                EventSC.SendMouseMove(pt.X, pt.Y);
            }
        }

        private Point ConvertPoint(int x, int y)
        {
            int nX = _clientsize.Width * x / pbox_remote.Width;
            int nY = _clientsize.Height * y / pbox_remote.Height;
            return new Point(nX, nY);
        }

        private void pbox_remote_MouseUp(object sender, MouseEventArgs e)
        {
            if (true == _imagecheck)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseUp(e.Button);
            }
        }
    }
}
