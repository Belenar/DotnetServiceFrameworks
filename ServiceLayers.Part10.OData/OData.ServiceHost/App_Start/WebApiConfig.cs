using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using OData.DataLayer;

namespace OData.ServiceHost
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Address>("Addresses");
            builder.EntitySet<AddressType>("AddressTypes");
            builder.EntitySet<BusinessEntity>("BusinessEntities");
            builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddresses");
            builder.EntitySet<BusinessEntityContact>("BusinessEntityContacts");
            builder.EntitySet<ContactType>("ContactTypes");
            builder.EntitySet<CountryRegion>("CountryRegions");
            builder.EntitySet<EmailAddress>("EmailAddresses");
            builder.EntitySet<Password>("Passwords");
            builder.EntitySet<Person>("People");
            builder.EntitySet<PersonPhone>("PersonPhones");
            builder.EntitySet<PhoneNumberType>("PhoneNumberTypes");
            builder.EntitySet<StateProvince>("StateProvinces");

            config.MapODataServiceRoute("OData", "odata", builder.GetEdmModel());

            config
                .Filter()     // Where
                .Expand()     // ~Include in EF
                .Select()     // Projections
                .OrderBy()    // Duh
                .MaxTop(null) // Maximum result size
                .Count();     // Duh
        }
    }
}
