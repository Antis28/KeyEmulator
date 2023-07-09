using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyboardEmulator;
using KeyEmulator.WindowWorkers;
using KeyEmulator.MouseWorker;

namespace TestDLL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _ = RunAsync();
            Console.ReadKey();
        }

        static async Task RunAsync()
        {
            await Task.Delay(3000);

            IntPtr hWnd = WorkerWithWindows.GetDesktopWindow();
            RECT rect = new RECT();
            WorkerWithWindows.GetWindowRect(hWnd, out rect);

            WorkerWithMouse.MouseMove(rect.right - 170, rect.bottom - 125);
            WorkerWithMouse.MouseClick(MouseButtons.left);
        }
    }
}
