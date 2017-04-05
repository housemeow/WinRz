using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WndRz
{
    internal class WindowResizer
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }


        private static WindowResizer _windowResizer;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowDisplayAffinity(IntPtr hWnd, out int dwAffinity);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

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

        internal RECT GetCurrentWindowRect()
        {
            RECT rect;
            GetWindowRect(GetForegroundWindow(), out rect);
            Console.WriteLine(rect);
            return rect;
        }

        internal void Resize(RECT newRect)
        {
            IntPtr currentHWnd = GetForegroundWindow();
            int width = newRect.Right - newRect.Left;
            int height = newRect.Bottom - newRect.Top;
            MoveWindow(currentHWnd, newRect.Left, newRect.Top, width, height, true);
        }
    }
}