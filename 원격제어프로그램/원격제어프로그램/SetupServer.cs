using RecvRCInfoEventLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 원격제어프로그램
{
    public static class SetupServer
    {
        static Socket _lis_sock;
        static public event RecvRCInfoEventHandler _RecvedRCInfoEventHandler = null;
        static string _IP;
        static int _PORT;

        public static void Start(string ip, int port)
        {
            SetupServer._IP = ip;
            SetupServer._PORT = port;
            SocketBooting();
        }

        private static void SocketBooting()
        {
            IPAddress ipaddr = IPAddress.Parse(_IP);
            IPEndPoint ep = new IPEndPoint(ipaddr, _PORT);
            _lis_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _lis_sock.Bind(ep);
            _lis_sock.Listen(1);

            _lis_sock.BeginAccept(DoAccept, null);
            
        }

        static void DoAccept(IAsyncResult result)
        {
            if(null == _lis_sock)
            {
                return;
            }

            try
            {
                Socket sock = _lis_sock.EndAccept(result);
                Doit(sock);

                _lis_sock.BeginAccept(DoAccept, null);
            }
            catch
            {
                Close();
            }
        }

        static void Doit(Socket dosock)
        {
            if(null != _RecvedRCInfoEventHandler)
            {
                RecvRCInfoEventArgs e = new RecvRCInfoEventArgs(dosock.RemoteEndPoint);
                _RecvedRCInfoEventHandler(null, e);
            }
            dosock.Close();
        }

        public static void Close()
        {
            if(null != _lis_sock)
            {
                _lis_sock.Close();
                _lis_sock = null;
            }

        }
    }
}
