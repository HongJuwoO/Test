using System;
using System.Windows.Forms;
using 원격제어프로그램;
using 이미지_캡쳐_전송클라이언트;
using 이미지서버;
using 이미지이벤트;

namespace 이미지수신서버
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ImageServer _ims;
        int _imageCount = 0;
        RecvEventServer _res;
        private void Form1_Load(object sender, EventArgs e)
        {
            _ims = new ImageServer("127.0.0.1", 5005);
            _ims._RecvedImage += _ims_RecvedImage;
            
            _res = new RecvEventServer("127.0.0.1", 5010);
            _res._RecvedKMEvent += Res_RecvedKMEvent;
        }

        private void Res_RecvedKMEvent(object sender, RecvKMEEventArgs e)
        {
            string s = e.MT.ToString();
            switch(e.MT)
            {
                case MsgType.MT_KEYBOARD_DOWN:
                case MsgType.MT_KEYBOARD_UP:
                    s += "" + e.Key.ToString();
                    break;
                case MsgType.MT_MOUSE_MOVE:
                    s += "" + e.Now.X.ToString() + "," + e.Now.Y.ToString();
                    break;
            }

            lbox_km.Invoke(new Action(() =>
            {
                lbox_km.Items.Add(s);
                lbox_km.SelectedIndex = lbox_km.Items.Count - 1;
            }));
        }

        private void _ims_RecvedImage(object sender, RecvImageEventArgs e)
        {
            _imageCount++;
            e.Image.Save(string.Format("{0}.bmp", _imageCount));

            listBox1.Invoke(new Action(()=>
            {
                listBox1.Items.Add(_imageCount);
            }));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(-1 == listBox1.SelectedIndex)
            {
                return;
            }

            int iCount = (int)listBox1.SelectedItem;
            pictureBox1.ImageLocation = string.Format("{0}.bmp", iCount);
        }

        static string DefaultIP
        {
            get
            {
                //string host_Name = Dns.GetHostName();
                //IPHostEntry host_entry = Dns.GetHostEntry(host_Name);

                //foreach(IPAddress ipaddr in host_entry.AddressList)
                //{
                //    if(ipaddr.AddressFamily == AddressFamily.InterNetwork)
                //    {
                //        return ipaddr.ToString();
                //    }
                //}
                return "127.0.0.1";
            }
        }
    }
}
