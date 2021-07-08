using 이미지_캡쳐_전송클라이언트;
using 이미지서버;
using 이미지이벤트;
namespace 원격제어프로그램
{
    public class Controller
    {
        static Controller _singleton;

        public static Controller Singleton
        {
            get
            {
                return _singleton;
            }
        }

        static Controller()
        {
            _singleton = new Controller();
        }
        private Controller()
        {

        }

        ImageServer _img_server = null;
        SendEventClient _sce = null;

        public event RecvImageEventHandler _RecvedImage = null;
        string _host_ip;

        public SendEventClient SendEventClient
        {
            get
            {
                return _sce;
            }
        }


        public string MyIP
        {
            get
            {
                return NetworkInfo.DefaultIP;
            }
        }

        public void StartEventClient()
        {
            _sce = new SendEventClient(_host_ip, NetworkInfo.EventPort);
        }

        public void Start(string host_ip)
        {
            _host_ip = host_ip;
            _img_server = new ImageServer(MyIP, NetworkInfo.ImgPort);
            _img_server._RecvedImage += Img_server_RecvedImage;
            SetupClinet.Setup(host_ip, NetworkInfo.SetupPort);
        }

        private void Img_server_RecvedImage(object sender, RecvImageEventArgs e)
        {
            if(null != _RecvedImage)
            {
                _RecvedImage(this, e);
            }
        }

        public void Stop()
        {
            if(null != _img_server)
            {
                _img_server.Close();
                _img_server = null;
            }
        }
    }
}
