using System;
using System.Windows.Forms;

namespace WndRz
{
    internal class HotKey
    {
        private int modifier;
        private Keys key;
        private Action handle;

        public HotKey(int modifier, Keys key, Action handle)
        {
            this.modifier = modifier;
            this.key = key;
            this.handle = handle;
        }

        public Action Handle { get { return this.handle; } }
    }
}
