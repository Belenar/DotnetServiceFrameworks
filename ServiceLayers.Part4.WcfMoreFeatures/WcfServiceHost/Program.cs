using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfService;

namespace WcfServiceHost
{
    class Program
    {
        static void Main()
        {
            using (var serviceHost = new ServiceHost(typeof(PersonService)))
            {
                try
                {
                    serviceHost.Open();

                    Console.WriteLine("PersonService is ready.");
                    Console.WriteLine("Press <ENTER> to terminate the service");
                    Console.ReadLine();

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
