using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfService;

namespace WcfServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/PersonService/");

            using (var host = new ServiceHost(typeof(PersonService), baseAddress))
            {
                host.AddServiceEndpoint(typeof(IPersonService), new WSHttpBinding(), "");

                var metaDataBehavior = new ServiceMetadataBehavior {HttpGetEnabled = true};

                host.Description.Behaviors.Add(metaDataBehavior);

                try
                {
                    host.Open();
                    Console.WriteLine("PersonService is ready");
                    Console.WriteLine("Press <ENTER> to exit.");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ReadLine();
                }
            }
        }
    }
}
