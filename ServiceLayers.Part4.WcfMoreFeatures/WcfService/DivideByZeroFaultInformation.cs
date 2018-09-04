using System;
using System.Runtime.Serialization;

namespace WcfService
{
    [DataContract]
    public class DivideByZeroFaultInformation
    {
        public DivideByZeroFaultInformation(DivideByZeroException exception)
        {
            Message = exception.Message;
            ExtraInfo = exception.Source;
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string ExtraInfo { get; set; }
    }
}
