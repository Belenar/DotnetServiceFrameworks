using System;
using System.Collections.Generic;
using System.Linq;
using WebApiSample.Data.Model;

namespace WebApiSample.Data
{
    public class PokerContext
    {
        private static PokerContext _instance;
        public static PokerContext Instance => _instance ?? (_instance = new PokerContext());

        public List<Player> Players { get; }
        public List<Tournament> Tournaments { get; }
        public List<Participant> Participants { get; }

        public PokerContext()
        {
            var playerAs =   new Player { Id = 1, FirstName = "Andy", LastName = "Smet", Birthday = new DateTime(1982, 1, 1) };
            var playerBb =   new Player { Id = 2, FirstName = "Ben", LastName = "Boyen", Birthday = new DateTime(1984, 1, 1) };
            var playerBd =   new Player { Id = 3, FirstName = "Bram", LastName = "Devuyst", Birthday = new DateTime(1992, 1, 1) };
            var playerFd =   new Player { Id = 4, FirstName = "Francis", LastName = "Didden", Birthday = new DateTime(1992, 1, 1) };
            var playerHl =   new Player { Id = 5, FirstName = "Hannes", LastName = "Lowette", Birthday = new DateTime(1982, 1, 1) };
            var playerJvdv = new Player { Id = 6, FirstName = "Jarrich", LastName = "Van De Voorde", Birthday = new DateTime(1992, 1, 1) };
            var playerJv =   new Player { Id = 7, FirstName = "Jasper", LastName = "Vermorgen", Birthday = new DateTime(1987, 1, 1) };
            var playerJk =   new Player { Id = 8, FirstName = "Jef", LastName = "Keijers", Birthday = new DateTime(1993, 1, 1) };
            var playerJs =   new Player { Id = 9, FirstName = "Jensen", LastName = "Somers", Birthday = new DateTime(1985, 1, 1) };
            var playerJve =  new Player { Id = 10, FirstName = "Jente", LastName = "Vets", Birthday = new DateTime(1994, 1, 1) };
            var playerKl =   new Player { Id = 11, FirstName = "Kenny", LastName = "Laevaert", Birthday = new DateTime(1991, 1, 1) };
            var playerKb =   new Player { Id = 12, FirstName = "Kris", LastName = "Bulté", Birthday = new DateTime(1986, 1, 1) };
            var playerKv =   new Player { Id = 13, FirstName = "Kris", LastName = "Verdonck", Birthday = new DateTime(1900, 1, 1) };
            var playerMv =   new Player { Id = 14, FirstName = "Mathias", LastName = "Vermeiren", Birthday = new DateTime(1994, 1, 1) };
            var playerMj =   new Player { Id = 14, FirstName = "Maxime", LastName = "Jonckheere", Birthday = new DateTime(1990, 1, 1) };
            var playerRv =   new Player { Id = 18, FirstName = "Reinhard", LastName = "Vanreusel", Birthday = new DateTime(1983, 1, 1) };
            var playerSr =   new Player { Id = 19, FirstName = "Steven", LastName = "Roeland", Birthday = new DateTime(1985, 1, 1) };
            var playerTvl =  new Player { Id = 20, FirstName = "Thomas", LastName = "Van Laere", Birthday = new DateTime(1990, 1, 1) };
            var playerTdw =  new Player { Id = 22, FirstName = "Tom", LastName = "De Wilde", Birthday = new DateTime(1985, 1, 1) };

            var tournament1 =  new Tournament { Id = 1, Name =  "Mancave league - Round 1",  Location = "The mancave",   BuyIn = 5.0M,    Date = new DateTime(2014, 1, 18)};
            var tournament2 =  new Tournament { Id = 2, Name =  "Mancave league - Round 2",  Location = "The mancave",   BuyIn = 5.0M,    Date = new DateTime(2014, 2, 15)};
            var tournament3 =  new Tournament { Id = 3, Name =  "Mancave league - Round 3",  Location = "The mancave",   BuyIn = 5.0M,    Date = new DateTime(2014, 3, 15)};
            var tournament4 =  new Tournament { Id = 4, Name =  "Mancave league - Round 4",  Location = "The mancave",   BuyIn = 5.0M,    Date = new DateTime(2014, 4, 19)};
            var tournament5 =  new Tournament { Id = 5, Name =  "Mancave league - Round 5",  Location = "The mancave",   BuyIn = 5.0M,    Date = new DateTime(2014, 5, 17)};
            var tournament6 =  new Tournament { Id = 6, Name =  "Backroom league - Round 1", Location = "The back room", BuyIn = 50.0M,   Date = new DateTime(2014, 6, 21)};
            var tournament7 =  new Tournament { Id = 7, Name =  "Backroom league - Round 2", Location = "The back room", BuyIn = 50.0M,   Date = new DateTime(2014, 7, 19)};
            var tournament8 =  new Tournament { Id = 8, Name =  "Backroom league - Round 3", Location = "The back room", BuyIn = 50.0M,   Date = new DateTime(2014, 8, 16)};
            var tournament9 =  new Tournament { Id = 9, Name =  "Backroom league - Round 4", Location = "The back room", BuyIn = 50.0M,   Date = new DateTime(2014, 9, 20)};
            var tournament10 = new Tournament { Id = 10, Name = "Backroom league - Round 5", Location = "The back room", BuyIn = 50.0M,   Date = new DateTime(2014, 10, 18)};
            var tournament11 = new Tournament { Id = 11, Name = "The big final",             Location = "The mancave",   BuyIn = 1000.0M, Date = new DateTime(2014, 11, 15)};

            tournament1.Participants.AddRange(new[]
            {
                new Participant {Id =  1, Player = playerAs,   Tournament = tournament1, Position = 1},
                new Participant {Id =  2, Player = playerBb,   Tournament = tournament1, Position = 2},
                new Participant {Id =  3, Player = playerBd,   Tournament = tournament1, Position = 3},
                new Participant {Id =  4, Player = playerFd,   Tournament = tournament1, Position = 4},
                new Participant {Id =  5, Player = playerHl,   Tournament = tournament1, Position = 5},
                new Participant {Id =  6, Player = playerJvdv, Tournament = tournament1, Position = 6}
            });

            tournament2.Participants.AddRange(new[]
            {
                new Participant {Id =  7, Player = playerJv,   Tournament = tournament2, Position = 1},
                new Participant {Id =  8, Player = playerJk,   Tournament = tournament2, Position = 2},
                new Participant {Id =  9, Player = playerJs,   Tournament = tournament2, Position = 3},
                new Participant {Id = 10, Player = playerJve,  Tournament = tournament2, Position = 4},
                new Participant {Id = 11, Player = playerKl,   Tournament = tournament2, Position = 5},
                new Participant {Id = 12, Player = playerKb,   Tournament = tournament2, Position = 6}
            });

            tournament3.Participants.AddRange(new[]
            {
                new Participant {Id = 13, Player = playerKv,   Tournament = tournament3, Position = 1},
                new Participant {Id = 14, Player = playerMv,   Tournament = tournament3, Position = 2},
                new Participant {Id = 15, Player = playerMj,   Tournament = tournament3, Position = 3},
                new Participant {Id = 16, Player = playerRv,   Tournament = tournament3, Position = 4},
                new Participant {Id = 17, Player = playerSr,   Tournament = tournament3, Position = 5},
                new Participant {Id = 18, Player = playerTvl,  Tournament = tournament3, Position = 6}
            });

            tournament4.Participants.AddRange(new[]
            {
                new Participant {Id = 19, Player = playerTdw,  Tournament = tournament4, Position = 1},
                new Participant {Id = 20, Player = playerAs,   Tournament = tournament4, Position = 2},
                new Participant {Id = 21, Player = playerBb,   Tournament = tournament4, Position = 3},
                new Participant {Id = 22, Player = playerBd,   Tournament = tournament4, Position = 4},
                new Participant {Id = 23, Player = playerFd,   Tournament = tournament4, Position = 5},
                new Participant {Id = 24, Player = playerHl,   Tournament = tournament4, Position = 6}
            });

            tournament5.Participants.AddRange(new[]
            {
                new Participant {Id = 25, Player = playerJvdv, Tournament = tournament5, Position = 1},
                new Participant {Id = 26, Player = playerJv,   Tournament = tournament5, Position = 2},
                new Participant {Id = 27, Player = playerJk,   Tournament = tournament5, Position = 3},
                new Participant {Id = 28, Player = playerJs,   Tournament = tournament5, Position = 4},
                new Participant {Id = 29, Player = playerJve,  Tournament = tournament5, Position = 5},
                new Participant {Id = 30, Player = playerKl,   Tournament = tournament5, Position = 6}
            });                                                
                                                               
            tournament6.Participants.AddRange(new[]            
            {                                                  
                new Participant {Id = 31, Player = playerKb,   Tournament = tournament6, Position = 1},
                new Participant {Id = 32, Player = playerJs,   Tournament = tournament6, Position = 2},
                new Participant {Id = 33, Player = playerKl,   Tournament = tournament6, Position = 3},
                new Participant {Id = 34, Player = playerKb,   Tournament = tournament6, Position = 4},
                new Participant {Id = 35, Player = playerKv,   Tournament = tournament6, Position = 5},
                new Participant {Id = 36, Player = playerMj,   Tournament = tournament6, Position = 6}
            });                                                
                                                               
            tournament7.Participants.AddRange(new[]            
            {                                                  
                new Participant {Id = 37, Player = playerKb,   Tournament = tournament7, Position = 1},
                new Participant {Id = 38, Player = playerKv,   Tournament = tournament7, Position = 2},
                new Participant {Id = 39, Player = playerMv,   Tournament = tournament7, Position = 3},
                new Participant {Id = 40, Player = playerMj,   Tournament = tournament7, Position = 4},
                new Participant {Id = 41, Player = playerRv,   Tournament = tournament7, Position = 5},
                new Participant {Id = 42, Player = playerSr,   Tournament = tournament7, Position = 6}
            });                                                
                                                               
            tournament8.Participants.AddRange(new[]            
            {                                                  
                new Participant {Id = 43, Player = playerTvl,  Tournament = tournament8, Position = 1},
                new Participant {Id = 44, Player = playerTdw,  Tournament = tournament8, Position = 2},
                new Participant {Id = 45, Player = playerAs,   Tournament = tournament8, Position = 3},
                new Participant {Id = 46, Player = playerBb,   Tournament = tournament8, Position = 4},
                new Participant {Id = 47, Player = playerBd,   Tournament = tournament8, Position = 5},
                new Participant {Id = 48, Player = playerFd,   Tournament = tournament8, Position = 6}
            });
            
            tournament9.Participants.AddRange(new[]            
            {                                                  
                new Participant {Id = 49, Player = playerHl,   Tournament = tournament9 },
                new Participant {Id = 50, Player = playerJvdv, Tournament = tournament9 },
                new Participant {Id = 51, Player = playerJv,   Tournament = tournament9 },
                new Participant {Id = 52, Player = playerJk,   Tournament = tournament9 },
                new Participant {Id = 53, Player = playerJs,   Tournament = tournament9 },
                new Participant {Id = 54, Player = playerJve,  Tournament = tournament9 }
            });

            tournament10.Participants.AddRange(new[]
            {
                new Participant {Id = 55, Player = playerKl,   Tournament = tournament10},
                new Participant {Id = 56, Player = playerKb,   Tournament = tournament10},
                new Participant {Id = 57, Player = playerKv,   Tournament = tournament10},
                new Participant {Id = 58, Player = playerMj,   Tournament = tournament10},
                new Participant {Id = 59, Player = playerJve,  Tournament = tournament10},
                new Participant {Id = 60, Player = playerJk,   Tournament = tournament10}
            });

            tournament11.Participants.AddRange(new[]
            {
                new Participant {Id = 61, Player = playerMv,   Tournament = tournament11},
                new Participant {Id = 62, Player = playerRv,   Tournament = tournament11},
                new Participant {Id = 63, Player = playerSr,   Tournament = tournament11},
                new Participant {Id = 64, Player = playerTvl,  Tournament = tournament11},
                new Participant {Id = 65, Player = playerFd,   Tournament = tournament11},
                new Participant {Id = 66, Player = playerTdw,  Tournament = tournament11}
            });

            var tournaments = new List<Tournament>
            {
                tournament1, tournament2, tournament3, tournament4, tournament5, tournament6, 
                tournament7, tournament8, tournament9, tournament10, tournament11
            };

            var players = new List<Player>
            {
                playerAs, playerBb, playerBd, playerFd, playerHl, playerJvdv,
                playerJv, playerJk, playerJs, playerJve, playerKl, playerKb, 
                playerKv, playerMv, playerMj, playerRv, playerSr, playerTvl,
                playerTdw
            };

            Players = players;
            Participants = tournaments.SelectMany(x => x.Participants).ToList();
            Tournaments = tournaments;
        }  
    }
}
