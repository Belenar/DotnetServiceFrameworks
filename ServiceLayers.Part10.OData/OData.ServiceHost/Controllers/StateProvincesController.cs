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
    builder.EntitySet<StateProvince>("StateProvinces");
    builder.EntitySet<Address>("Addresses"); 
    builder.EntitySet<CountryRegion>("CountryRegions"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class StateProvincesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/StateProvinces
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<StateProvince> GetStateProvinces()
        {
            return _db.StateProvinces;
        }

        // GET: odata/StateProvinces(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<StateProvince> GetStateProvince([FromODataUri] int key)
        {
            return SingleResult.Create(_db.StateProvinces.Where(stateProvince => stateProvince.StateProvinceID == key));
        }

        // PUT: odata/StateProvinces(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<StateProvince> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StateProvince stateProvince = _db.StateProvinces.Find(key);
            if (stateProvince == null)
            {
                return NotFound();
            }

            patch.Put(stateProvince);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateProvinceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(stateProvince);
        }

        // POST: odata/StateProvinces
        public IHttpActionResult Post(StateProvince stateProvince)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.StateProvinces.Add(stateProvince);
            _db.SaveChanges();

            return Created(stateProvince);
        }

        // PATCH: odata/StateProvinces(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<StateProvince> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StateProvince stateProvince = _db.StateProvinces.Find(key);
            if (stateProvince == null)
            {
                return NotFound();
            }

            patch.Patch(stateProvince);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateProvinceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(stateProvince);
        }

        // DELETE: odata/StateProvinces(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            StateProvince stateProvince = _db.StateProvinces.Find(key);
            if (stateProvince == null)
            {
                return NotFound();
            }

            _db.StateProvinces.Remove(stateProvince);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/StateProvinces(5)/Addresses
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Address> GetAddresses([FromODataUri] int key)
        {
            return _db.StateProvinces.Where(m => m.StateProvinceID == key).SelectMany(m => m.Addresses);
        }

        // GET: odata/StateProvinces(5)/CountryRegion
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<CountryRegion> GetCountryRegion([FromODataUri] int key)
        {
            return SingleResult.Create(_db.StateProvinces.Where(m => m.StateProvinceID == key).Select(m => m.CountryRegion));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateProvinceExists(int key)
        {
            return _db.StateProvinces.Count(e => e.StateProvinceID == key) > 0;
        }
    }
}
