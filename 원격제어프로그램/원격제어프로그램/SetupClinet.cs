using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace 원격제어프로그램
{
    public static class SetupClinet
    {
        public static event EventHandler _ConnectedEventHandler = null;
        public static event EventHandler _ConnectFailedEventHandler = null;
        static Socket sock;

        public static void Setup(string ip, int port)
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //sock.Connect(ep);
            sock.BeginConnect(ep, DoConnect, null);
        }

        static void DoConnect(IAsyncResult result)
        {
            AsyncResult ar = result as AsyncResult;
            try
            {
                sock.EndConnect(result);
                if(null != _ConnectedEventHandler)
                {
                    _ConnectedEventHandler(null, new EventArgs());
                }
            }
            catch
            {
                // 예외 처리
                if(null != _ConnectFailedEventHandler)
                {
                    _ConnectFailedEventHandler(null, new EventArgs());
                }
            }
            sock.Close();
        }
    }
}
