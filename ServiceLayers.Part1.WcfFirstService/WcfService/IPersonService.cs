using System.Collections.Generic;

namespace WcfService
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        ICollection<Person> GetConsultants();
    }
}