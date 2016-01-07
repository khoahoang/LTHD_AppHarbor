using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Authentication.Models;
using PagedList;

namespace Authentication.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Search(string keyword, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            MobileStoreServiceEntities sv = new MobileStoreServiceEntities();
            List<PRODUCT> lstProduct = sv.PRODUCTs.Where(x => x.MODEL.Contains(keyword) && x.DELETED == 0).ToList();
            int totalPages = lstProduct.Count/pageSize + 1;

            var result = new
            {
                TotalPages = totalPages,
                Products = lstProduct.ToPagedList(pageNumber, pageSize)
            };
            return CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpGet]
        public HttpResponseMessage SearchPage(string keyword)
        {
            MobileStoreServiceEntities sv = new MobileStoreServiceEntities();
            List<PRODUCT> lstProduct = sv.PRODUCTs.Where(x => x.MODEL.Contains(keyword)).ToList();

            return CreateResponse(HttpStatusCode.OK, lstProduct);
        }
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
    }
}
