using System;
using System.ServiceModel;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class CallbackService : IServiceContract
    {
        public void RegisterWithServer()
        {
            // Register the callback channel here
        }

        public void SendMessageToServer(string message)
        {
            Console.WriteLine("Message received from client:");
            Console.WriteLine(message);
        }

        public void SendMessageToClients(string message)
        {
            // Message the clients here
        }
    }
}
