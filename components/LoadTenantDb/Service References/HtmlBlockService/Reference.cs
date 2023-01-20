﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AddHtmlBlockCommand", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Administration.BAS.TenantConfi" +
        "guration.Contracts.Commands")]
    [System.SerializableAttribute()]
    public partial class AddHtmlBlockCommand : HP.HSP.UA3.Core.BAS.CQRS.Base.Command, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlock HtmlBlockField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlock HtmlBlock {
            get {
                return this.HtmlBlockField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBlockField, value) != true)) {
                    this.HtmlBlockField = value;
                    this.RaisePropertyChanged("HtmlBlock");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="HtmlBlock", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Administration.BAS.TenantConfi" +
        "guration.Contracts.Domain")]
    [System.SerializableAttribute()]
    public partial class HtmlBlock : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlBlockIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockLanguage[] HtmlBlockLanguagesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsActiveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LastModifiedTSField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OperatorIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TenantModuleIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ContentId {
            get {
                return this.ContentIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentIdField, value) != true)) {
                    this.ContentIdField = value;
                    this.RaisePropertyChanged("ContentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HtmlBlockId {
            get {
                return this.HtmlBlockIdField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBlockIdField, value) != true)) {
                    this.HtmlBlockIdField = value;
                    this.RaisePropertyChanged("HtmlBlockId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockLanguage[] HtmlBlockLanguages {
            get {
                return this.HtmlBlockLanguagesField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBlockLanguagesField, value) != true)) {
                    this.HtmlBlockLanguagesField = value;
                    this.RaisePropertyChanged("HtmlBlockLanguages");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsActive {
            get {
                return this.IsActiveField;
            }
            set {
                if ((this.IsActiveField.Equals(value) != true)) {
                    this.IsActiveField = value;
                    this.RaisePropertyChanged("IsActive");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastModifiedTS {
            get {
                return this.LastModifiedTSField;
            }
            set {
                if ((this.LastModifiedTSField.Equals(value) != true)) {
                    this.LastModifiedTSField = value;
                    this.RaisePropertyChanged("LastModifiedTS");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperatorID {
            get {
                return this.OperatorIDField;
            }
            set {
                if ((object.ReferenceEquals(this.OperatorIDField, value) != true)) {
                    this.OperatorIDField = value;
                    this.RaisePropertyChanged("OperatorID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TenantModuleId {
            get {
                return this.TenantModuleIdField;
            }
            set {
                if ((object.ReferenceEquals(this.TenantModuleIdField, value) != true)) {
                    this.TenantModuleIdField = value;
                    this.RaisePropertyChanged("TenantModuleId");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UpdateHtmlBlockCommand", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Administration.BAS.TenantConfi" +
        "guration.Contracts.Commands")]
    [System.SerializableAttribute()]
    public partial class UpdateHtmlBlockCommand : HP.HSP.UA3.Core.BAS.CQRS.Base.Command, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlock HtmlBlockField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlock HtmlBlock {
            get {
                return this.HtmlBlockField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBlockField, value) != true)) {
                    this.HtmlBlockField = value;
                    this.RaisePropertyChanged("HtmlBlock");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="HtmlBlockLanguage", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Administration.BAS.TenantConfi" +
        "guration.Contracts.Domain")]
    [System.SerializableAttribute()]
    public partial class HtmlBlockLanguage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlBlockIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HtmlBlockLanguageUpdatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsActiveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LastModifiedTSField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocaleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OperatorIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Html {
            get {
                return this.HtmlField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlField, value) != true)) {
                    this.HtmlField = value;
                    this.RaisePropertyChanged("Html");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HtmlBlockId {
            get {
                return this.HtmlBlockIdField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBlockIdField, value) != true)) {
                    this.HtmlBlockIdField = value;
                    this.RaisePropertyChanged("HtmlBlockId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HtmlBlockLanguageUpdated {
            get {
                return this.HtmlBlockLanguageUpdatedField;
            }
            set {
                if ((this.HtmlBlockLanguageUpdatedField.Equals(value) != true)) {
                    this.HtmlBlockLanguageUpdatedField = value;
                    this.RaisePropertyChanged("HtmlBlockLanguageUpdated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsActive {
            get {
                return this.IsActiveField;
            }
            set {
                if ((this.IsActiveField.Equals(value) != true)) {
                    this.IsActiveField = value;
                    this.RaisePropertyChanged("IsActive");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastModifiedTS {
            get {
                return this.LastModifiedTSField;
            }
            set {
                if ((this.LastModifiedTSField.Equals(value) != true)) {
                    this.LastModifiedTSField = value;
                    this.RaisePropertyChanged("LastModifiedTS");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Locale {
            get {
                return this.LocaleField;
            }
            set {
                if ((object.ReferenceEquals(this.LocaleField, value) != true)) {
                    this.LocaleField = value;
                    this.RaisePropertyChanged("Locale");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperatorId {
            get {
                return this.OperatorIdField;
            }
            set {
                if ((object.ReferenceEquals(this.OperatorIdField, value) != true)) {
                    this.OperatorIdField = value;
                    this.RaisePropertyChanged("OperatorId");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TenantConfigurationEvents", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Administration.BAS.TenantConfi" +
        "guration.Contracts.Events")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockUpdated))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockAdded))]
    public partial class TenantConfigurationEvents : HP.HSP.UA3.Core.BAS.CQRS.Base.Event, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    [System.Runtime.Serialization.DataContractAttribute(Name="HtmlBlockUpdated", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Administration.BAS.TenantConfi" +
        "guration.Contracts.Events")]
    [System.SerializableAttribute()]
    public partial class HtmlBlockUpdated : HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.TenantConfigurationEvents {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlBlockIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HtmlBlockId {
            get {
                return this.HtmlBlockIdField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBlockIdField, value) != true)) {
                    this.HtmlBlockIdField = value;
                    this.RaisePropertyChanged("HtmlBlockId");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="HtmlBlockAdded", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Administration.BAS.TenantConfi" +
        "guration.Contracts.Events")]
    [System.SerializableAttribute()]
    public partial class HtmlBlockAdded : HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.TenantConfigurationEvents {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlBlockIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HtmlBlockId {
            get {
                return this.HtmlBlockIdField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBlockIdField, value) != true)) {
                    this.HtmlBlockIdField = value;
                    this.RaisePropertyChanged("HtmlBlockId");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HtmlBlockService.IHtmlBlockService")]
    public interface IHtmlBlockService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHtmlBlockService/AddHtmlBlock", ReplyAction="http://tempuri.org/IHtmlBlockService/AddHtmlBlockResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(HP.HSP.UA3.Core.BAS.CQRS.Base.ServiceException), Action="http://tempuri.org/IHtmlBlockService/AddHtmlBlockServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Core.BAS.CQRS.Base")]
        HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockAdded AddHtmlBlock(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.AddHtmlBlockCommand command);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHtmlBlockService/AddHtmlBlock", ReplyAction="http://tempuri.org/IHtmlBlockService/AddHtmlBlockResponse")]
        System.Threading.Tasks.Task<HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockAdded> AddHtmlBlockAsync(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.AddHtmlBlockCommand command);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHtmlBlockService/UpdateHtmlBlock", ReplyAction="http://tempuri.org/IHtmlBlockService/UpdateHtmlBlockResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(HP.HSP.UA3.Core.BAS.CQRS.Base.ServiceException), Action="http://tempuri.org/IHtmlBlockService/UpdateHtmlBlockServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/HP.HSP.UA3.Core.BAS.CQRS.Base")]
        HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockUpdated UpdateHtmlBlock(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.UpdateHtmlBlockCommand command);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHtmlBlockService/UpdateHtmlBlock", ReplyAction="http://tempuri.org/IHtmlBlockService/UpdateHtmlBlockResponse")]
        System.Threading.Tasks.Task<HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockUpdated> UpdateHtmlBlockAsync(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.UpdateHtmlBlockCommand command);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHtmlBlockServiceChannel : HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.IHtmlBlockService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HtmlBlockServiceClient : System.ServiceModel.ClientBase<HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.IHtmlBlockService>, HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.IHtmlBlockService {
        
        public HtmlBlockServiceClient() {
        }
        
        public HtmlBlockServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HtmlBlockServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HtmlBlockServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HtmlBlockServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockAdded AddHtmlBlock(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.AddHtmlBlockCommand command) {
            return base.Channel.AddHtmlBlock(command);
        }
        
        public System.Threading.Tasks.Task<HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockAdded> AddHtmlBlockAsync(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.AddHtmlBlockCommand command) {
            return base.Channel.AddHtmlBlockAsync(command);
        }
        
        public HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockUpdated UpdateHtmlBlock(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.UpdateHtmlBlockCommand command) {
            return base.Channel.UpdateHtmlBlock(command);
        }
        
        public System.Threading.Tasks.Task<HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.HtmlBlockUpdated> UpdateHtmlBlockAsync(HP.HSP.UA3.Utilities.LoadTenantDb.HtmlBlockService.UpdateHtmlBlockCommand command) {
            return base.Channel.UpdateHtmlBlockAsync(command);
        }
    }
}