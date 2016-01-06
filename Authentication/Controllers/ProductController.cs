using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MobileSt.Models;
using Newtonsoft.Json;
using Authentication;
using Authentication.Models;
namespace MobileSt.Controllers
{
    public class ProductController : ApiController
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
        [HttpGet]
        public HttpResponseMessage GetProduct(int id)
        {
            FullProductInfo ProductDetail = new FullProductInfo();
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                List<MANUFACTURE> list = data.MANUFACTUREs.ToList();

                ProductDetail.product = (from e in data.PRODUCTs
                                         where e.PRODUCT_ID == id
                                         select e).FirstOrDefault();

                MANUFACTURE m = list.SingleOrDefault(x => x.MANUFACTURE_ID == ProductDetail.product.MANUFACTURE_ID);
                ProductDetail.NSX = m.MANUFACTURE_NAME;

                ProductDetail.description = (from e in data.PRODUCT_DESCRIPTION
                                             where e.PRODUCT_ID == id
                                             select e).FirstOrDefault();

                ProductDetail.attribute = (from e in data.ATTRIBUTEs
                                           where e.PRODUCT_ID == id
                                           select e).ToList();
            }

            string json = JsonConvert.SerializeObject(ProductDetail);
            return CreateResponse(HttpStatusCode.OK, ProductDetail);
        }
    }
}
