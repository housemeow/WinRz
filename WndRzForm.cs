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
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Up, OnWindowFullTop);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Down, OnWindowFullBottom);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Left, OnWindowFullLeft);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Right, OnWindowFullRight);

            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Up, OnWindowTop);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Down, OnWindowBottom);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Left, OnWindowLeft);
            _keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Right, OnWindowRight);

            //_keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN | MOD_ALT, Keys.Down, OnWindowBottom);
            //_keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Up, OnWindowTopLeft);
            //_keyboardHooker.Register(Handle, MOD_CONTROL | MOD_WIN, Keys.Down, OnWindowBottomLeft);
            //_keyboardHooker.Register(Handle, MOD_WIN | MOD_ALT, Keys.Up, OnWindowTopRight);
            //_keyboardHooker.Register(Handle, MOD_WIN | MOD_ALT, Keys.Down, OnWindowBottomRight);
        }

        private void OnWindowFullTop()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewFullTopRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Full top");
        }

        private void OnWindowFullBottom()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewFullBottomRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Full bottom");
        }

        private void OnWindowFullLeft()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewFullLeftRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Full left");
        }

        private void OnWindowFullRight()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewFullRightRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Full right");
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
            Console.WriteLine("Bottom");
        }

        private void OnWindowLeft()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewLeftRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Left");
        }

        private void OnWindowRight()
        {
            RECT rect = _windowResizer.GetCurrentWindowRect();
            RECT newRect = _model.GetNewRightRect(rect);
            _windowResizer.Resize(newRect);
            Console.WriteLine("Right");
        }

        private void ClickClose(object sender, EventArgs e)
        {
            Close();
        }
    }
}
