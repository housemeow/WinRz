using System;
using System.Windows.Forms;
using static WndRz.WindowResizer;

namespace WndRz
{
    internal class WndRzModel
    {
        private int _screenWidth;
        private int _screenHeight;

        public WndRzModel(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        private RECT Trim(RECT rect)
        {
            rect.Left = rect.Left > 0 ? rect.Left : 0;
            rect.Top = rect.Top > 0 ? rect.Top : 0;
            rect.Bottom = rect.Bottom < _screenHeight ? rect.Bottom : _screenHeight;
            rect.Right = rect.Right < _screenWidth ? rect.Right : _screenWidth;
            return rect;
        }

        internal RECT GetNewTopRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Left = rect.Left;
            newRect.Top = 0;
            newRect.Right = rect.Right;

            int height = rect.Bottom - rect.Top;
            if (height < _screenHeight / 3)
            {
                newRect.Bottom = newRect.Top + _screenHeight / 3;
            }
            else if (height < _screenHeight / 2)
            {
                newRect.Bottom = newRect.Top + _screenHeight / 2;
            }
            else if (height < _screenHeight * 2 / 3)
            {
                newRect.Bottom = newRect.Top + _screenHeight * 2 / 3;
            }
            else if (height < _screenHeight)
            {
                newRect.Bottom = newRect.Top + _screenHeight;
            }
            else
            {
                newRect.Bottom = newRect.Top + _screenHeight / 3;
            }

            newRect = Trim(newRect);
            return newRect;
        }

        internal RECT GetNewFullTopRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Left = 0;
            newRect.Top = 0;
            newRect.Right = _screenWidth;
            newRect.Bottom = rect.Bottom - rect.Top;

            newRect = Trim(newRect);
            return newRect;
        }

        internal RECT GetNewBottomRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Left = rect.Left;
            newRect.Bottom = _screenHeight;
            newRect.Right = rect.Right;

            int height = rect.Bottom - rect.Top;
            if (height < _screenHeight / 3)
            {
                newRect.Top = newRect.Bottom - _screenHeight / 3;
            }
            else if (height < _screenHeight / 2)
            {
                newRect.Top = newRect.Bottom - _screenHeight / 2;
            }
            else if (height < _screenHeight * 2 / 3)
            {
                newRect.Top = newRect.Bottom - _screenHeight * 2 / 3;
            }
            else if (height < _screenHeight)
            {
                newRect.Top = newRect.Bottom - _screenHeight;
            }
            else
            {
                newRect.Top = newRect.Bottom - _screenHeight / 3;
            }

            newRect = Trim(newRect);
            return newRect;
        }

        internal RECT GetNewFullBottomRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Left = 0;
            newRect.Bottom = _screenHeight;
            newRect.Right = _screenWidth;
            newRect.Top = _screenHeight - (rect.Bottom - rect.Top);

            newRect = Trim(newRect);
            return newRect;
        }

        internal RECT GetNewLeftRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Bottom = rect.Bottom;
            newRect.Top = rect.Top;
            newRect.Left = 0;

            int width = rect.Right - rect.Left;
            if (width < _screenWidth / 3)
            {
                newRect.Right = newRect.Left + _screenWidth / 3;
            }
            else if (width < _screenWidth / 2)
            {
                newRect.Right = newRect.Left + _screenWidth / 2;
            }
            else if (width < _screenWidth * 2 / 3)
            {
                newRect.Right = newRect.Left + _screenWidth * 2 / 3;
            }
            else if (width < _screenWidth)
            {
                newRect.Right = newRect.Left + _screenWidth;
            }
            else
            {
                newRect.Right = newRect.Left + _screenWidth / 3;
            }

            newRect = Trim(newRect);
            return newRect;
        }

        internal RECT GetNewFullLeftRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Bottom = _screenHeight;
            newRect.Top = 0;
            newRect.Left = 0;
            newRect.Right = rect.Right - rect.Left;

            newRect = Trim(newRect);
            return newRect;
        }

        internal RECT GetNewRightRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Bottom = rect.Bottom;
            newRect.Top = rect.Top;
            newRect.Right = _screenWidth;

            int width = rect.Right - rect.Left;
            if (width < _screenWidth / 3)
            {
                newRect.Left = newRect.Right - _screenWidth / 3;
            }
            else if (width < _screenWidth / 2)
            {
                newRect.Left = newRect.Right - _screenWidth / 2;
            }
            else if (width < _screenWidth * 2 / 3)
            {
                newRect.Left = newRect.Right - _screenWidth * 2 / 3;
            }
            else if (width < _screenWidth)
            {
                newRect.Left = newRect.Right - _screenWidth;
            }
            else
            {
                newRect.Left = newRect.Right - _screenWidth / 3;
            }

            newRect = Trim(newRect);
            return newRect;
        }

        internal RECT GetNewFullRightRect(RECT rect)
        {
            RECT newRect = new RECT();
            newRect.Bottom = _screenHeight;
            newRect.Top = 0;
            newRect.Right = _screenWidth;
            newRect.Left = _screenWidth - (rect.Right - rect.Left);

            newRect = Trim(newRect);
            return newRect;
        }
    }
}
