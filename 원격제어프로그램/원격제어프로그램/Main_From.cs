using RecvRCInfoEventLib;
using System;
using System.Drawing;
using System.Windows.Forms;
using 이미지클라이언트;

namespace 원격제어프로그램
{
    public partial class Main_From : Form
    {
        string _tagetIP;
        int _tagetPort;
        RemoteClientForm _rcf = null;
        VirtualCursorForm _vcf = null;

        delegate void Remote_Delegate(object sender, RecvRCInfoEventArgs e);

        public Main_From()
        {
            InitializeComponent();
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            //string ip = tbox_IP.Text;
            //SetupClinet.ConnectedEventHandler += SetupClinet_ConnectedEventHandler;
            //SetupClinet.ConnectFailedEventHandler += SetupClinet_ConnectFailedEventHandler;
            //SetupClinet.Setup(ip, 5001);

            if(tbox_IP.Text == NetworkInfo.DefaultIP)
            {
                MessageBox.Show("같은 호스트를 원격제어 할 수 없습니다");
                tbox_IP.Text = string.Empty;
                return;
            }

            string host_ip = tbox_IP.Text;
            Rectangle rect = Remote.Singleton.Rect;
            Controller.Singleton.Start(host_ip);

            _rcf.ClientSize = new Size(rect.Width - 40, rect.Height - 80);
            _rcf.Show();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Hide();
            Remote.Singleton.RecvEventStart();
            timer_send_img.Start();
            _vcf.Show();
        }

        private void Main_From_Load(object sender, EventArgs e)
        {
            //SetupServer.RecvedRCInfoEventHandler += SetupServer_RecvedRCinfoEventHandler;
            //SetupServer.Start("127.0.0.1", 5001);

            Text += "." + NetworkInfo.DefaultIP;
            _vcf = new VirtualCursorForm();
            _rcf = new RemoteClientForm();

            Remote.Singleton._RecvedRCInfo += Singleton_RecvedRCInfo;
        }
        
        private void Singleton_RecvedRCInfo(object sender, RecvRCInfoEventArgs e)
        {
            if(this.InvokeRequired)
            {
                object[] objs = new object[2] { sender, e };
                this.Invoke(new Remote_Delegate(Singleton_RecvedRCInfo), objs);
            }
            else
            {
                tbox_controller_IP.Text = e.IPAddressStr;
                _tagetIP = e.IPAddressStr;
                _tagetPort = e.Port;
                btn_ok.Enabled = true;
            }
        }

        private void timer_send_img_Tick(object sender, EventArgs e)
        {
            Rectangle rect = Remote.Singleton.Rect;
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            Size size2 = new Size(rect.Width, rect.Height);
            graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), size2);
            graphics.Dispose();

            try
            {
                ImageClinet ic = new ImageClinet();
                ic.Connect(_tagetIP, NetworkInfo.ImgPort);
                ic.SendImageAsync(bitmap, null);
            }
            catch
            {
                timer_send_img.Stop();
                MessageBox.Show("서버에 문제");
                this.Close();
            }
        }

        private void noti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void SetupClinet_ConnectFailedEventHandler(object sender, EventArgs e)
        {
            //MessageBox.Show("연결실패");
        }

        private void SetupClinet_ConnectedEventHandler(object sender, EventArgs e)
        {
            //MessageBox.Show("연결성공");
        }

        private void SetupServer_RecvedRCinfoEventHandler(object sender, RecvRCInfoEventArgs e)
        {
            //tbox_controller_IP.Text = e.IPAddressStr;  이부분이 왜 안되는지 UI쓰레드가 왜 안되는지 확인
            //tbox_controller_IP.BeginInvoke(new Action(() =>
           //{
           //    tbox_controller_IP.Text = e.IPAddressStr;
           //}));
        }

        private void Main_From_FormClosed(object sender, FormClosedEventArgs e)
        {
            Remote.Singleton.Stop();
            Controller.Singleton.Stop();
            Application.Exit();
        }
    }
}
