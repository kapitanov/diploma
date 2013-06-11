using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal sealed class WcfEventsMonitor
    {
        public WcfEventsMonitor(string serviceName)
        {
            this.serviceName = serviceName;
            onChannelOpeningMessage = string.Format("{0}::Channel.Opening", serviceName);
            onChannelOpenedMessage = string.Format("{0}::Channel.Opened", serviceName);
            onOperationCompletedMessage = string.Format("{0}::OperationCompleted", serviceName);
            onChannelFaultedMessage = string.Format("{0}::Channel.Faulted", serviceName);
            onChannelClosingMessage = string.Format("{0}::Channel.Closing", serviceName);
            onChannelClosedMessage = string.Format("{0}::Channel.Closed", serviceName);
        }

        public WcfEventsMonitor BindTo(OperationContext operationContext)
        {
            this.operationContext = operationContext;
            AttachToEvents();
            return this;
        }

        private void AttachToEvents()
        {
            operationContext.OperationCompleted += OnOperationCompleted;
            operationContext.Channel.Closed += OnChannelClosed;
            operationContext.Channel.Closing += OnChannelClosing;
            operationContext.Channel.Faulted += OnChannelFaulted;
            operationContext.Channel.Opened += OnChannelOpened;
            operationContext.Channel.Opening += OnChannelOpening;
        }

        private void DetachFromEvents()
        {
            operationContext.OperationCompleted -= OnOperationCompleted;
            operationContext.Channel.Closed -= OnChannelClosed;
            operationContext.Channel.Closing -= OnChannelClosing;
            operationContext.Channel.Faulted -= OnChannelFaulted;
            operationContext.Channel.Opened -= OnChannelOpened;
            operationContext.Channel.Opening -= OnChannelOpening;
        }

        #region Event handlers

        private void OnChannelOpening(object _, EventArgs e)
        {
            Perfomance.Message(onChannelOpeningMessage);
        }

        private void OnChannelOpened(object _, EventArgs e)
        {
            Perfomance.Message(onChannelOpenedMessage);
        }

        private void OnOperationCompleted(object _, EventArgs e)
        {
            Perfomance.Message(onOperationCompletedMessage);
        }

        private void OnChannelFaulted(object _, EventArgs e)
        {
            Perfomance.Message(onChannelFaultedMessage);
        }

        private void OnChannelClosing(object _, EventArgs e)
        {
            Perfomance.Message(onChannelClosingMessage);
        }

        private void OnChannelClosed(object _, EventArgs e)
        {
            Perfomance.Message(onChannelClosedMessage);
            DetachFromEvents();
        }

        #endregion

        private OperationContext operationContext;
        private readonly string serviceName;
        private readonly string onChannelOpeningMessage;
        private readonly string onChannelOpenedMessage;
        private readonly string onOperationCompletedMessage;
        private readonly string onChannelFaultedMessage;
        private readonly string onChannelClosingMessage;
        private readonly string onChannelClosedMessage;
    }
}
