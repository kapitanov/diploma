using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AISTek.DataFlow.PresentationExtensions
{
    internal sealed class CloseButtonHider
    {
        private readonly WindowInteropHelper interopHelper;
        private int windowLong;
        private bool dirty;

        public Window Window { get; private set; }

        public bool IsButtonHidden { get; private set; }

        private bool IsSourceInitialized
        {
            get { return WindowHandle != IntPtr.Zero; }
        }

        private IntPtr WindowHandle
        {
            get { return interopHelper.Handle; }
        }

        public CloseButtonHider(Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);
            Contract.Ensures(Window == window);

            Window = window;
            interopHelper = new WindowInteropHelper(window);

            if (!IsSourceInitialized)
                Window.SourceInitialized += Window_SourceInitialized;
        }

        public void Hide()
        {
            Contract.Ensures(IsButtonHidden);

            if (IsButtonHidden)
                return;

            IsButtonHidden = true;
            ApplyChanges();
        }

        public void Show()
        {
            Contract.Ensures(!IsButtonHidden);

            if (!IsButtonHidden)
                return;

            IsButtonHidden = false;
            ApplyChanges();
        }

        private void ApplyChanges()
        {
            if (!IsSourceInitialized)
            {
                dirty = true;
                return;
            }

            GetWindowLong();
            SetSysMenu(!IsButtonHidden);
            ApplyWindowLong();
            dirty = false;
        }

        private void GetWindowLong()
        {
            Contract.Requires(IsSourceInitialized);

            windowLong = NativeMethods.GetWindowLong(WindowHandle, NativeMethods.GwlStyle);
        }

        private void ApplyWindowLong()
        {
            Contract.Assert(IsSourceInitialized);

            SetWindowLong();
            RefreshWindow();
        }

        private void RefreshWindow()
        {
            Contract.Requires(IsSourceInitialized);

            var result = NativeMethods.SetWindowPos(WindowHandle, IntPtr.Zero, 0, 0, 100, 100,
                NativeMethods.SwpFrameChanged | NativeMethods.SwpNoMove | NativeMethods.SwpNoSize);

            if (!result)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private void SetWindowLong()
        {
            Contract.Requires(IsSourceInitialized);

            IntPtr result = NativeMethods.SetWindowLongPtr(WindowHandle, NativeMethods.GwlStyle, new IntPtr(windowLong));
            if (result == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private void SetSysMenu(bool value)
        {
            windowLong = windowLong.Set(NativeMethods.WsSysMenu, value);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            Window.SourceInitialized -= Window_SourceInitialized;
            Contract.Assume(IsSourceInitialized);

            if (dirty)
                ApplyChanges();
        }

        [ContractInvariantMethod]
        private void CloseButtonHiderInvariant()
        {
            Contract.Invariant(interopHelper != null);
            Contract.Invariant(Window != null);
        }
    }
}
