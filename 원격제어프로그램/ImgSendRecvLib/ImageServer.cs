using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using 이미지이벤트;

namespace 이미지서버
{
    public class ImageServer
    {
        Socket _lis_sock;

        public event RecvImageEventHandler _RecvedImage = null;

        public ImageServer(string ip, int port)
        {
            _lis_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);

            _lis_sock.Bind(ep);
            _lis_sock.Listen(5);
            _lis_sock.BeginAccept(DoAccept, null);
        }

        void DoAccept(IAsyncResult result)
        {
            if(_lis_sock == null)
            {
                return;
            }

            try
            {
                Socket dosock = _lis_sock.EndAccept(result);
                Recevice(dosock);

                _lis_sock.BeginAccept(DoAccept, null);
            }
            catch
            {
                Close();
            }
        }

        public void Close()
        {
            if(_lis_sock != null)
            {
                _lis_sock.Close();
                _lis_sock = null;
            }
        }

        private void Recevice(Socket dosock)
        {
            byte[] lbuf = new byte[4];
            dosock.Receive(lbuf);

            int len = BitConverter.ToInt32(lbuf, 0);
            byte[] buffer = new byte[len];

            //dosock.Receive(buffer, 0, len, SocketFlags.None);

            int trans = 0;
            while(trans < len)
            {
                trans += dosock.Receive(buffer, trans, len - trans, SocketFlags.None);
            }

            if(null != _RecvedImage)
            {
                IPEndPoint ep = dosock.RemoteEndPoint as IPEndPoint;
                RecvImageEventArgs e = new RecvImageEventArgs(ep, ConvertBitmap(buffer));

                _RecvedImage(this, e);
            }
        }
        public Bitmap ConvertBitmap(byte[] data)
        {
            MemoryStream ms = new MemoryStream();

            ms.Write(data, 0, (int)data.Length);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;

        }
    }
}
