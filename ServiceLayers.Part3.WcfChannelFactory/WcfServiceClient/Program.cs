using System;
using WcfServiceClient.PersonServiceProxy;

namespace WcfServiceClient
{
    class Program
    {
        static void Main()
        {
            using (var client = new PersonServiceClient())
            {
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
