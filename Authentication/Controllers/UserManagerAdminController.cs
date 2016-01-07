using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication.Controllers
{
    public class UserManagerAdminController : ApiController
    {
        #region Helper
        public HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, T data)
        {
            return Request.CreateResponse(statusCode, data);
        }

        public HttpResponseMessage CreateResponse(HttpStatusCode httpStatusCode)
        {
            return Request.CreateResponse(httpStatusCode);
        }
        #endregion
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/usermanageradmin/getall")]
        public HttpResponseMessage GetAll()
        {
            return CreateResponse(HttpStatusCode.OK);
        }
    }
}
