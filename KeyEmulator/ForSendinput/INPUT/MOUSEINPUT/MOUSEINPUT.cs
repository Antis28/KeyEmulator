using System;
using System.Runtime.InteropServices;

namespace KeyboardEmulator.ForSendInput;

[StructLayout(LayoutKind.Sequential)]
internal struct MOUSEINPUT
{
    internal int dx;
    internal int dy;
    internal int mouseData;
    internal MOUSEEVENTF dwFlags;
    internal uint time;
    internal UIntPtr dwExtraInfo;
}
