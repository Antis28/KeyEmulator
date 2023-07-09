/*
 * hypnodok #elitepvpers quakenet
 * 8.5.08
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace KeyboardEmulator
{
    public static class WinAPI
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
           UIntPtr dwExtraInfo);


        private const int SRCCOPY = 0x00CC0020;
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hObject, int nXDest, int
                                            nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int
                                            nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int
                                                             nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        

        #region static methods

        /// <summary>
        /// simulates a keypress, see http://msdn2.microsoft.com/en-us/library/system.windows.forms.sendkeys(VS.71).aspx
        /// no winapi but this works just fine for me
        /// </summary>
        /// <param name="keys">the keys to press</param>
        public static void ManagedSendKeys(string keys)
        {
            //  SendKeys.SendWait(keys);
        }

        /// <summary>
        /// checks if the correct window is active, then send keypress
        /// </summary>
        /// <param name="keys">keys to press</param>
        /// <param name="windowName">window to send the keys to</param>
        public static void ManagedSendKeys(string keys, string windowName)
        {
            if (WindowActive(windowName))
            {
                ManagedSendKeys(keys);
            }
        }

        

       

        

       

       
        

        

       

       

        
        #endregion

        /// <summary>
        /// makes a screenshot of your current desktop and returns a bitmap
        /// </summary>
        /// <returns></returns>
        //public static Bitmap CreateScreenshot()
        //{
        //    IntPtr hWnd = GetDesktopWindow();
        //    IntPtr hSorceDC = GetWindowDC(hWnd);
        //    RECT rect = new RECT();
        //    GetWindowRect(hWnd, ref rect);
        //    int width = rect.right - rect.left;
        //    int height = rect.bottom - rect.top;
        //    IntPtr hDestDC = CreateCompatibleDC(hSorceDC);
        //    IntPtr hBitmap = CreateCompatibleBitmap(hSorceDC, width, height);
        //    IntPtr hObject = SelectObject(hDestDC, hBitmap);
        //    BitBlt(hDestDC, 0, 0, width, height, hSorceDC, 0, 0, SRCCOPY);
        //    SelectObject(hDestDC, hObject);
        //    DeleteDC(hDestDC);
        //    ReleaseDC(hWnd, hSorceDC);
        //    Bitmap screenshot = Bitmap.FromHbitmap(hBitmap);
        //    DeleteObject(hBitmap);
        //    return screenshot;
        //}

    }
}