using System;
using System.ComponentModel;
using System.Windows;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    public abstract class Stage : INotifyPropertyChanged
    {
        protected Stage(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public StageState State
        {
            get { return state; }
            set
            {
                if (state != value)
                    InvokePropertyChanged("State");
                state = value;
            }
        }

        public Window Owner { get; set; }

        public abstract void Execute(
            JobDefinition job, 
            Action<int, int> notifyProgress,
            Action<string> updateStatus,
            IRepository repositoryService, 
            IJobManager jobManager);

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private StageState state = StageState.Pending;
    }
}
