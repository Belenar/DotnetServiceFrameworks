using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using ErrorHandlingBehaviorLibrary;

namespace WcfService
{
    [ServiceBehavior]
    [ErrorHandlingBehavior]
    public class PersonService : IPersonService
    {
        public PersonService()
        {
            Console.WriteLine("New PersonService created.");
        }

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

        public ICollection<Person> GetConsultants()
        {
            return _consultants;
        }

        public void OneWayCall(int input)
        {
            // Simulate long running operation
            Thread.Sleep(5000);
        }
 
        public int Divide(int number, int divider)
        {
            if(divider == 0)
                throw new DivideByZeroException("Wie deelt door nul is een snul!");

            return number / divider;
        }
    }
}