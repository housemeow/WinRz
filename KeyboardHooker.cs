using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WndRz
{
    internal class KeyboardHooker
    {
        internal delegate void KeyboardHandler(bool isDown, int vkCode);
        internal event KeyboardHandler KeyboardEvent;
        internal class HookException : Exception { }
        internal class UnhookException : Exception { }

        internal const int WH_KEYBOARD_LL = 13;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_SYSKEYUP = 0x0105;

        internal const int WM_LEFT_CTRL_KEY = 162;
        internal const int WM_LEFT_WIN_KEY = 91;
        internal const int WM_LEFT_ALT_KEY = 164;
        internal const int WM_UP_KEY = 38;
        internal const int WM_DOWN_KEY = 40;


        private int m_HookHandle = 0;    // Hook handle
        private HookProc m_KbdHookProc;            // 鍵盤掛鉤函式指標

        // 設置掛鉤.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        // 將之前設置的掛鉤移除。記得在應用程式結束前呼叫此函式.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        // 呼叫下一個掛鉤處理常式（若不這麼做，會令其他掛鉤處理常式失效）.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

        public delegate int HookProc(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        private static KeyboardHooker _hooker;

        private KeyboardHooker() { }

        public static KeyboardHooker GetInstance()
        {
            if (_hooker == null)
            {
                _hooker = new KeyboardHooker();
            }
            return _hooker;
        }

        internal void Hook()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                m_KbdHookProc = new HookProc(KeyboardHookProc);

                m_HookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, m_KbdHookProc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }

            if (m_HookHandle == 0)
            {
                throw new HookException();
            }
        }

        public static int KeyboardHookProc(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            bool isDown = (wParam.ToInt32() == KeyboardHooker.WM_KEYDOWN) ||
                    (wParam.ToInt32() == KeyboardHooker.WM_SYSKEYDOWN);
            KeyboardHooker.GetInstance().KeyboardEvent?.Invoke(isDown, lParam.vkCode);
            return CallNextHookEx(0, nCode, wParam, ref lParam);
        }

        internal void Unhook()
        {
            bool ret = UnhookWindowsHookEx(m_HookHandle);
            if (ret == false)
            {
                throw new UnhookException();
            }
            m_HookHandle = 0;
        }
    }
}