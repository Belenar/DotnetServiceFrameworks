using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    interface ICallbackContract
    {
        [OperationContract]
        void SendMessageToClient(string message);
    }
}
