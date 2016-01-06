using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication.Controllers
{
    public class CatergoryAdminController : ApiController
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
        public HttpResponseMessage GetAll()
        {
            List<CategoryViewModel> kq = new List<CategoryViewModel>();
            List<CATEGORY> list = new List<CATEGORY>();
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                list = data.CATEGORies.ToList();
                foreach (var item in list)
                {
                    CategoryViewModel cat = new CategoryViewModel();
                    cat.ID = item.CATEGORY_ID;
                    cat.Name = item.CATEGORY_NAME;
                    cat.Editing = false;

                    kq.Add(cat);
                }
            }

            return CreateResponse(HttpStatusCode.OK, kq);
        }

        [HttpPost]
        public HttpResponseMessage EditCategory(int id, string name)
        {
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                CATEGORY c = data.CATEGORies.FirstOrDefault(cat => cat.CATEGORY_ID == id);
                c.CATEGORY_NAME = name;
                data.SaveChanges();
            }
            return CreateResponse(HttpStatusCode.OK);
        }
    }
}
