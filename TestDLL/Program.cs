using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyboardEmulator;
using KeyEmulator.WindowWorkers;

namespace TestDLL
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            IntPtr hWnd = WorkerWithWindows.GetDesktopWindow();
            RECT rect = new RECT();
            WorkerWithWindows.GetWindowRect(hWnd, out rect);
            
            WinAPI.MouseMove(rect.right -170, rect.bottom -125);
        }
    }
}
