using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using KeyEmulator.WindowWorkers;

namespace KeyEmulator.MouseWorker
{
    public class WorkerWithMouse
    {
        #region function imports
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out PointInter lpPoint);       
        #endregion

        #region static methods
        /// <summary>
        /// moves the mouse
        /// </summary>
        /// <param name="x">x position to move to</param>
        /// <param name="y">y position to move to</param>
        public static void MouseMove(int x, int y)
        {
            SetCursorPos(x, y);
        }

        /// <summary>
        /// checks for the currently active window then simulates a mouseclick
        /// </summary>
        /// <param name="button">which button to press (left middle up)</param>
        /// <param name="windowName">the window to send to</param>
        public static void MouseClick(MouseButtons button, string windowName)
        {
            if (WorkerWithWindows.WindowActive(windowName))
                MouseClick(button);
        }
        /// <summary>
        /// sends a mouseclick to a window state=1 lifts it up state=0 presses it down
        /// </summary>
        /// <param name="button"></param>
        /// <param name="state"></param>
        public static void MouseClick(MouseButtons button, MouseState state)
        {
            switch (button)
            {
                case MouseButtons.left:
                    switch (state)
                    {
                        case MouseState.up:
                            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
                            break;
                        case MouseState.down:
                            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
                            break;
                    }
                    break;
                case MouseButtons.right:
                    switch (state)
                    {
                        case MouseState.up:
                            mouse_event((uint)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
                            break;
                        case MouseState.down:
                            mouse_event((uint)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);
                            break;
                    }
                    break;
                case MouseButtons.middle:
                    switch (state)
                    {
                        case MouseState.up:
                            mouse_event((uint)MouseEventFlags.MIDDLEUP, 0, 0, 0, 0);
                            break;
                        case MouseState.down:
                            mouse_event((uint)MouseEventFlags.MIDDLEDOWN, 0, 0, 0, 0);
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// simulates a mouse click see http://pinvoke.net/default.aspx/user32/mouse_event.html?diff=y
        /// </summary>
        /// <param name="button">which button to press (left middle up)</param>
        public static void MouseClick(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.left:
                    MouseClick(button, MouseState.down);
                    MouseClick(button, MouseState.up);
                    break;
                case MouseButtons.right:
                    MouseClick(button, MouseState.down);
                    MouseClick(button, MouseState.up);
                    break;
                case MouseButtons.middle:
                    MouseClick(button, MouseState.down);
                    MouseClick(button, MouseState.up);
                    break;
            }
        }


        /// <summary>
        /// Получаеи координаты мыши на экране
        /// https://stackoverflow.com/questions/1316681/getting-mouse-position-in-c-sharp
        /// </summary>
        /// <returns></returns>
        public static PointInter GetCursorPosition()
        {
            PointInter lpPoint;
            GetCursorPos(out lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }
        #endregion
    }
}
