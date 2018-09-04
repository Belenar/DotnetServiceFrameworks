using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebApiSample.Data.Model
{
    public class Participant
    {
        public int Id { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Tournament Tournament { get; set; }

        public Player Player { get; set; }
        public int? Position { get; set; }
    }
}