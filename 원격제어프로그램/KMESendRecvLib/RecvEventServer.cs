using System;
using System.Net;
using System.Net.Sockets;

namespace 원격제어프로그램
{
    public class RecvEventServer
    {
        Socket _lis_sock;
        public event RecvKMEEventHandler _RecvedKMEvent = null;

        public RecvEventServer(string ip, int port)
        {
            _lis_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);

            _lis_sock.Bind(ep);
            _lis_sock.Listen(5);
            _lis_sock.BeginAccept(DoAccept, null);
        }

        private void DoAccept(IAsyncResult result)
        {
            if(_lis_sock != null)
            {
                Socket dosock = _lis_sock.EndAccept(result);
                _lis_sock.BeginAccept(DoAccept, null);
                Receive(dosock);
            }
        }

        private void Receive(Socket dosock)
        {
            byte[] buffer = new byte[9];
            int n = dosock.Receive(buffer);

            if(null != _RecvedKMEvent)
            {
                RecvKMEEventArgs e = new RecvKMEEventArgs(new Meta(buffer));
                _RecvedKMEvent(this, e);
            }

            dosock.Close();
        }

        public void Close()
        {
            if(_lis_sock != null)
            {
                _lis_sock.Close();
                _lis_sock = null;
            }
        }
    }
}
