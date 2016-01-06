using Authentication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication.Controllers
{
    public class ProductAdminController : ApiController
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
            ProductAdminViewModel kq = new ProductAdminViewModel();
            List<ProductSModel> products = new List<ProductSModel>();
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                List<PRODUCT> pro = data.PRODUCTs.ToList();
                kq.Mans = data.MANUFACTUREs.ToList();

                foreach (var item in pro)
                {
                    ProductSModel s = new ProductSModel();
                    s.ID = item.PRODUCT_ID;
                    s.Name = item.MODEL;
                    s.Image = item.PRODUCT_IMG;
                    s.Price = item.PRICE;
                    s.Category = getNameOfCategory(item.PRODUCT_ID, data);
                    s.NSX = getNameOfNXS((int)item.MANUFACTURE_ID, data);
                    s.Editing = false;
                    s.Delete = item.DELETED == 1;

                    products.Add(s);
                }

                kq.Products = products;
            }

            string json = JsonConvert.SerializeObject(products);
            return CreateResponse(HttpStatusCode.OK, kq);
        }

        private string getNameOfNXS(int manId, MobileStoreServiceEntities data)
        {
            List<MANUFACTURE> mans = data.MANUFACTUREs.ToList();
            foreach (var item in mans)
            {
                if (item.MANUFACTURE_ID == manId)
                    return item.MANUFACTURE_NAME;
            }

            return "";
        }

        private string getNameOfCategory(int proId, MobileStoreServiceEntities data)
        {
            List<PRODUCT_CATEGORY> proCats = data.PRODUCT_CATEGORY.ToList();
            List<CATEGORY> cats = data.CATEGORies.ToList();
            foreach (var item in proCats)
            {
                if (item.PRODUCT_ID == proId)
                {
                    foreach (var cat in cats)
                    {
                        if (cat.CATEGORY_ID == item.CATEGORY_ID)
                            return cat.CATEGORY_NAME;
                    }
                }
            }

            return "";
        }

        [HttpPost]
        public HttpResponseMessage Remove(int id)
        {
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                PRODUCT pro = data.PRODUCTs.FirstOrDefault(p => p.PRODUCT_ID == id);
                pro.DELETED = 1;
                data.SaveChanges();
            }

            return CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage EditProduct(int id, string name, string man, double price)
        {
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                PRODUCT pro = data.PRODUCTs.FirstOrDefault(p => p.PRODUCT_ID == id);
                pro.MODEL = name;
                pro.MANUFACTURE_ID = (int)getIdMan(man, data);
                pro.PRICE = price;

                data.SaveChanges();
            }

            return CreateResponse(HttpStatusCode.OK);
        }

        private int getIdMan(string man, MobileStoreServiceEntities data)
        {
            MANUFACTURE mans = data.MANUFACTUREs.FirstOrDefault(m => m.MANUFACTURE_NAME == man);
            return mans.MANUFACTURE_ID;
        }
    }
}
