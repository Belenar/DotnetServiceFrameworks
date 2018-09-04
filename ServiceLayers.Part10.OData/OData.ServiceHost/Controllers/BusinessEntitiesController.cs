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
    builder.EntitySet<BusinessEntity>("BusinessEntities");
    builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddresses"); 
    builder.EntitySet<BusinessEntityContact>("BusinessEntityContacts"); 
    builder.EntitySet<Person>("People"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BusinessEntitiesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/BusinessEntities
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntity> GetBusinessEntities()
        {
            return _db.BusinessEntities;
        }

        // GET: odata/BusinessEntities(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<BusinessEntity> GetBusinessEntity([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntities.Where(businessEntity => businessEntity.BusinessEntityID == key));
        }

        // PUT: odata/BusinessEntities(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<BusinessEntity> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessEntity businessEntity = _db.BusinessEntities.Find(key);
            if (businessEntity == null)
            {
                return NotFound();
            }

            patch.Put(businessEntity);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessEntity);
        }

        // POST: odata/BusinessEntities
        public IHttpActionResult Post(BusinessEntity businessEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.BusinessEntities.Add(businessEntity);
            _db.SaveChanges();

            return Created(businessEntity);
        }

        // PATCH: odata/BusinessEntities(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BusinessEntity> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessEntity businessEntity = _db.BusinessEntities.Find(key);
            if (businessEntity == null)
            {
                return NotFound();
            }

            patch.Patch(businessEntity);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessEntity);
        }

        // DELETE: odata/BusinessEntities(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BusinessEntity businessEntity = _db.BusinessEntities.Find(key);
            if (businessEntity == null)
            {
                return NotFound();
            }

            _db.BusinessEntities.Remove(businessEntity);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BusinessEntities(5)/BusinessEntityAddresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityAddress> GetBusinessEntityAddresses([FromODataUri] int key)
        {
            return _db.BusinessEntities.Where(m => m.BusinessEntityID == key).SelectMany(m => m.BusinessEntityAddresses);
        }

        // GET: odata/BusinessEntities(5)/BusinessEntityContacts
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityContact> GetBusinessEntityContacts([FromODataUri] int key)
        {
            return _db.BusinessEntities.Where(m => m.BusinessEntityID == key).SelectMany(m => m.BusinessEntityContacts);
        }

        // GET: odata/BusinessEntities(5)/Person
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntities.Where(m => m.BusinessEntityID == key).Select(m => m.Person));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessEntityExists(int key)
        {
            return _db.BusinessEntities.Count(e => e.BusinessEntityID == key) > 0;
        }
    }
}
