using System.Linq;
using System.Web.Http;
using PokerEventPlanner.Data;
using PokerEventPlanner.Data.Model;

namespace PokerEventPlanner.Mvc.ControllersApi
{
    public class TournamentController : ApiController
    {
        private readonly PokerContext _context = PokerContext.Instance;

        public Tournament Get(int id)
        {
            var tournament = _context.Tournaments.Single(x => x.Id == id);

            return tournament;
        }
    }
}
