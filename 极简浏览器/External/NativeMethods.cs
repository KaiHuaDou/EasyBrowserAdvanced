using System;
using System.Runtime.InteropServices;

namespace 极简浏览器.External;
internal sealed class NativeMethods
{
    internal enum AccentState
    {
        ACCENT_DISABLED,
        ACCENT_ENABLE_GRADIENT,
        ACCENT_ENABLE_TRANSPARENTGRADIENT,
        ACCENT_ENABLE_BLURBEHIND,
        ACCENT_INVALID_STATE,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        // ...
        WCA_ACCENT_POLICY = 19,
        // ...
    }

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(
        IntPtr hwnd, ref WindowCompositionAttributeData data);
}
