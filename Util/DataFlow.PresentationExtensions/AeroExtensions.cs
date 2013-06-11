using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;
using System.Windows.Media;

namespace AISTek.DataFlow.PresentationExtensions
{
    public static class AeroExtensions
    {
        public static bool ExtendGlassFrame(this Window window, Thickness margin)
        {
            Contract.Requires(window != null);

            if (!NativeMethods.IsCompositionEnabled())
                return false;

            var hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero)
                throw new InvalidOperationException("The Window must be shown before extending glass.");

            // Set the background to transparent from both the WPF and Win32 perspectives
            window.Background = Brushes.Transparent;
            HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

            var margins = new NativeMethods.Margins(margin);
            NativeMethods.ExtendFrameIntoClientArea(hwnd, ref margins);
            return true;
        }

        public static void DisableControlBox(this Window window)
        {
            var helper = new WindowInteropHelper(window);
            var windowHandle = helper.Handle; //Get the handle of this window

            var hmenu = NativeMethods.GetSystemMenu(windowHandle, 0);
            var cnt = NativeMethods.GetMenuItemCount(hmenu);

            //remove the button
            NativeMethods.RemoveMenu(hmenu, cnt - 1, NativeMethods.MfDisabled | NativeMethods.MfByPosition);

            //remove the extra menu line
            NativeMethods.RemoveMenu(hmenu, cnt - 2, NativeMethods.MfDisabled | NativeMethods.MfByPosition);
            NativeMethods.DrawMenuBar(windowHandle); //Redraw the menu bar
        }

        public static void HideCloseButton(this Window window)
        {
            Interaction.GetBehaviors(window).Add(new HideCloseButtonBehaviour());
        }

        public static void RemoveIcon(this Window window)
        {
            // Get this window's handle
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            // Change the extended window style to not show a window icon
            int extendedStyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GwlExStyle);
            NativeMethods.SetWindowLong(hwnd, NativeMethods.GwlExStyle, extendedStyle | NativeMethods.WsExDlgmodalFrame);

            // Update the window's non-client area to reflect the changes
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                NativeMethods.SwpNoMove | NativeMethods.SwpNoSize | NativeMethods.SwpNoZorder | NativeMethods.SwpFrameChanged);
        }
    }
}
