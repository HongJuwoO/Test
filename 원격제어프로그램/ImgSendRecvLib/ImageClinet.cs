using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace 이미지클라이언트
{
    public delegate bool SendImageDele(Image img);
    public class ImageClinet
    {
        Socket sock;
        public void Connect(string ip, int port)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            sock.Connect(ep);
            // 비동기로 
        }
        
        public bool SendImage(Image img)
        {
            if(sock == null)
            {
                return false;
            }

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);

            byte[] data = ms.GetBuffer();

            try
            {
                int trans = 0;
                byte[] ibuf = BitConverter.GetBytes(data.Length);

                sock.Send(ibuf); // 전송할 이미지 길이 전송

                while(trans < data.Length)
                {
                    //sock.Send(data, SocketFlags.None);
                    trans += sock.Send(data, trans, data.Length - trans, SocketFlags.None);

                }
                sock.Close();
                sock = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void SendImageAsync(Image img, AsyncCallback callback)
        {
            SendImageDele dele = SendImage;
            dele.BeginInvoke(img, callback, this);
        }
        public void Close()
        {
            if(sock != null)
            {
                sock.Close();
                sock = null;
            }
        }
    }
}
