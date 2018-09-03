using System;
using System.Collections.Generic;

namespace PokerEventPlanner.Data.Model
{
    public class Tournament
    {
        public Tournament()
        {
            Participants = new List<Participant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public decimal BuyIn { get; set; }

        public List<Participant> Participants { get; set; } 
    }
}
