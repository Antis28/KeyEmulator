using System.Runtime.InteropServices;
using System.Windows; // Or use whatever point class you like for the implicit cast operator


namespace KeyEmulator.MouseWorker
{
    /// <summary>
    /// Struct representing a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PointInter
    {
        public int X;
        public int Y;

        //public static implicit operator Point(POINT point)
        //{
        //    return new Point(point.X, point.Y);
        //}
    }
}
