using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace AISTek.DataFlow.PresentationExtensions
{
    public static class AsynchExtensions
    {
        public static void Synch<T>(this T window, Action<T> action)
            where T : Control 
        {
            Contract.Requires(window!=null);
            Contract.Requires(action!=null);

            window.Dispatcher.Invoke(action, window);
        }

        public static void Synch<T>(this T window, Action action)
            where T : Control
        {
            Contract.Requires(window != null);
            Contract.Requires(action != null);

            window.Dispatcher.Invoke(action);
        }

        public static void Asynch(this Window window, Action longTermAction, Action showWaitScreen = null, Action hideWaitScreen = null, Action synchAction = null)
        {
            Contract.Requires(window != null);
            Contract.Requires(longTermAction != null);

            if (showWaitScreen == null)
                showWaitScreen = () => { };

            if (hideWaitScreen == null)
                hideWaitScreen = () => { };

            window.Synch(showWaitScreen);
            ThreadPool.QueueUserWorkItem(del =>
            {
                try
                {
                    longTermAction();
                }
                catch
                {
                    window.Synch(hideWaitScreen);
                    throw;
                }
                window.Synch(_ =>
                {
                    if (synchAction != null)
                    {
                        try
                        {
                            synchAction();
                        }
                        catch
                        {
                            hideWaitScreen();
                            throw;
                        }
                    }
                    hideWaitScreen();
                });
            });
        }

       
        public static void Asynch(this UserControl control, Action longTermAction, Action showWaitScreen = null, Action hideWaitScreen = null, Action synchAction = null)
        {
            Contract.Requires(control != null);
            Contract.Requires(longTermAction != null);

            if (showWaitScreen == null)
                showWaitScreen = () => { };

            if (hideWaitScreen == null)
                hideWaitScreen = () => { };

            control.Synch(showWaitScreen);
            ThreadPool.QueueUserWorkItem(del =>
            {
                try
                {
                    longTermAction();
                }
                catch
                {
                    control.Synch(hideWaitScreen);
                    throw;
                }
                control.Synch(() =>
                {
                    if (synchAction != null)
                    {
                        try
                        {
                            synchAction();
                        }
                        catch
                        {
                            hideWaitScreen();
                            throw;
                        }
                    }
                    hideWaitScreen();
                });
            });
        }
    }
}
