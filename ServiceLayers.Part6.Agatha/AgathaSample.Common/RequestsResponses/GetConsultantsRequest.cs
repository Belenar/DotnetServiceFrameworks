using Agatha.Common;

namespace AgathaSample.Common.RequestsResponses
{
    public class GetConsultantsRequest : Request
    {
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }
    }
}
