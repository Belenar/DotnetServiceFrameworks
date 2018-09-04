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
    builder.EntitySet<BusinessEntityContact>("BusinessEntityContacts");
    builder.EntitySet<BusinessEntity>("BusinessEntities"); 
    builder.EntitySet<ContactType>("ContactTypes"); 
    builder.EntitySet<Person>("People"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BusinessEntityContactsController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/BusinessEntityContacts
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityContact> GetBusinessEntityContacts()
        {
            return _db.BusinessEntityContacts;
        }

        // GET: odata/BusinessEntityContacts(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<BusinessEntityContact> GetBusinessEntityContact([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityContacts.Where(businessEntityContact => businessEntityContact.BusinessEntityID == key));
        }

        // PUT: odata/BusinessEntityContacts(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<BusinessEntityContact> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessEntityContact businessEntityContact = _db.BusinessEntityContacts.Find(key);
            if (businessEntityContact == null)
            {
                return NotFound();
            }

            patch.Put(businessEntityContact);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityContactExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessEntityContact);
        }

        // POST: odata/BusinessEntityContacts
        public IHttpActionResult Post(BusinessEntityContact businessEntityContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.BusinessEntityContacts.Add(businessEntityContact);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BusinessEntityContactExists(businessEntityContact.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(businessEntityContact);
        }

        // PATCH: odata/BusinessEntityContacts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BusinessEntityContact> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessEntityContact businessEntityContact = _db.BusinessEntityContacts.Find(key);
            if (businessEntityContact == null)
            {
                return NotFound();
            }

            patch.Patch(businessEntityContact);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityContactExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessEntityContact);
        }

        // DELETE: odata/BusinessEntityContacts(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BusinessEntityContact businessEntityContact = _db.BusinessEntityContacts.Find(key);
            if (businessEntityContact == null)
            {
                return NotFound();
            }

            _db.BusinessEntityContacts.Remove(businessEntityContact);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BusinessEntityContacts(5)/BusinessEntity
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<BusinessEntity> GetBusinessEntity([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityContacts.Where(m => m.BusinessEntityID == key).Select(m => m.BusinessEntity));
        }

        // GET: odata/BusinessEntityContacts(5)/ContactType
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<ContactType> GetContactType([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityContacts.Where(m => m.BusinessEntityID == key).Select(m => m.ContactType));
        }

        // GET: odata/BusinessEntityContacts(5)/Person
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityContacts.Where(m => m.BusinessEntityID == key).Select(m => m.Person));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessEntityContactExists(int key)
        {
            return _db.BusinessEntityContacts.Count(e => e.BusinessEntityID == key) > 0;
        }
    }
}
