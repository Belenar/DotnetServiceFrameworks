using System;
using System.Collections.Generic;
using System.ServiceModel;
using ErrorHandlingBehaviorLibrary;

namespace WcfService
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        ICollection<Person> GetConsultants();

        [OperationContract(IsOneWay = true)]
        void OneWayCall(int input);

        [OperationContract]
        [FaultContract(typeof(DivideByZeroFaultInformation))]
        [MapExceptionToFault(typeof(DivideByZeroException), typeof(DivideByZeroFaultInformation))]
        int Divide(int number, int divider);
    }
}