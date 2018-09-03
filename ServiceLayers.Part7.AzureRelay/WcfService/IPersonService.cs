using System.Collections.Generic;
using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        ICollection<Person> GetConsultants();
    }
}