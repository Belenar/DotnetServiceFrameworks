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
    builder.EntitySet<ContactType>("ContactTypes");
    builder.EntitySet<BusinessEntityContact>("BusinessEntityContacts"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ContactTypesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/ContactTypes
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<ContactType> GetContactTypes()
        {
            return _db.ContactTypes;
        }

        // GET: odata/ContactTypes(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<ContactType> GetContactType([FromODataUri] int key)
        {
            return SingleResult.Create(_db.ContactTypes.Where(contactType => contactType.ContactTypeID == key));
        }

        // PUT: odata/ContactTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ContactType> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContactType contactType = _db.ContactTypes.Find(key);
            if (contactType == null)
            {
                return NotFound();
            }

            patch.Put(contactType);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contactType);
        }

        // POST: odata/ContactTypes
        public IHttpActionResult Post(ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.ContactTypes.Add(contactType);
            _db.SaveChanges();

            return Created(contactType);
        }

        // PATCH: odata/ContactTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ContactType> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContactType contactType = _db.ContactTypes.Find(key);
            if (contactType == null)
            {
                return NotFound();
            }

            patch.Patch(contactType);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(contactType);
        }

        // DELETE: odata/ContactTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ContactType contactType = _db.ContactTypes.Find(key);
            if (contactType == null)
            {
                return NotFound();
            }

            _db.ContactTypes.Remove(contactType);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ContactTypes(5)/BusinessEntityContacts
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityContact> GetBusinessEntityContacts([FromODataUri] int key)
        {
            return _db.ContactTypes.Where(m => m.ContactTypeID == key).SelectMany(m => m.BusinessEntityContacts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactTypeExists(int key)
        {
            return _db.ContactTypes.Count(e => e.ContactTypeID == key) > 0;
        }
    }
}
