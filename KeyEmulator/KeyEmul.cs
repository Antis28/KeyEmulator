using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using KeyboardEmulator.ForSendInput;
using KeyboardEmulator.ForPostMessage;

//////
/// var hWnd = procFinder.ScanPrcesses("PotPlayer");
/// new KeyEmul().PostClick(hWnd);

namespace KeyboardEmulator
{
    public class KeyEmul
    {
        [DllImport("user32.dll", SetLastError = false)]
        static extern UIntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd,  // handle to destination window
                                               UInt32 Msg,   // message
                                               Int32 wParam, // first message parameter
                                               Int32 lParam  // second message parameter
        );

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className,
                                                 string windowName);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, string lParam);


        [DllImport("user32.dll")] static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);


        // подключить System.Drawing
        //public void PostClick(IntPtr hWnd, Point pt)
        //{
        //    PostMessage(hWnd, (UInt32)WM.LBUTTONDOWN, 1, ((pt.X << 16) | (pt.Y & 0xFFFF)));
        //    PostMessage(hWnd, (UInt32)WM.LBUTTONUP, 1, ((pt.X << 16) | (pt.Y & 0xFFFF)));
        //}

        public void PostClick(IntPtr hWnd, Int32 key)
        {
            PostMessage(hWnd, (UInt32)WM.KEYDOWN, key, 0);
            PostMessage(hWnd, (UInt32)WM.KEYUP, key, 0);
        }

        public void PostDownButton(IntPtr hWnd, Int32 key)
        {
            PostMessage(hWnd, (UInt32)WM.KEYDOWN, key, 0);
        }

        public void PostUpButton(IntPtr hWnd, Int32 key)
        {
            PostMessage(hWnd, (UInt32)WM.KEYUP, key, 0);
        }

        public void PostClick(IntPtr hWnd, Int32 key, Int32 key2)
        {
            SendMessage(hWnd, (UInt32)WM.KEYDOWN, key, "1");
            SendMessage(hWnd, (UInt32)WM.KEYDOWN, key2, "2");
            SendMessage(hWnd, (UInt32)WM.KEYUP, key2, "2");
        }

        /// <summary>
        /// Эмулирует нажатие двух клавиш клавиатуры
        /// </summary>
        /// <param name="firstKey">клавиша нажимается первой и отпускается последней</param>
        /// <param name="secondKey">нажимается между нажатием и отпусканием первой</param>
        public void SendInput(ScanCodeShort firstKey, ScanCodeShort secondKey)
        {
            Input[] inputs = new Input[]
            {
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = 0,
                            wScan = firstKey, // W
                            dwFlags = (KeyEventF.KeyDown | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = 0,
                            wScan = secondKey, // key
                            dwFlags = (KeyEventF.KeyDown | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT()
                        {
                            wVk = 0,
                            wScan = secondKey, // key
                            dwFlags = (KeyEventF.KeyUp | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT()
                        {
                            wVk = 0,
                            wScan = firstKey, // W
                            dwFlags = (KeyEventF.KeyUp | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public void SendInput(ScanCodeShort firstKey)
        {
            Input[] inputs = new Input[]
            {
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = 0,
                            wScan = firstKey, // W
                            dwFlags = (KeyEventF.KeyDown | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT()
                        {
                            wVk = 0,
                            wScan = firstKey, // W
                            dwFlags = (KeyEventF.KeyUp | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }
        public void SendInput(VirtualKeyShort firstKey)
        {
            Input[] inputs = new Input[]
            {
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = firstKey,
                            wScan = 0, // W
                            dwFlags = (KeyEventF.KeyDown | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
                new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT()
                        {
                            wVk = firstKey,
                            wScan = 0, // W
                            dwFlags = (KeyEventF.KeyUp | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                },
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }



        /// <summary>
        /// Эмулирует последовательное нажатие клавиш клавиатуры
        /// </summary>
        /// <param name="keys"></param>
        public void SendInput(params ScanCodeShort[] keys)
        {
            if (keys.Length == 0) return;

            var inputs = new Input[keys.Length * 2];
            for (var i = 0; i < keys.Length; i += 2)
            {
                var scanCodeShort = keys[i];

                inputs[i] = new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = 0,
                            wScan = scanCodeShort, // W
                            dwFlags = (KeyEventF.KeyDown | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                };
                inputs[i + 1] = new Input
                {
                    type = InputType.INPUT_KEYBOARD,
                    U = new InputUnion
                    {
                        ki = new KEYBDINPUT()
                        {
                            wVk = 0,
                            wScan = scanCodeShort, // W
                            dwFlags = (KeyEventF.KeyUp | KeyEventF.Scancode),
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                };
            }

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public void SendInputParal(params ScanCodeShort[] keys)
        {
            if (keys.Length == 0) return;

            var inputs = new Input[keys.Length * 2];
            for (var i = 0; i < keys.Length - 1; i += 2)
            {
                var scanCodeShort = keys[i];
                if (i % 2 == 0)
                {
                    inputs[i] = new Input
                    {
                        type = InputType.INPUT_KEYBOARD,
                        U = new InputUnion
                        {
                            ki = new KEYBDINPUT
                            {
                                wVk = 0,
                                wScan = scanCodeShort, // W
                                dwFlags = (KeyEventF.KeyDown | KeyEventF.Scancode),
                                dwExtraInfo = GetMessageExtraInfo()
                            }
                        }
                    };
                }
                else
                {
                    inputs[i] = new Input
                    {
                        type = InputType.INPUT_KEYBOARD,
                        U = new InputUnion
                        {
                            ki = new KEYBDINPUT()
                            {
                                wVk = 0,
                                wScan = scanCodeShort, // W
                                dwFlags = (KeyEventF.KeyUp | KeyEventF.Scancode),
                                dwExtraInfo = GetMessageExtraInfo()
                            }
                        }
                    };
                }
            }

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }
    }
}
