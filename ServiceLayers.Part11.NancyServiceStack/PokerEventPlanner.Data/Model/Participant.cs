using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PokerEventPlanner.Data.Model
{
    public class Participant
    {
        public int Id { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Tournament Tournament { get; set; }
        
        public Player Player { get; set; }
        public int? Postition { get; set; }
    }
}
