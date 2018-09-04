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
    builder.EntitySet<AddressType>("AddressTypes");
    builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddresses"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AddressTypesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/AddressTypes
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<AddressType> GetAddressTypes()
        {
            return _db.AddressTypes;
        }

        // GET: odata/AddressTypes(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<AddressType> GetAddressType([FromODataUri] int key)
        {
            return SingleResult.Create(_db.AddressTypes.Where(addressType => addressType.AddressTypeID == key));
        }

        // PUT: odata/AddressTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<AddressType> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressType addressType = _db.AddressTypes.Find(key);
            if (addressType == null)
            {
                return NotFound();
            }

            patch.Put(addressType);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(addressType);
        }

        // POST: odata/AddressTypes
        public IHttpActionResult Post(AddressType addressType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.AddressTypes.Add(addressType);
            _db.SaveChanges();

            return Created(addressType);
        }

        // PATCH: odata/AddressTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<AddressType> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressType addressType = _db.AddressTypes.Find(key);
            if (addressType == null)
            {
                return NotFound();
            }

            patch.Patch(addressType);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(addressType);
        }

        // DELETE: odata/AddressTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            AddressType addressType = _db.AddressTypes.Find(key);
            if (addressType == null)
            {
                return NotFound();
            }

            _db.AddressTypes.Remove(addressType);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AddressTypes(5)/BusinessEntityAddresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityAddress> GetBusinessEntityAddresses([FromODataUri] int key)
        {
            return _db.AddressTypes.Where(m => m.AddressTypeID == key).SelectMany(m => m.BusinessEntityAddresses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressTypeExists(int key)
        {
            return _db.AddressTypes.Count(e => e.AddressTypeID == key) > 0;
        }
    }
}
