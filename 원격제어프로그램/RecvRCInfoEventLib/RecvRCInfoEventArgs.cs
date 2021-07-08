using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecvRCInfoEventLib
{
    public delegate void RecvRCInfoEventHandler(object sdnder, RecvRCInfoEventArgs e);
    public class RecvRCInfoEventArgs : EventArgs
    {
        /// <summary>
        /// IP 단말 정보 가져오기
        /// </summary>
        public IPEndPoint IPEndPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// IP 주소 문자열 가져오기 
        /// </summary>
        public string IPAddressStr
        {
            get
            {
                return IPEndPoint.Address.ToString();
            }
        }

        /// <summary>
        /// Port 가져오기 
        /// </summary>
        public int Port
        {
            get
            {
                return IPEndPoint.Port;
            }
        }
        /// <summary>
        /// 생성자 
        /// </summary>
        /// <param name="RemoteEndPoint"></param>
        public RecvRCInfoEventArgs(EndPoint RemoteEndPoint)
        {
            IPEndPoint = RemoteEndPoint as IPEndPoint;
        }
    }
}
