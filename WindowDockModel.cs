using System;
using System.Windows.Forms;
using static WndRz.KeyboardHooker;

namespace WndRz
{
    internal class WindowDockModel
    {
        internal event EventHandler WindowTopLeft;
        internal event EventHandler WindowTopRight;
        internal event EventHandler WindowBottomLeft;
        internal event EventHandler WindowBottomRight;
        internal event EventHandler WindowTop;
        internal event EventHandler WindowBottom;
        private bool _isCtrlKeyDown;
        private bool _isWinkeyDown;
        private bool _isAltKeyDown;
        private KeyState _keyState;

        private enum KeyState
        {
            Waiting,
            Top,
            Bottom,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        internal WindowDockModel()
        {
            _isWinkeyDown = false;
            _isCtrlKeyDown = false;
            _isAltKeyDown = false;
            _keyState = KeyState.Waiting;
        }

        internal void OnKeyboardEvent(bool isDown, int vkCode)
        {
            if (vkCode == KeyboardHooker.WM_LEFT_CTRL_KEY)
            {
                _isCtrlKeyDown = isDown;
            }
            else if (vkCode == KeyboardHooker.WM_LEFT_WIN_KEY)
            {
                _isWinkeyDown = isDown;
            }
            else if (vkCode == KeyboardHooker.WM_LEFT_ALT_KEY)
            {
                _isAltKeyDown = isDown;
            }

            if (!isDown)
            {
                _keyState = KeyState.Waiting;
                return;
            }

            if (IsMoveWindowLeftModifiersPressed())
            {
                if (vkCode == KeyboardHooker.WM_UP_KEY)
                {
                    ChangeState(KeyState.TopLeft);
                }
                else if (vkCode == KeyboardHooker.WM_DOWN_KEY)
                {
                    ChangeState(KeyState.BottomLeft);
                }
            }
            else if (IsMoveWindowRightModifierPressed())
            {
                if (vkCode == KeyboardHooker.WM_UP_KEY)
                {
                    ChangeState(KeyState.TopRight);
                }
                else if (vkCode == KeyboardHooker.WM_DOWN_KEY)
                {
                    ChangeState(KeyState.BottomRight);
                }
            }
            else if (IsMoveWindowTopModifiersPressed())
            {
                if (vkCode == KeyboardHooker.WM_UP_KEY)
                {
                    ChangeState(KeyState.Top);
                }
                else if (vkCode == KeyboardHooker.WM_DOWN_KEY)
                {
                    ChangeState(KeyState.Bottom);
                }
            }
            else if (IsMoveWindowDownModifiersPressed())
            {
                if (vkCode == KeyboardHooker.WM_UP_KEY)
                {
                    ChangeState(KeyState.Top);
                }
                else if (vkCode == KeyboardHooker.WM_DOWN_KEY)
                {
                    ChangeState(KeyState.Bottom);
                }
            }
        }

        private void ChangeState(KeyState state)
        {
            if (_keyState == state)
            {
                return;
            }
            _keyState = state;

            if (_keyState == KeyState.TopLeft)
            {
                if (WindowTopLeft != null)
                {
                    WindowTopLeft(this, new EventArgs());
                }
            }
            else if (_keyState == KeyState.BottomLeft)
            {
                if (WindowBottomLeft != null)
                {
                    WindowBottomLeft(this, new EventArgs());
                }
            }
            else if (_keyState == KeyState.TopRight)
            {
                if (WindowTopRight != null)
                {
                    WindowTopRight(this, new EventArgs());
                }
            }
            else if (_keyState == KeyState.BottomRight)
            {
                if (WindowBottomRight != null)
                {
                    WindowBottomRight(this, new EventArgs());
                }
            }
            else if (_keyState == KeyState.Top)
            {
                if (WindowTop != null)
                {
                    WindowTop(this, new EventArgs());
                }
            }
            else if (_keyState == KeyState.Bottom)
            {
                if (WindowBottom != null)
                {
                    WindowBottom(this, new EventArgs());
                }
            }
        }

        private bool IsMoveWindowTopModifiersPressed()
        {
            return _isCtrlKeyDown && _isWinkeyDown && _isAltKeyDown;
        }

        private bool IsMoveWindowDownModifiersPressed()
        {
            return _isCtrlKeyDown && _isWinkeyDown && _isAltKeyDown;
        }

        private bool IsMoveWindowLeftModifiersPressed()
        {
            return _isCtrlKeyDown && _isWinkeyDown && !_isAltKeyDown;
        }

        private bool IsMoveWindowRightModifierPressed()
        {
            return !_isCtrlKeyDown && _isWinkeyDown && _isAltKeyDown;
        }
    }
}