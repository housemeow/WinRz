using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WndRz
{
    internal class WindowResizer
    {
        public const int SW_HIDE = 0; // 完全消失 
        public const int SW_SHOWNORMAL = 1; // 正常狀態 
        public const int SW_SHOWMINIMIZED = 2; // 縮小 
        public const int SW_SHOWMAXIMIZED = 3; // 全螢幕 
        private RECT workingBound;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TagWindowPlacement
        {
            public uint length;
            public uint flags;
            public uint showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public RECT rcNormalPosition;
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

        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out TagWindowPlacement lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref TagWindowPlacement lpwndpl);

        private WindowResizer() { }

        internal static WindowResizer GetInstance()
        {
            if (_windowResizer == null)
            {
                _windowResizer = new WindowResizer();
            }

            return _windowResizer;
        }

        internal void PreventMaximizedWindow(IntPtr handle)
        {
            TagWindowPlacement windowPlacement;
            GetWindowPlacement(handle, out windowPlacement);

            if (windowPlacement.showCmd == SW_SHOWMAXIMIZED)
            {
                windowPlacement.showCmd = SW_SHOWNORMAL;
            }

            SetWindowPlacement(handle, ref windowPlacement);
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
            IntPtr currentHandle = GetForegroundWindow();
            PreventMaximizedWindow(currentHandle);
            int width = newRect.Right - newRect.Left;
            int height = newRect.Bottom - newRect.Top;
            MoveWindow(currentHandle, newRect.Left, newRect.Top, width, height, true);
        }
    }
}
