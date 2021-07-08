using RecvRCInfoEventLib;
using System.Drawing;
using System.Windows.Automation;
using System.Windows.Forms;
using 이미지_캡쳐_전송클라이언트;

namespace 원격제어프로그램
{
    public class Remote
    {
        static Remote _singleton;

        public event RecvRCInfoEventHandler _RecvedRCInfo = null;
        public event RecvKMEEventHandler _RecvedKMEvent = null;
        
        RecvEventServer _res = null;
        public Rectangle Rect
        {
            get;
            private set;

        }

        public static Remote Singleton
        {
            get
            {
                return _singleton;
            }
        }
        static Remote()
        {
            _singleton = new Remote();
        }
        private Remote()
        {
            //int height = Screen.PrimaryScreen.Bounds.Height;
            //int width = Screen.PrimaryScreen.Bounds.Width;
            //int left = Screen.PrimaryScreen.Bounds.Left;
            //int top = Screen.PrimaryScreen.Bounds.Top;

            AutomationElement ae = AutomationElement.RootElement; // 바탕화면
            System.Windows.Rect rt = ae.Current.BoundingRectangle; // 바탕화면 사각원도우 사이즈 가져오기

            Rect = new Rectangle((int)rt.Left, (int)rt.Top, (int)rt.Width, (int)rt.Height);

            SetupServer._RecvedRCInfoEventHandler += SetupServer_RecvedRCInfoEventHandler;
            SetupServer.Start(MyIP, NetworkInfo.SetupPort);
        }

        public string MyIP
        {
            get
            {
                return NetworkInfo.DefaultIP;
            }
        }

        private void SetupServer_RecvedRCInfoEventHandler(object sdnder, RecvRCInfoEventArgs e)
        {
            _RecvedRCInfo(this, e);
        }

        public void RecvEventStart()
        {
            _res = new RecvEventServer(MyIP, NetworkInfo.EventPort);
            _res._RecvedKMEvent += Res_RecvedKMEvent;
        }

        private void Res_RecvedKMEvent(object sender, RecvKMEEventArgs e)
        {
            if(null != _RecvedKMEvent)
            {
                _RecvedKMEvent(this, e);
            }

            switch(e.MT)
            {
                case MsgType.MT_KEYBOARD_DOWN:
                    WrapNative.KeyDown(e.Key);
                    break;
                case MsgType.MT_KEYBOARD_UP:
                    WrapNative.KeyDown(e.Key);
                    break;
                case MsgType.MT_MOUSE_LEFT_DOWN:
                    WrapNative.LeftDown();
                    break;
                case MsgType.MT_MOUSE_LEFT_UP:
                    WrapNative.LeftUP();
                    break;
                case MsgType.MT_MOUSE_RIGHT_DOWN:
                    WrapNative.RightDown();
                    break;
                case MsgType.MT_MOUSE_RIGHT_UP:
                    WrapNative.RightUp();
                    break;
                case MsgType.MT_MOUSE_MOVE:
                    WrapNative.Move(e.Now);
                    break;
            }
        }
        public void Stop()
        {
            SetupServer.Close();
            if(null != _res)
            {
                _res.Close();
                _res = null;
            }
        }
    }
}
