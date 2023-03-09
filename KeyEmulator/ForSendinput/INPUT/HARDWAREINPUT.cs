using System.Runtime.InteropServices;

namespace KeyboardEmulator.ForSendInput;

[StructLayout(LayoutKind.Sequential)]
internal struct HARDWAREINPUT
{
    internal int uMsg;
    internal short wParamL;
    internal short wParamH;
}
