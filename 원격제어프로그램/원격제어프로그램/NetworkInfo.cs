using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 원격제어프로그램
{
    public class NetworkInfo
    {
        public static short ImgPort
        {
            get
            {
                return 10001;
            }
        }

        public static short SetupPort
        {
            get
            {
                return 20001;
            }
        }

        public static short EventPort
        {
            get
            {
                return 30001;
            }
                
        }

        public static string DefaultIP
        {
            get
            {
                string host_name = Dns.GetHostName();
                IPHostEntry host_entry = Dns.GetHostEntry(host_name);

                foreach(IPAddress ipaddr in host_entry.AddressList)
                {
                    if(ipaddr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipaddr.ToString();
                    }
                }

                return "127.0.0.1";
            }
        }
    }
}
