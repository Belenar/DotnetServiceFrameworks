using System;
using System.ServiceModel;
using WcfService;

namespace WcfServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup the service host here
            var serviceInstance = new CallbackService();

            using (var serviceHost = new ServiceHost(serviceInstance))
            {
                try
                {
                    serviceHost.Open();

                    Console.WriteLine("CallbackService is ready.");
                    while (true)
                    {
                        Console.WriteLine("Enter a message to send to the clients or type 'exit' to end:");
                        var message = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(message))
                            continue;
                        if (message.ToLower() == "exit")
                            break;
                        serviceInstance.SendMessageToClients(message);
                    }

                    serviceHost.Close();
                }
                catch (TimeoutException timeProblem)
                {
                    Console.WriteLine(timeProblem.Message);
                    Console.ReadLine();
                }
                catch (CommunicationException commProblem)
                {
                    Console.WriteLine(commProblem.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
