using System;

namespace WcfService
{
    public class CallbackHandler : ICallbackContract
    {
        public void SendMessageToClient(string message)
        {
            Console.WriteLine("Message received from server:");
            Console.WriteLine(message);
        }
    }
}
