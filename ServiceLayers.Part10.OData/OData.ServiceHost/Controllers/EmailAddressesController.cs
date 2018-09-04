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
    builder.EntitySet<EmailAddress>("EmailAddresses");
    builder.EntitySet<Person>("People"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EmailAddressesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/EmailAddresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<EmailAddress> GetEmailAddresses()
        {
            return _db.EmailAddresses;
        }

        // GET: odata/EmailAddresses(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<EmailAddress> GetEmailAddress([FromODataUri] int key)
        {
            return SingleResult.Create(_db.EmailAddresses.Where(emailAddress => emailAddress.BusinessEntityID == key));
        }

        // PUT: odata/EmailAddresses(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<EmailAddress> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmailAddress emailAddress = _db.EmailAddresses.Find(key);
            if (emailAddress == null)
            {
                return NotFound();
            }

            patch.Put(emailAddress);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailAddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(emailAddress);
        }

        // POST: odata/EmailAddresses
        public IHttpActionResult Post(EmailAddress emailAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.EmailAddresses.Add(emailAddress);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmailAddressExists(emailAddress.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(emailAddress);
        }

        // PATCH: odata/EmailAddresses(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<EmailAddress> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmailAddress emailAddress = _db.EmailAddresses.Find(key);
            if (emailAddress == null)
            {
                return NotFound();
            }

            patch.Patch(emailAddress);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailAddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(emailAddress);
        }

        // DELETE: odata/EmailAddresses(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            EmailAddress emailAddress = _db.EmailAddresses.Find(key);
            if (emailAddress == null)
            {
                return NotFound();
            }

            _db.EmailAddresses.Remove(emailAddress);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EmailAddresses(5)/Person
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(_db.EmailAddresses.Where(m => m.BusinessEntityID == key).Select(m => m.Person));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmailAddressExists(int key)
        {
            return _db.EmailAddresses.Count(e => e.BusinessEntityID == key) > 0;
        }
    }
}
