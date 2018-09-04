using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfServiceClient.PersonService;

namespace WcfServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new PersonServiceClient())
            {
                var consultants = client.GetConsultants();

                foreach (var consultant in consultants)
                {
                    Console.WriteLine($"{consultant.FirstName} {consultant.LastName}");
                }

                Console.ReadKey();
            }
        }
    }
}
