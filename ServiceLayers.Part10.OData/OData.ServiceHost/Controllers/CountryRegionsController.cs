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
    builder.EntitySet<CountryRegion>("CountryRegions");
    builder.EntitySet<StateProvince>("StateProvinces"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CountryRegionsController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/CountryRegions
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<CountryRegion> GetCountryRegions()
        {
            return _db.CountryRegions;
        }

        // GET: odata/CountryRegions(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<CountryRegion> GetCountryRegion([FromODataUri] string key)
        {
            return SingleResult.Create(_db.CountryRegions.Where(countryRegion => countryRegion.CountryRegionCode == key));
        }

        // PUT: odata/CountryRegions(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<CountryRegion> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CountryRegion countryRegion = _db.CountryRegions.Find(key);
            if (countryRegion == null)
            {
                return NotFound();
            }

            patch.Put(countryRegion);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryRegionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(countryRegion);
        }

        // POST: odata/CountryRegions
        public IHttpActionResult Post(CountryRegion countryRegion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.CountryRegions.Add(countryRegion);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CountryRegionExists(countryRegion.CountryRegionCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(countryRegion);
        }

        // PATCH: odata/CountryRegions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<CountryRegion> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CountryRegion countryRegion = _db.CountryRegions.Find(key);
            if (countryRegion == null)
            {
                return NotFound();
            }

            patch.Patch(countryRegion);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryRegionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(countryRegion);
        }

        // DELETE: odata/CountryRegions(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            CountryRegion countryRegion = _db.CountryRegions.Find(key);
            if (countryRegion == null)
            {
                return NotFound();
            }

            _db.CountryRegions.Remove(countryRegion);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/CountryRegions(5)/StateProvinces
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<StateProvince> GetStateProvinces([FromODataUri] string key)
        {
            return _db.CountryRegions.Where(m => m.CountryRegionCode == key).SelectMany(m => m.StateProvinces);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryRegionExists(string key)
        {
            return _db.CountryRegions.Count(e => e.CountryRegionCode == key) > 0;
        }
    }
}
