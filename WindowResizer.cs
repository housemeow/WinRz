using System;
using System.Runtime.InteropServices;

namespace WndRz
{
    internal class WindowResizer
    {
        private static WindowResizer _windowResizer;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowDisplayAffinity(IntPtr hWnd, out int dwAffinity);

        private WindowResizer() { }

        internal static WindowResizer GetInstance()
        {
            if (_windowResizer == null)
            {
                _windowResizer = new WindowResizer();
            }

            return _windowResizer;
        }

        internal void Resize(int x, int y, int width, int height)
        {
            IntPtr currentHWnd = GetForegroundWindow();
            MoveWindow(currentHWnd, x, y, width, height, true);
        }
    }
}