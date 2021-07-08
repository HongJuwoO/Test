using System.Drawing;
using 이미지_캡쳐_전송클라이언트;

namespace 원격제어프로그램
{
    //enum KeyFlag
    //{

    //}
    //enum MouseFlag
    //{

    //}
    //enum MsgType
    //{

    //}
    public class Meta
    {
        public MsgType MT
        {
            get;
            private set;
        }

        public int Key
        {
            get;
            private set;
        }

        public Point Now
        {
            get;
            private set;
        }

        public Meta(byte[] data)
        {
            MT = (MsgType)data[0];
            switch(MT)
            {
                case MsgType.MT_KEYBOARD_DOWN:
                case MsgType.MT_KEYBOARD_UP:
                    MakingKey(data);
                    break;
                case MsgType.MT_MOUSE_MOVE:
                    MakingPoint(data);
                    break;
            }
        }

        private void MakingPoint(byte[] data)
        {
            Point now = new Point(0, 0);
            now.X = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + data[1];
            now.Y = (data[8] << 24) + (data[7] << 16) + (data[6] << 8) + data[4];

            Now = now;
        }

        private void MakingKey(byte[] data)
        {
            Key = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + data[1];
        }
    }
}
