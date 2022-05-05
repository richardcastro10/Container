using Container.Core;
using Container.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace Container.Presentation.Controllers
{
    public class BaseController : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            if (controllerContext.Request.RequestUri.IsAbsoluteUri && controllerContext.Request.RequestUri.Scheme != "https")
                return Task.Run(() => controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must use https"));

            return base.ExecuteAsync(controllerContext, cancellationToken);
        }


    }
}