using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSample.Data;
using WebApiSample.Data.Model;

namespace WebApiSample.Web.Controllers
{
    public class TournamentController : ApiController
    {
        private readonly PokerContext _data = PokerContext.Instance;

        // GET: api/Tournament
        public IEnumerable<Tournament> Get()
        {
            return _data.Tournaments;
        }

        // GET: api/Tournament/5
        public Tournament Get(int id)
        {
            return _data.Tournaments.FirstOrDefault(t => t.Id == id);
        }

        // POST: api/Tournament
        public void Post([FromBody]Tournament value)
        {
            var newId = _data.Tournaments.Max(t => t.Id + 1);

            value.Id = newId;

            _data.Tournaments.Add(value);
        }

        // PUT: api/Tournament/5
        public void Put(int id, [FromBody]Tournament value)
        {
            var existingTournament = _data.Tournaments.FirstOrDefault(t => t.Id == id);

            if(existingTournament == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            existingTournament.Name = value.Name;
        }

        // DELETE: api/Tournament/5
        public void Delete(int id)
        {
            var existingTournament = _data.Tournaments.FirstOrDefault(t => t.Id == id);

            _data.Tournaments.Remove(existingTournament);
        }
    }
}
