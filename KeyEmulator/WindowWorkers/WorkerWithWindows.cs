using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyEmulator.WindowWorkers
{
    public class WorkerWithWindows
    {
        #region function imports

        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);       


        /// <summary>
        /// Позволяет получить размер экрана
        /// https://pinvoke.net/default.aspx/user32/GetWindowRect.html
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, out RECT rect);

        /// <summary>
        /// Окно рабочего стола занимает весь экран. Окно рабочего стола - это область, поверх которой нарисованы другие окна.
        /// </summary>
        /// <returns>функция возвращает дескриптор для окна рабочего стола.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentHandle"></param>
        /// <param name="childAfter"></param>
        /// <param name="className"></param>
        /// <param name="windowTitle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(IntPtr className, string windowName);

        #endregion

        #region static methods

        /// <summary>
        /// Получить размеры экрана по дискриптору(например так: WorkerWithWindows.GetDesktopWindow();)
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void GetWidthWindow(IntPtr hWnd, out int width, out int height)
        {
            RECT rect = new RECT();
            WorkerWithWindows.GetWindowRect(hWnd, out rect);
            width = rect.right - rect.left;
            height = rect.bottom - rect.top;
        }

        /// <summary>
        ///  Получить размеры экрана рабочего стола
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void GetWidthWindow(out int width, out int height)
        {
            IntPtr hWnd = WorkerWithWindows.GetDesktopWindow();
            GetWidthWindow(hWnd, out width, out height);
        }

        /// <summary>
        /// checks if a specified window is currently the topmost one
        /// </summary>
        /// <param name="windowName">the window to check for</param>
        /// <returns>true if windowName machtes the topmost window, false if not</returns>
        public static bool WindowActive(string windowName)
        {
            IntPtr myHandle = FindWindow((IntPtr)null, windowName);
            IntPtr foreGround = GetForegroundWindow();
            if (myHandle != foreGround)
                return false;
            else
                return true;
        }

        /// <summary>
        /// checks if a handle is the active window atm
        /// </summary>
        /// <param name="myHandle"></param>
        /// <returns></returns>
        public static bool WindowActive(IntPtr myHandle)
        {
            IntPtr foreGround = WorkerWithWindows.GetForegroundWindow();
            if (myHandle != foreGround)
                return false;
            else
                return true;
        }

        /// <summary>
        /// makes the specified window the topmost one
        /// </summary>
        /// <param name="windowName">the window to activate</param>
        public static void WindowActivate(string windowName)
        {
            IntPtr myHandle = FindWindow(null, windowName);
            SetForegroundWindow(myHandle);
        }
        /// <summary>
        /// makes the specified window the topmost one
        /// </summary>
        /// <param name="handle">the window handle</param>
        public static void WindowActivate(IntPtr handle)
        {
            SetForegroundWindow(handle);
        }

        /// <summary>
        /// moves a window and resizes it accordingly
        /// </summary>
        /// <param name="x">x position to move to</param>
        /// <param name="y">y position to move to</param>
        /// <param name="windowName">the window to move</param>
        /// <param name="width">the window's new width</param>
        /// <param name="height">the window's new height</param>
        public static void WindowMove(int x, int y, string windowName, int width, int height)
        {
            IntPtr window = FindWindow((IntPtr)null, windowName);
            if (window != IntPtr.Zero)
                MoveWindow(window, x, y, width, height, true);
        }
        /// <summary>
        /// moves a window to a specified position
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="windowName">the window to be moved</param>
        public static void WindowMove(int x, int y, string windowName)
        {
            WindowMove(x, y, windowName, 800, 600);
        }
        #endregion
    }
}
