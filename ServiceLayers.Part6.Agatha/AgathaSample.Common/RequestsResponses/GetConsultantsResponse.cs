using System.Collections.Generic;
using Agatha.Common;

namespace AgathaSample.Common.RequestsResponses
{
    public class GetConsultantsResponse : Response
    {
        public List<Person> Consultants { get; set; }
    }
}
