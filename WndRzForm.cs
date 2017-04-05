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

namespace WndRz
{
    public partial class WndRzForm : Form
    {
        int _screenWidth;
        int _screenHeight;
        KeyboardHooker _keyboardHooker;
        WindowResizer _windowResizer;
        WindowDockModel _model;

        public WndRzForm()
        {
            InitializeComponent();

            _keyboardHooker = KeyboardHooker.GetInstance();
            _windowResizer = WindowResizer.GetInstance();
            _model = new WindowDockModel();
            _screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            _screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            _keyboardHooker.Hook();
            _keyboardHooker.KeyboardEvent += _model.OnKeyboardEvent;
            _model.WindowTop += OnWindowTop;
            _model.WindowBottom += OnWindowBottom;
            _model.WindowTopLeft += OnWindowTopLeft;
            _model.WindowTopRight += OnWindowTopRight;
            _model.WindowBottomLeft += OnWindowBottomLeft;
            _model.WindowBottomRight += OnWindowBottomRight;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _keyboardHooker.Unhook();
        }

        private void OnWindowTop(object sender, EventArgs e)
        {
            _windowResizer.Resize(0, 0, _screenWidth, _screenHeight / 2);
            Console.WriteLine("Top");
        }

        private void OnWindowBottom(object sender, EventArgs e)
        {
            _windowResizer.Resize(0, _screenHeight / 2, _screenWidth, _screenHeight / 2);
            Console.WriteLine("Bottom");
        }

        private void OnWindowTopLeft(object sender, EventArgs e)
        {
            _windowResizer.Resize(0, 0, _screenWidth / 2, _screenHeight / 2);
            Console.WriteLine("TopLeft");
        }

        private void OnWindowBottomLeft(object sender, EventArgs e)
        {
            _windowResizer.Resize(0, _screenHeight / 2, _screenWidth / 2, _screenHeight / 2);
            Console.WriteLine("BottomLeft");
        }

        private void OnWindowTopRight(object sender, EventArgs e)
        {
            _windowResizer.Resize(_screenWidth / 2, 0, _screenWidth / 2, _screenHeight / 2);
            Console.WriteLine("TopRight");
        }

        private void OnWindowBottomRight(object sender, EventArgs e)
        {
            _windowResizer.Resize(_screenWidth / 2, _screenHeight / 2, _screenWidth / 2, _screenHeight / 2);
            Console.WriteLine("BottomRight");
        }

        private void 關閉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
