using System;
using System.ComponentModel;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    public class Pair : INotifyPropertyChanged
    {
        public Pair(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public Pair()
            : this(string.Empty, string.Empty)
        { }

        public string Key
        {
            get { return key; }
            set
            {
                lock (key)
                {
                    if (key != value)
                        InvokePropertyChanged("Key");
                    key = value;
                }
            }
        }

        public string Value
        {
            get { return value; }
            set
            {
                lock (this.value)
                {
                    if (this.value != value)
                        InvokePropertyChanged("Value");
                    this.value = value;
                }
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private string key;

        private string value;
    }
}
