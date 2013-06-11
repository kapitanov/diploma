using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.MasterNode.Tests.DataExchangeTest
{
    public sealed class Experiment
    {
        public Experiment()
        {
            Name = string.Empty;
        }

        public string Name { get; set; }
        public TimeSpan SerializeTime { get; set; }
        public TimeSpan DeserializeTime { get; set; }

        public Experiment Named(string name)
        {
            Name = name;
            return this;
        }


        public Experiment Divided(double dividor)
        {
            SerializeTime = TimeSpan.FromMilliseconds(SerializeTime.TotalMilliseconds / dividor);
            DeserializeTime = TimeSpan.FromMilliseconds(DeserializeTime.TotalMilliseconds / dividor);
            return this;
        }
    }
}
