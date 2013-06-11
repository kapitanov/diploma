using System;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.Util
{
    public class DisposableWrapper : IDisposable
    {
        public DisposableWrapper(Action onDisposeAction)
        {
            Contract.Requires<ArgumentNullException>(onDisposeAction != null);
            this.onDisposeAction = onDisposeAction;
        }

        public void Dispose()
        {
            onDisposeAction();
        }

        private readonly Action onDisposeAction;
    }
}
