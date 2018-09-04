using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiSample.Web.Handlers
{
    public class VjParrotHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(!request.Headers.TryGetValues("VJParrotHeader", out var headers))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            if(headers.First() != "present")
                return new HttpResponseMessage(HttpStatusCode.Forbidden);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}