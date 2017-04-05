using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WndRz
{
    internal class KeyboardHooker
    {
        Dictionary<int, HotKey> registers;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        private static KeyboardHooker _hooker;

        private KeyboardHooker()
        {
            registers = new Dictionary<int, HotKey>();
        }

        public static KeyboardHooker GetInstance()
        {
            if (_hooker == null)
            {
                _hooker = new KeyboardHooker();
            }
            return _hooker;
        }

        internal void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                if (registers.ContainsKey(id))
                {
                    registers[id].Handle();
                }
            }
        }

        internal void Register(IntPtr handle, int modifier, Keys key, Action handleHotKey)
        {
            int id = registers.Count;
            registers.Add(id, new HotKey(modifier, key, handleHotKey));
            RegisterHotKey(handle, id, modifier, key);
        }

        internal void Unregister(IntPtr handle)
        {
            foreach (var register in registers)
            {
                UnregisterHotKey(handle, register.Key);
            }
            registers.Clear();
        }
    }
}
