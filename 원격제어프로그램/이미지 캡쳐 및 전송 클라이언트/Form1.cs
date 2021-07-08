using System;
using System.Drawing;
using System.Windows.Forms;
using 이미지_캡쳐_전송클라이언트;
using 이미지클라이언트;

namespace 이미지_캡쳐_및_전송_클라이언트
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string _ip;
        ImageClinet _ic;

        private void button1_Click(object sender, EventArgs e)
        {
            //_ip = textBox1.Text;
            _ip = "127.0.0.1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(null == _ic)
            {
                return;
            }

            int left = trackBar1.Value;
            int right = trackBar3.Value;

            if(left > right)
            {
                int temp = left;
                left = right;
                right = temp;
            }

            int top = trackBar2.Value;
            int bottom = trackBar4.Value;

            if(top > bottom)
            {
                int temp = top;
                top = bottom;
                bottom = temp;
            }

            int width = right - left;
            int height = bottom - top;

            if((0 == width) || (0 == height))
            {
                return;
            }

            Bitmap bitmap = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(bitmap);
            Size size = new Size(width, height);

            gr.CopyFromScreen(new Point(left, top), new Point(0, 0), size);
            _ic.Connect(_ip, 5005);
            _ic.SendImage(bitmap);
            _ic.Close();

            pictureBox1.Image = bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            _ic = new ImageClinet();
            trackBar1.Maximum = trackBar3.Maximum = Screen.PrimaryScreen.Bounds.Width;
            trackBar2.Maximum = trackBar4.Maximum = Screen.PrimaryScreen.Bounds.Height;
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(null == _ip)
            {
                return;
            }

            SendEventClient sec = new SendEventClient(_ip, 5010);
            sec.SendMouseUp(e.Button);
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (null == _ip)
            {
                return;
            }

            SendEventClient sec = new SendEventClient(_ip, 5010);
            sec.SendMouseMove(e.X, e.Y);
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (null == _ip)
            {
                return;
            }

            SendEventClient sec = new SendEventClient(_ip, 5010);
            sec.SendMouseDown(e.Button);
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (null == _ip)
            {
                return;
            }

            SendEventClient sec = new SendEventClient(_ip, 5010);
            sec.SendKeyUp(e.KeyValue);
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (null == _ip)
            {
                return;
            }

            SendEventClient sec = new SendEventClient(_ip, 5010);
            sec.SendKeyDown(e.KeyValue);
        }
    }
}
