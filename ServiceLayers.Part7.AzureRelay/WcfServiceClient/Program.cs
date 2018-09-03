using System;
using System.ServiceModel;
using WcfService;

namespace WcfServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var channelFactory = new ChannelFactory<IPersonService>("PersonServiceClient"))
            {
                var serviceClient = channelFactory.CreateChannel();

                var consultantList = serviceClient.GetConsultants();

                foreach (var consultant in consultantList)
                {
                    Console.WriteLine($"{consultant.FirstName} {consultant.LastName}");
                }
            }

            Console.ReadLine();


        }
    }
}
