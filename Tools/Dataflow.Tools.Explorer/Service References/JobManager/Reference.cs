﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.21006.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AISTek.DataFlow.Tools.Explorer.JobManager {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Job", Namespace="http://aistek.dataflow/services/scheduler/")]
    [System.SerializableAttribute()]
    public partial class Job : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid IdField;
        
        private string NameField;
        
        private AISTek.DataFlow.Tools.Explorer.JobManager.Task[] TasksField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public AISTek.DataFlow.Tools.Explorer.JobManager.Task[] Tasks {
            get {
                return this.TasksField;
            }
            set {
                if ((object.ReferenceEquals(this.TasksField, value) != true)) {
                    this.TasksField = value;
                    this.RaisePropertyChanged("Tasks");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Task", Namespace="http://aistek.dataflow/services/scheduler/")]
    [System.SerializableAttribute()]
    public partial class Task : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private AISTek.DataFlow.Tools.Explorer.JobManager.EntryPoint EntryPointField;
        
        private System.Guid IdField;
        
        private AISTek.DataFlow.Tools.Explorer.JobManager.FileLink[] InputFilesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AISTek.DataFlow.Tools.Explorer.JobManager.Task[] InputsField;
        
        private string NameField;
        
        private AISTek.DataFlow.Tools.Explorer.JobManager.FileLink[] OutputFilesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AISTek.DataFlow.Tools.Explorer.JobManager.Task[] OutputsField;
        
        private System.Collections.Generic.Dictionary<string, string> ParametersField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public AISTek.DataFlow.Tools.Explorer.JobManager.EntryPoint EntryPoint {
            get {
                return this.EntryPointField;
            }
            set {
                if ((object.ReferenceEquals(this.EntryPointField, value) != true)) {
                    this.EntryPointField = value;
                    this.RaisePropertyChanged("EntryPoint");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public AISTek.DataFlow.Tools.Explorer.JobManager.FileLink[] InputFiles {
            get {
                return this.InputFilesField;
            }
            set {
                if ((object.ReferenceEquals(this.InputFilesField, value) != true)) {
                    this.InputFilesField = value;
                    this.RaisePropertyChanged("InputFiles");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AISTek.DataFlow.Tools.Explorer.JobManager.Task[] Inputs {
            get {
                return this.InputsField;
            }
            set {
                if ((object.ReferenceEquals(this.InputsField, value) != true)) {
                    this.InputsField = value;
                    this.RaisePropertyChanged("Inputs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public AISTek.DataFlow.Tools.Explorer.JobManager.FileLink[] OutputFiles {
            get {
                return this.OutputFilesField;
            }
            set {
                if ((object.ReferenceEquals(this.OutputFilesField, value) != true)) {
                    this.OutputFilesField = value;
                    this.RaisePropertyChanged("OutputFiles");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AISTek.DataFlow.Tools.Explorer.JobManager.Task[] Outputs {
            get {
                return this.OutputsField;
            }
            set {
                if ((object.ReferenceEquals(this.OutputsField, value) != true)) {
                    this.OutputsField = value;
                    this.RaisePropertyChanged("Outputs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Collections.Generic.Dictionary<string, string> Parameters {
            get {
                return this.ParametersField;
            }
            set {
                if ((object.ReferenceEquals(this.ParametersField, value) != true)) {
                    this.ParametersField = value;
                    this.RaisePropertyChanged("Parameters");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntryPoint", Namespace="http://aistek.dataflow/services/scheduler/")]
    [System.SerializableAttribute()]
    public partial class EntryPoint : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid AssemblyIdField;
        
        private System.Guid[] DependentAssemblyIdsField;
        
        private string QualifiedClassNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid AssemblyId {
            get {
                return this.AssemblyIdField;
            }
            set {
                if ((this.AssemblyIdField.Equals(value) != true)) {
                    this.AssemblyIdField = value;
                    this.RaisePropertyChanged("AssemblyId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid[] DependentAssemblyIds {
            get {
                return this.DependentAssemblyIdsField;
            }
            set {
                if ((object.ReferenceEquals(this.DependentAssemblyIdsField, value) != true)) {
                    this.DependentAssemblyIdsField = value;
                    this.RaisePropertyChanged("DependentAssemblyIds");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string QualifiedClassName {
            get {
                return this.QualifiedClassNameField;
            }
            set {
                if ((object.ReferenceEquals(this.QualifiedClassNameField, value) != true)) {
                    this.QualifiedClassNameField = value;
                    this.RaisePropertyChanged("QualifiedClassName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FileLink", Namespace="http://aistek.dataflow/services/scheduler/")]
    [System.SerializableAttribute()]
    public partial class FileLink : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Guid IdField;
        
        private System.Collections.Generic.Dictionary<string, string> MetadataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Collections.Generic.Dictionary<string, string> Metadata {
            get {
                return this.MetadataField;
            }
            set {
                if ((object.ReferenceEquals(this.MetadataField, value) != true)) {
                    this.MetadataField = value;
                    this.RaisePropertyChanged("Metadata");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://aistek.dataflow/services/scheduler/", ConfigurationName="JobManager.IJobManager", CallbackContract=typeof(AISTek.DataFlow.Tools.Explorer.JobManager.IJobManagerCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IJobManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://aistek.dataflow/services/scheduler/IJobManager/CreateJob", ReplyAction="http://aistek.dataflow/services/scheduler/IJobManager/CreateJobResponse")]
        AISTek.DataFlow.Tools.Explorer.JobManager.Job CreateJob(string name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/OpenJob")]
        void OpenJob(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://aistek.dataflow/services/scheduler/IJobManager/CreateTask", ReplyAction="http://aistek.dataflow/services/scheduler/IJobManager/CreateTaskResponse")]
        System.Guid CreateTask(AISTek.DataFlow.Tools.Explorer.JobManager.Task task);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/UpdateTask")]
        void UpdateTask(System.Guid id, AISTek.DataFlow.Tools.Explorer.JobManager.Task task);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/RemoveTask")]
        void RemoveTask(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/StartJob")]
        void StartJob();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/CancelJob")]
        void CancelJob();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/RestartJob")]
        void RestartJob();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/DeleteJob")]
        void DeleteJob();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://aistek.dataflow/services/scheduler/IJobManager/FindJobs", ReplyAction="http://aistek.dataflow/services/scheduler/IJobManager/FindJobsResponse")]
        AISTek.DataFlow.Tools.Explorer.JobManager.Job[] FindJobs(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://aistek.dataflow/services/scheduler/IJobManager/GetAllJobs", ReplyAction="http://aistek.dataflow/services/scheduler/IJobManager/GetAllJobsResponse")]
        AISTek.DataFlow.Tools.Explorer.JobManager.Job[] GetAllJobs();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://aistek.dataflow/services/scheduler/IJobManager/get_CurrentJob", ReplyAction="http://aistek.dataflow/services/scheduler/IJobManager/get_CurrentJobResponse")]
        AISTek.DataFlow.Tools.Explorer.JobManager.Job get_CurrentJob();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJobManagerCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/JobCompleted")]
        void JobCompleted(AISTek.DataFlow.Tools.Explorer.JobManager.Job job);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://aistek.dataflow/services/scheduler/IJobManager/JobFailed")]
        void JobFailed(AISTek.DataFlow.Tools.Explorer.JobManager.Job job);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJobManagerChannel : AISTek.DataFlow.Tools.Explorer.JobManager.IJobManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JobManagerClient : System.ServiceModel.DuplexClientBase<AISTek.DataFlow.Tools.Explorer.JobManager.IJobManager>, AISTek.DataFlow.Tools.Explorer.JobManager.IJobManager {
        
        public JobManagerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public JobManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public JobManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JobManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JobManagerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public AISTek.DataFlow.Tools.Explorer.JobManager.Job CreateJob(string name) {
            return base.Channel.CreateJob(name);
        }
        
        public void OpenJob(System.Guid id) {
            base.Channel.OpenJob(id);
        }
        
        public System.Guid CreateTask(AISTek.DataFlow.Tools.Explorer.JobManager.Task task) {
            return base.Channel.CreateTask(task);
        }
        
        public void UpdateTask(System.Guid id, AISTek.DataFlow.Tools.Explorer.JobManager.Task task) {
            base.Channel.UpdateTask(id, task);
        }
        
        public void RemoveTask(System.Guid id) {
            base.Channel.RemoveTask(id);
        }
        
        public void StartJob() {
            base.Channel.StartJob();
        }
        
        public void CancelJob() {
            base.Channel.CancelJob();
        }
        
        public void RestartJob() {
            base.Channel.RestartJob();
        }
        
        public void DeleteJob() {
            base.Channel.DeleteJob();
        }
        
        public AISTek.DataFlow.Tools.Explorer.JobManager.Job[] FindJobs(string name) {
            return base.Channel.FindJobs(name);
        }
        
        public AISTek.DataFlow.Tools.Explorer.JobManager.Job[] GetAllJobs() {
            return base.Channel.GetAllJobs();
        }
        
        public AISTek.DataFlow.Tools.Explorer.JobManager.Job get_CurrentJob() {
            return base.Channel.get_CurrentJob();
        }
    }
}
