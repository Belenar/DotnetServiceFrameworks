﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfServiceClient.PersonServiceProxy {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PersonServiceProxy.IPersonService")]
    public interface IPersonService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetConsultants", ReplyAction="http://tempuri.org/IPersonService/GetConsultantsResponse")]
        System.Collections.Generic.List<WcfService.Person> GetConsultants();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetConsultants", ReplyAction="http://tempuri.org/IPersonService/GetConsultantsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WcfService.Person>> GetConsultantsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPersonServiceChannel : WcfServiceClient.PersonServiceProxy.IPersonService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PersonServiceClient : System.ServiceModel.ClientBase<WcfServiceClient.PersonServiceProxy.IPersonService>, WcfServiceClient.PersonServiceProxy.IPersonService {
        
        public PersonServiceClient() {
        }
        
        public PersonServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PersonServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<WcfService.Person> GetConsultants() {
            return base.Channel.GetConsultants();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WcfService.Person>> GetConsultantsAsync() {
            return base.Channel.GetConsultantsAsync();
        }
    }
}
