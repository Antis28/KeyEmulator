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
        public void GetWidthWindow( out int width, out int height)
        {
            IntPtr hWnd = WorkerWithWindows.GetDesktopWindow();
            GetWidthWindow(hWnd, out width, out  height);
        }
    }
}
