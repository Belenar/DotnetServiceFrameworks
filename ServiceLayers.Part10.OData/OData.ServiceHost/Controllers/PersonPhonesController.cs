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
    builder.EntitySet<PersonPhone>("PersonPhones");
    builder.EntitySet<Person>("People"); 
    builder.EntitySet<PhoneNumberType>("PhoneNumberTypes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PersonPhonesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/PersonPhones
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<PersonPhone> GetPersonPhones()
        {
            return _db.PersonPhones;
        }

        // GET: odata/PersonPhones(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<PersonPhone> GetPersonPhone([FromODataUri] int key)
        {
            return SingleResult.Create(_db.PersonPhones.Where(personPhone => personPhone.BusinessEntityID == key));
        }

        // PUT: odata/PersonPhones(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<PersonPhone> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonPhone personPhone = _db.PersonPhones.Find(key);
            if (personPhone == null)
            {
                return NotFound();
            }

            patch.Put(personPhone);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonPhoneExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personPhone);
        }

        // POST: odata/PersonPhones
        public IHttpActionResult Post(PersonPhone personPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.PersonPhones.Add(personPhone);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonPhoneExists(personPhone.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(personPhone);
        }

        // PATCH: odata/PersonPhones(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PersonPhone> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonPhone personPhone = _db.PersonPhones.Find(key);
            if (personPhone == null)
            {
                return NotFound();
            }

            patch.Patch(personPhone);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonPhoneExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personPhone);
        }

        // DELETE: odata/PersonPhones(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PersonPhone personPhone = _db.PersonPhones.Find(key);
            if (personPhone == null)
            {
                return NotFound();
            }

            _db.PersonPhones.Remove(personPhone);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PersonPhones(5)/Person
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(_db.PersonPhones.Where(m => m.BusinessEntityID == key).Select(m => m.Person));
        }

        // GET: odata/PersonPhones(5)/PhoneNumberType
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<PhoneNumberType> GetPhoneNumberType([FromODataUri] int key)
        {
            return SingleResult.Create(_db.PersonPhones.Where(m => m.BusinessEntityID == key).Select(m => m.PhoneNumberType));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonPhoneExists(int key)
        {
            return _db.PersonPhones.Count(e => e.BusinessEntityID == key) > 0;
        }
    }
}
