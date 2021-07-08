using System.Drawing;
using System.Runtime.InteropServices;

namespace 원격제어프로그램
{

    public enum KeyFlag
    {
        KEY_DOWN = 0,
        KEY_EXTENDEDKEY,
        KEY_UP
    }
    public enum MouseFlag
    {
        MOUSE_MOVE      = 1,
        MOUSE_LEFTDOWN  = 2,
        MOUSE_LEFTUP    = 4,
        MOUSE_RIGHTDOWN = 8,
        MOUSE_RIGHTUP   = 0x10,
        MOUSE_MIDDLEDOWN= 0x20,
        MOUSE_MIDDLEUP  = 0x40,
        MOUSE_WHEEL     = 0x800,
        MOUSE_ABSOULUTE = 8000
    }

    public  static class WrapNative
    {

        [DllImport("user32.dll")]
        static extern void keybd_event(byte vk, byte scan, int flags, int extra);

        [DllImport("user32.dll")]
        static extern void mouse_event(int flags, int dx, int dy, int button, int extra);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point point);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        public static void KeyDown(int keycode)
        {
            keybd_event((byte)keycode, 0, (int)KeyFlag.KEY_DOWN, 0);
        }

        public static void KeyUp(int keycode)
        {
            keybd_event((byte)keycode, 0, (int)KeyFlag.KEY_UP, 1);
        }

        public static void Move(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void Move(Point pt)
        {
            SetCursorPos(pt.X, pt.Y);
        }

        public static void LeftDown()
        {
            mouse_event((int)MouseFlag.MOUSE_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void LeftUP()
        {
            mouse_event((int)MouseFlag.MOUSE_LEFTUP, 0, 0, 0, 0);
        }

        public static void RightDown()
        {
            mouse_event((int)MouseFlag.MOUSE_RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void RightUp()
        {
            mouse_event((int)MouseFlag.MOUSE_RIGHTUP, 0, 0, 0, 0);
        }
    }
}
