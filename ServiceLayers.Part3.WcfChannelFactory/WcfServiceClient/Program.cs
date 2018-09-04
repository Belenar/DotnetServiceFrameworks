using System;
using System.ServiceModel;
using WcfService;

namespace WcfServiceClient
{
    class Program
    {
        static void Main()
        {
            using (var factory = new ChannelFactory<IPersonService>("PersonServiceClient"))
            {
                var client = factory.CreateChannel();

                var consultantList = client.GetConsultants();

                foreach (var consultant in consultantList)
                {
                    Console.WriteLine($"{consultant.FirstName} {consultant.LastName}");
                }

                Console.ReadLine();
            }
        }
    }
}
