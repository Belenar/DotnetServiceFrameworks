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
            var mediumConsultantsRequest = new GetConsultantsRequest { MinimumAge = 25, MaximumAge = 30 };

            requestDispatcher.Add("youngOnes", youngConsultantsRequest);
            requestDispatcher.Add("mediumOnes", mediumConsultantsRequest);

            var youngResponse = requestDispatcher.Get<GetConsultantsResponse>("youngOnes");
            var mediumResponse = requestDispatcher.Get<GetConsultantsResponse>("mediumOnes");

            Console.WriteLine("Young Consultants:");
            foreach (var consultant in youngResponse.Consultants)
            {
                Console.WriteLine($"{consultant.FirstName} {consultant.LastName}");
            }

            Console.WriteLine("Medium Consultants:");
            foreach (var consultant in mediumResponse.Consultants)
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
