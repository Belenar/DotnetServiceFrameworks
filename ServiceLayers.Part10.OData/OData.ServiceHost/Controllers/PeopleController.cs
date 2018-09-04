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
    builder.EntitySet<Person>("People");
    builder.EntitySet<BusinessEntity>("BusinessEntities"); 
    builder.EntitySet<BusinessEntityContact>("BusinessEntityContacts"); 
    builder.EntitySet<EmailAddress>("EmailAddresses"); 
    builder.EntitySet<Password>("Passwords"); 
    builder.EntitySet<PersonPhone>("PersonPhones"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PeopleController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/People
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Person> GetPeople()
        {
            return _db.People;
        }

        // GET: odata/People(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(_db.People.Where(person => person.BusinessEntityID == key));
        }

        // PUT: odata/People(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = _db.People.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Put(person);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // POST: odata/People
        public IHttpActionResult Post(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.People.Add(person);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(person);
        }

        // PATCH: odata/People(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = _db.People.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Patch(person);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // DELETE: odata/People(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Person person = _db.People.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            _db.People.Remove(person);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/People(5)/BusinessEntity
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<BusinessEntity> GetBusinessEntity([FromODataUri] int key)
        {
            return SingleResult.Create(_db.People.Where(m => m.BusinessEntityID == key).Select(m => m.BusinessEntity));
        }

        // GET: odata/People(5)/BusinessEntityContacts
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityContact> GetBusinessEntityContacts([FromODataUri] int key)
        {
            return _db.People.Where(m => m.BusinessEntityID == key).SelectMany(m => m.BusinessEntityContacts);
        }

        // GET: odata/People(5)/EmailAddresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<EmailAddress> GetEmailAddresses([FromODataUri] int key)
        {
            return _db.People.Where(m => m.BusinessEntityID == key).SelectMany(m => m.EmailAddresses);
        }

        // GET: odata/People(5)/Password
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Password> GetPassword([FromODataUri] int key)
        {
            return SingleResult.Create(_db.People.Where(m => m.BusinessEntityID == key).Select(m => m.Password));
        }

        // GET: odata/People(5)/PersonPhones
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<PersonPhone> GetPersonPhones([FromODataUri] int key)
        {
            return _db.People.Where(m => m.BusinessEntityID == key).SelectMany(m => m.PersonPhones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int key)
        {
            return _db.People.Count(e => e.BusinessEntityID == key) > 0;
        }
    }
}
