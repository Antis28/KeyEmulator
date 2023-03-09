using System;
using System.Runtime.InteropServices;

namespace KeyboardEmulator.ForSendInput;

[StructLayout(LayoutKind.Sequential)]
internal struct KEYBDINPUT
{
    internal VirtualKeyShort wVk;
    internal ScanCodeShort wScan;
    internal KeyEventF dwFlags;
    internal int time;
    internal UIntPtr dwExtraInfo;
}
