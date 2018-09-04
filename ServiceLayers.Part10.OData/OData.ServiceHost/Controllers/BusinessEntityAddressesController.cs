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
    builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddresses");
    builder.EntitySet<Address>("Addresses"); 
    builder.EntitySet<AddressType>("AddressTypes"); 
    builder.EntitySet<BusinessEntity>("BusinessEntities"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BusinessEntityAddressesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/BusinessEntityAddresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityAddress> GetBusinessEntityAddresses()
        {
            return _db.BusinessEntityAddresses;
        }

        // GET: odata/BusinessEntityAddresses(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<BusinessEntityAddress> GetBusinessEntityAddress([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityAddresses.Where(businessEntityAddress => businessEntityAddress.BusinessEntityID == key));
        }

        // PUT: odata/BusinessEntityAddresses(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<BusinessEntityAddress> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessEntityAddress businessEntityAddress = _db.BusinessEntityAddresses.Find(key);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }

            patch.Put(businessEntityAddress);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityAddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessEntityAddress);
        }

        // POST: odata/BusinessEntityAddresses
        public IHttpActionResult Post(BusinessEntityAddress businessEntityAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.BusinessEntityAddresses.Add(businessEntityAddress);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BusinessEntityAddressExists(businessEntityAddress.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(businessEntityAddress);
        }

        // PATCH: odata/BusinessEntityAddresses(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BusinessEntityAddress> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BusinessEntityAddress businessEntityAddress = _db.BusinessEntityAddresses.Find(key);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }

            patch.Patch(businessEntityAddress);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityAddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(businessEntityAddress);
        }

        // DELETE: odata/BusinessEntityAddresses(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BusinessEntityAddress businessEntityAddress = _db.BusinessEntityAddresses.Find(key);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }

            _db.BusinessEntityAddresses.Remove(businessEntityAddress);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BusinessEntityAddresses(5)/Address
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Address> GetAddress([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityAddresses.Where(m => m.BusinessEntityID == key).Select(m => m.Address));
        }

        // GET: odata/BusinessEntityAddresses(5)/AddressType
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<AddressType> GetAddressType([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityAddresses.Where(m => m.BusinessEntityID == key).Select(m => m.AddressType));
        }

        // GET: odata/BusinessEntityAddresses(5)/BusinessEntity
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<BusinessEntity> GetBusinessEntity([FromODataUri] int key)
        {
            return SingleResult.Create(_db.BusinessEntityAddresses.Where(m => m.BusinessEntityID == key).Select(m => m.BusinessEntity));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessEntityAddressExists(int key)
        {
            return _db.BusinessEntityAddresses.Count(e => e.BusinessEntityID == key) > 0;
        }
    }
}
