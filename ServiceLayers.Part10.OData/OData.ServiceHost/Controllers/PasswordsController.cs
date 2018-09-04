using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using OData.DataLayer;

namespace OData.ServiceHost.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using OData.DataLayer;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Password>("Passwords");
    builder.EntitySet<Person>("People"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PasswordsController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/Passwords
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Password> GetPasswords()
        {
            return _db.Passwords;
        }

        // GET: odata/Passwords(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Password> GetPassword([FromODataUri] int key)
        {
            return SingleResult.Create(_db.Passwords.Where(password => password.BusinessEntityID == key));
        }

        // PUT: odata/Passwords(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Password> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Password password = _db.Passwords.Find(key);
            if (password == null)
            {
                return NotFound();
            }

            patch.Put(password);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(password);
        }

        // POST: odata/Passwords
        public IHttpActionResult Post(Password password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Passwords.Add(password);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PasswordExists(password.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(password);
        }

        // PATCH: odata/Passwords(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Password> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Password password = _db.Passwords.Find(key);
            if (password == null)
            {
                return NotFound();
            }

            patch.Patch(password);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasswordExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(password);
        }

        // DELETE: odata/Passwords(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Password password = _db.Passwords.Find(key);
            if (password == null)
            {
                return NotFound();
            }

            _db.Passwords.Remove(password);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Passwords(5)/Person
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(_db.Passwords.Where(m => m.BusinessEntityID == key).Select(m => m.Person));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PasswordExists(int key)
        {
            return _db.Passwords.Count(e => e.BusinessEntityID == key) > 0;
        }
    }
}
