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
    builder.EntitySet<PhoneNumberType>("PhoneNumberTypes");
    builder.EntitySet<PersonPhone>("PersonPhones"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PhoneNumberTypesController : ODataController
    {
        private readonly AdventureWorksContext _db = new AdventureWorksContext();

        // GET: odata/PhoneNumberTypes
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<PhoneNumberType> GetPhoneNumberTypes()
        {
            return _db.PhoneNumberTypes;
        }

        // GET: odata/PhoneNumberTypes(5)
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<PhoneNumberType> GetPhoneNumberType([FromODataUri] int key)
        {
            return SingleResult.Create(_db.PhoneNumberTypes.Where(phoneNumberType => phoneNumberType.PhoneNumberTypeID == key));
        }

        // PUT: odata/PhoneNumberTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<PhoneNumberType> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PhoneNumberType phoneNumberType = _db.PhoneNumberTypes.Find(key);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            patch.Put(phoneNumberType);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(phoneNumberType);
        }

        // POST: odata/PhoneNumberTypes
        public IHttpActionResult Post(PhoneNumberType phoneNumberType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.PhoneNumberTypes.Add(phoneNumberType);
            _db.SaveChanges();

            return Created(phoneNumberType);
        }

        // PATCH: odata/PhoneNumberTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PhoneNumberType> patch)
        {
            Validate(patch.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PhoneNumberType phoneNumberType = _db.PhoneNumberTypes.Find(key);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            patch.Patch(phoneNumberType);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(phoneNumberType);
        }

        // DELETE: odata/PhoneNumberTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PhoneNumberType phoneNumberType = _db.PhoneNumberTypes.Find(key);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            _db.PhoneNumberTypes.Remove(phoneNumberType);
            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PhoneNumberTypes(5)/PersonPhones
        [EnableQuery(AllowedFunctions = AllowedFunctions.All, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<PersonPhone> GetPersonPhones([FromODataUri] int key)
        {
            return _db.PhoneNumberTypes.Where(m => m.PhoneNumberTypeID == key).SelectMany(m => m.PersonPhones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneNumberTypeExists(int key)
        {
            return _db.PhoneNumberTypes.Count(e => e.PhoneNumberTypeID == key) > 0;
        }
    }
}
