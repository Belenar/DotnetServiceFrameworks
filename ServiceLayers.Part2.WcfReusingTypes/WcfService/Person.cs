using System;
using System.Runtime.Serialization;

namespace WcfService
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime BirthDay { get; set; }
        [DataMember]
        public bool TrackParticipant { get; set; }
        [DataMember]
        public bool HasHisOwnEmoji { get; set; }
    }
}
