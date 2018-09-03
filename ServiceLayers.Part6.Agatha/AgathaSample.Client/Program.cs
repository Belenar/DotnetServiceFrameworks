using System;
using Agatha.Common;
using Agatha.Common.InversionOfControl;
using AgathaSample.Common.RequestsResponses;

namespace AgathaSample.Client
{
    class Program
    {   
        static void Main()
        {
            InitializeAgathaClient();

            var requestDispatcher = IoC.Container.Resolve<IRequestDispatcher>();

            var youngConsultantsRequest = new GetConsultantsRequest { MaximumAge = 25 };

            var response = requestDispatcher.Get<GetConsultantsResponse>(youngConsultantsRequest);

            Console.WriteLine("Young Consultants:");
            foreach (var consultant in response.Consultants)
            {
                Console.WriteLine($"{consultant.FirstName} {consultant.LastName}");
            }

            Console.ReadLine();
        }

        private static void InitializeAgathaClient()
        {
            var clientConfig = new ClientConfiguration(typeof(GetConsultantsRequest).Assembly, 
                                                       typeof(Agatha.Ninject.Container));
            
            clientConfig.Initialize();
        }
    }
}
