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
    builder.EntitySet<Address>("Addresses");
    builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddresses"); 
    builder.EntitySet<StateProvince>("StateProvinces"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AddressesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/Addresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Address> GetAddresses()
        {
            return _db.Addresses;
        }

        // GET: odata/Addresses(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Address> GetAddress([FromODataUri] int key)
        {
            return SingleResult.Create(_db.Addresses.Where(address => address.AddressID == key));
        }

        // PUT: odata/Addresses(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Address> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = _db.Addresses.Find(key);
            if (address == null)
            {
                return NotFound();
            }

            patch.Put(address);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(address);
        }

        // POST: odata/Addresses
        public IHttpActionResult Post(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Addresses.Add(address);
            _db.SaveChanges();

            return Created(address);
        }

        // PATCH: odata/Addresses(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Address> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = _db.Addresses.Find(key);
            if (address == null)
            {
                return NotFound();
            }

            patch.Patch(address);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(address);
        }

        // DELETE: odata/Addresses(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Address address = _db.Addresses.Find(key);
            if (address == null)
            {
                return NotFound();
            }

            _db.Addresses.Remove(address);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Addresses(5)/BusinessEntityAddresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BusinessEntityAddress> GetBusinessEntityAddresses([FromODataUri] int key)
        {
            return _db.Addresses.Where(m => m.AddressID == key).SelectMany(m => m.BusinessEntityAddresses);
        }

        // GET: odata/Addresses(5)/StateProvince
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<StateProvince> GetStateProvince([FromODataUri] int key)
        {
            return SingleResult.Create(_db.Addresses.Where(m => m.AddressID == key).Select(m => m.StateProvince));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int key)
        {
            return _db.Addresses.Count(e => e.AddressID == key) > 0;
        }
    }
}
