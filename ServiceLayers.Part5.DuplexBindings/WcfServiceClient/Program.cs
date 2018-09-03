using System;
using System.ServiceModel;
using WcfService;

namespace WcfServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var channelFactory = new ChannelFactory<IServiceContract>("CallbackServiceClient"))
            {
                var serviceClient = channelFactory.CreateChannel();

                serviceClient.RegisterWithServer();

                while (true)
                {
                    Console.WriteLine("Enter a message to send to the server or type 'exit' to end:");
                    var message = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(message))
                        continue;
                    if (message.ToLower() == "exit")
                        break;
                    serviceClient.SendMessageToServer(message);
                }
            }
        }
    }
}
