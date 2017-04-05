using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WndRz.WindowResizer;

namespace WndRz
{
    public partial class WndRzForm : Form
    {
        int _screenWidth;
        int _screenHeight;
        KeyboardHooker _keyboardHooker;
        WindowResizer _windowResizer;
        WndRzModel _model;

        public const int MOD_ALT = 0x0001;
        public const int MOD_CONTROL = 0x0002;
        public const int MOD_NOREPEAT = 0x4000;

        public const int MOD_SHIFT = 0x0004;
        public const int MOD_WIN = 0x0008;

        public WndRzForm()
        {
            InitializeComponent();
            _keyboardHooker = KeyboardHooker.GetInstance();
            _windowResizer = WindowResizer.GetInstance();
            _screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            _screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            _model = new WndRzModel(_screenWidth, _screenHeight);
        }

        // Passing event to keyboard hooker to handle HotKey message
        protected override void WndProc(ref Message m)
        {
            _keyboardHooker.WndProc(ref m);
            base.WndProc(ref m);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            Visible = false;
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Up, OnWindowTop);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Down, OnWindowBottom);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Left, OnWindowLeft);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Right, OnWindowRight);

            //_keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Down, OnWindowBottom);
            //_keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Up, OnWindowTopLeft);
            //_keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Down, OnWindowBottomLeft);
            //_keyboardHooker.Register(Handle, MOD_WIN | MOD_ALT, Keys.Up, OnWindowTopRight);
            //_keyboardHooker.Register(Handle, MOD_WIN | MOD_ALT, Keys.Down, OnWindowBottomRight);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _keyboardHooker.Unregister(Handle);
        }

        private void OnWindowTop()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewTopRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Top");
        }

        private void OnWindowBottom()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewBottomRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Top");
        }

        private void OnWindowLeft()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewLeftRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Top");
        }

        private void OnWindowRight()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewRightRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Top");
        }

        //private void OnWindowBottom()
        //{
        //    _windowResizer.Resize(0, _screenHeight / 2, _screenWidth, _screenHeight / 2);
        //    Console.WriteLine("Bottom");
        //}

        //private void OnWindowTopLeft()
        //{
        //    _windowResizer.Resize(0, 0, _screenWidth / 2, _screenHeight / 2);
        //    Console.WriteLine("TopLeft");
        //}

        //private void OnWindowBottomLeft()
        //{
        //    _windowResizer.Resize(0, _screenHeight / 2, _screenWidth / 2, _screenHeight / 2);
        //    Console.WriteLine("BottomLeft");
        //}

        //private void OnWindowTopRight()
        //{
        //    _windowResizer.Resize(_screenWidth / 2, 0, _screenWidth / 2, _screenHeight / 2);
        //    Console.WriteLine("TopRight");
        //}

        //private void OnWindowBottomRight()
        //{
        //    _windowResizer.Resize(_screenWidth / 2, _screenHeight / 2, _screenWidth / 2, _screenHeight / 2);
        //    Console.WriteLine("BottomRight");
        //}

        private void ClickClose(object sender, EventArgs e)
        {
            Close();
        }
    }
}
