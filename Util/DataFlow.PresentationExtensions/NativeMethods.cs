using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace AISTek.DataFlow.PresentationExtensions
{
    internal static class NativeMethods
    {
        [DllImport("dwmapi.dll", PreserveSig = false, EntryPoint = "DwmExtendFrameIntoClientArea")]
        internal static extern void ExtendFrameIntoClientArea(IntPtr hwnd, ref Margins margins);

        [DllImport("dwmapi.dll", PreserveSig = false, EntryPoint = "DwmIsCompositionEnabled")]
        internal static extern bool IsCompositionEnabled();

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        internal static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        internal static extern int GetMenuItemCount(IntPtr hmenu);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        internal static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        internal static extern int DrawMenuBar(IntPtr hwnd);

        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                   int x, int y, int width, int height, uint flags);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hwnd, uint msg,
                   IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        internal static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);
       
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        internal static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        
        internal const int GwlExStyle = -20;
        internal const int WsExDlgmodalFrame = 0x0001;
        internal const int SwpNoSize = 0x0001;
        internal const int SwpNoMove = 0x0002;
        internal const int SwpNoZorder = 0x0004;
        internal const int SwpFrameChanged = 0x0020;
        internal const uint WmSetIcon = 0x0080;
        internal const int MfByPosition = 0x0400;
        internal const int MfDisabled = 0x0002;
        internal const int GwlStyle = (-16);
        internal const int WsSysMenu = 0x00080000;

        internal static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);

            return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        internal struct Margins
        {
            public Margins(Thickness t)
            {
                Left = (int)t.Left;
                Right = (int)t.Right;
                Top = (int)t.Top;
                Bottom = (int)t.Bottom;
            }

            public int Left;

            public int Right;

            public int Top;

            public int Bottom;
        }
    }
}
