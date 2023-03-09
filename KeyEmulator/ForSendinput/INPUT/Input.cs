using System;
using System.Runtime.InteropServices;

namespace KeyboardEmulator.ForSendInput;
//Used by [user32.SendInput] to store information for synthesizing input events such as keystrokes, mouse movement, and mouse clicks.
[StructLayout(LayoutKind.Sequential)]
public struct Input
{
    internal InputType type;
    internal InputUnion U;
    internal static int Size
    {
        get { return Marshal.SizeOf(typeof(Input)); }
    }
}

public enum InputType : uint
{
    INPUT_MOUSE,
    INPUT_KEYBOARD,
    INPUT_HARDWARE
}

[StructLayout(LayoutKind.Explicit)]
internal struct InputUnion
{
    [FieldOffset(0)]
    internal MOUSEINPUT mi;
    [FieldOffset(0)]
    internal KEYBDINPUT ki;
    [FieldOffset(0)]
    internal HARDWAREINPUT hi;
}

