using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using Agatha.ServiceLayer;
using AgathaSample.Common;
using AgathaSample.Common.RequestsResponses;

namespace AgathaSample.ServiceLayer.Handlers
{
    public class GetConsultantsHandler : RequestHandler<GetConsultantsRequest,GetConsultantsResponse>
    {
        private readonly List<Person> _consultants = new List<Person>
            {
                new Person {FirstName = "Andy", LastName = "Smet", BirthDay = new DateTime(1982, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Bart", LastName = "Gevaert", BirthDay = new DateTime(1994, 1, 1), TrackParticipant = true},
                new Person {FirstName = "Ben", LastName = "Boyen", BirthDay = new DateTime(1984, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Bram", LastName = "Devuyst", BirthDay = new DateTime(1992, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Francis", LastName = "Didden", BirthDay = new DateTime(1992, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Frederick", LastName = "Eskens", BirthDay = new DateTime(1993, 1, 1), TrackParticipant = true},
                new Person {FirstName = "Hannes", LastName = "Lowette", BirthDay = new DateTime(1982, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Jasper", LastName = "Vermorgen", BirthDay = new DateTime(1987, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Jensen", LastName = "Somers", BirthDay = new DateTime(1985, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Jente", LastName = "Vets", BirthDay = new DateTime(1994, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Kenny", LastName = "Laevaert", BirthDay = new DateTime(1991, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Kris", LastName = "Bulté", BirthDay = new DateTime(1986, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Kris", LastName = "Verdonck", BirthDay = new DateTime(1977, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Mathias", LastName = "Vermeiren", BirthDay = new DateTime(1994, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Matthias", LastName = "Heylen", BirthDay = new DateTime(1995, 1, 1), TrackParticipant = true},
                new Person {FirstName = "Maxime", LastName = "Jonckheere", BirthDay = new DateTime(1990, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Reinhard", LastName = "Vanreusel", BirthDay = new DateTime(1983, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Steven", LastName = "Roeland", BirthDay = new DateTime(1985, 1, 1), TrackParticipant = false},
                new Person {FirstName = "Stijn", LastName = "Schrauwen", BirthDay = new DateTime(1995, 1, 1), TrackParticipant = true},
                new Person {FirstName = "Tom", LastName = "De Wilde", BirthDay = new DateTime(1985, 1, 1), TrackParticipant = false}
            };

        public override Response Handle(GetConsultantsRequest request)
        {
            var response = CreateTypedResponse();

            IEnumerable<Person> resultSet = _consultants;

            if (request.MinimumAge != null)
            {
                resultSet = resultSet.Where(x => x.BirthDay <= DateTime.Today.AddYears((int)(-request.MinimumAge)));
            }
            if (request.MaximumAge != null)
            {
                resultSet = resultSet.Where(x => x.BirthDay > DateTime.Today.AddYears((int)(-request.MaximumAge)));
            }

            response.Consultants = resultSet.ToList();
            
            return response;
        }
    }
}
