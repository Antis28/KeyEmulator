using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KeyboardEmulator
{
    public class ProcessFinder
    {
        private const int WM_SETTEXT = 0xC;

        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;
        public int pID;

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(IntPtr className, string windowName);
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, string lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wparam, int lparam);


        /// <summary>
        /// Находит процесс по имени и выводит его полное имя и хэндл
        /// </summary>
        /// <param name="nameProc"></param>
        public IntPtr ScanPrcesses(string nameProc)
        {
            IntPtr hWnd = IntPtr.Zero;
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in process)
            {

                if (pro.ProcessName.Contains(nameProc))
                    try
                    {

                        hWnd = pro.MainWindowHandle;
                        //SendMessage(hWnd, WM_SETTEXT, 0, "Ура!Поймали!");
                        Console.WriteLine(pro.ProcessName);
                        Console.WriteLine("Handle = " + pro.Handle);
                        Console.WriteLine("hWnd = " + hWnd);

                        break;

                    }
                    catch { Console.WriteLine("Ошибка!!!"); }                
            }
            return hWnd;
        }

        public IntPtr GetActiveProcesses()
        {
            IntPtr hWnd = GetForegroundWindow();
            int pid = 0;
            GetWindowThreadProcessId(hWnd, ref pid);
            Process p = Process.GetProcessById(pid);
            Console.Write("pid: {0}; window: {1}", pid, p.MainWindowTitle);
            return hWnd;
        }
        public Process GetActiveProcess()
        {
            IntPtr hWnd = GetForegroundWindow();
            int pid = 0;
            GetWindowThreadProcessId(hWnd, ref pid);
            return Process.GetProcessById(pid);;
        }
    }
}
