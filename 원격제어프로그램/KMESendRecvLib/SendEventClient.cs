using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 이미지_캡쳐_전송클라이언트
{
    public enum MsgType
    {
        MT_KEYBOARD_DOWN = 1,
        MT_KEYBOARD_UP, 
        MT_MOUSE_LEFT_DOWN,
        MT_MOUSE_LEFT_UP,
        MT_MOUSE_RIGHT_DOWN,
        MT_MOUSE_RIGHT_UP,
        MT_MOUSE_MIDDLE_DOWN,
        MT_MOUSE_MIDDLE_UP,
        MT_MOUSE_MOVE
    }
    public class SendEventClient
    {
        IPEndPoint _ep;
        public SendEventClient(string ip, int port)
        {
            _ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void SendKeyDown(int key)
        {
            byte[] data = new byte[9];
            data[0] = (byte)MsgType.MT_KEYBOARD_DOWN;

            Array.Copy(BitConverter.GetBytes(key), 0, data, 1, 4);
            SendData(data);
        }

        public void SendKeyUp (int key)
        {
            byte[] data = new byte[9];
            data[0] = (byte)MsgType.MT_KEYBOARD_UP;

            Array.Copy(BitConverter.GetBytes(key), 0, data, 1, 4);
            SendData(data);
        }

        public void SendMouseDown(MouseButtons mouseButtons)
        {
            byte[] data = new byte[9];
            switch(mouseButtons)
            {
                case MouseButtons.Left: 
                    data[0] = (byte)MsgType.MT_MOUSE_LEFT_DOWN; 
                    break;
                case MouseButtons.Right: 
                    data[0] = (byte)MsgType.MT_MOUSE_RIGHT_DOWN; 
                    break;
                case MouseButtons.Middle: 
                    data[0] = (byte)MsgType.MT_MOUSE_MIDDLE_DOWN; 
                    break;
            }

            SendData(data);
        }

        public void SendMouseUp(MouseButtons mouseButtons)
        {
            byte[] data = new byte[9];
            switch (mouseButtons)
            {
                case MouseButtons.Left: 
                    data[0] = (byte)MsgType.MT_MOUSE_LEFT_UP; 
                    break;
                case MouseButtons.Right: 
                    data[0] = (byte)MsgType.MT_MOUSE_RIGHT_UP; 
                    break;
                case MouseButtons.Middle: 
                    data[0] = (byte)MsgType.MT_MOUSE_MIDDLE_UP; 
                    break;
            }

            SendData(data);
        }

        public void SendMouseMove(int x, int y)
        {
            byte[] data = new byte[9];
            data[0] = (byte)MsgType.MT_MOUSE_MOVE;
            Array.Copy(BitConverter.GetBytes(x), 0, data, 1, 4);
            Array.Copy(BitConverter.GetBytes(y), 0, data, 5, 4);

            SendData(data);
        }


        delegate void MY();
        MY Test;

        void add()
        {

        }

        private void SendData(byte[] data)
        {

            Test += add;


            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sock.Connect(_ep);
            sock.Send(data);
            sock.Close();
        }
    }
}
