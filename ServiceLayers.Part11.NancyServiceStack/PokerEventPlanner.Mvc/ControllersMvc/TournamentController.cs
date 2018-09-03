using System.Linq;
using System.Web.Mvc;
using PokerEventPlanner.Data;

namespace PokerEventPlanner.Mvc.ControllersMvc
{
    public class TournamentController : Controller
    {
        private readonly PokerContext _context = PokerContext.Instance;

        public ActionResult Item(int id)
        {
            var tournament = _context.Tournaments.Single(x => x.Id == id);

            return View(tournament);
        }
    }
}
