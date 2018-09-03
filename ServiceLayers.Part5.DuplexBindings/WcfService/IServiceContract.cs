using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IServiceContract
    {
        [OperationContract]
        void RegisterWithServer();

        [OperationContract]
        void SendMessageToServer(string message);
    }
}
