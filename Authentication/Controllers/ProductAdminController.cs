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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/productadmin/getall")]
        public HttpResponseMessage GetAll()
        {
            ProductAdminViewModel kq = new ProductAdminViewModel();
            List<ProductSModel> products = new List<ProductSModel>();
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                List<PRODUCT> pro = data.PRODUCTs.ToList();
                kq.Mans = data.MANUFACTUREs.ToList();
                kq.Cats = data.CATEGORies.ToList();

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/productadmin/get")]
        public HttpResponseMessage Get(int id)
        {
            PRODUCT kq = new PRODUCT();
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                kq = data.PRODUCTs.FirstOrDefault(p => p.PRODUCT_ID == id);
            }

            return CreateResponse(HttpStatusCode.OK, kq);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public HttpResponseMessage UploadImg(UploadImgModel model)
        {
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                PRODUCT kq = data.PRODUCTs.FirstOrDefault(p => p.PRODUCT_ID == model.ID);
                kq.PRODUCT_IMG = model.Link;

                data.SaveChanges();
            }

            return CreateResponse(HttpStatusCode.OK);
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/productadmin/remove")]
        public IHttpActionResult Remove(int id)
        {
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                PRODUCT pro = data.PRODUCTs.FirstOrDefault(p => p.PRODUCT_ID == id);
                pro.DELETED = 1;
                data.SaveChanges();
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/productadmin/editproduct")]
        public IHttpActionResult EditProduct(int id, string name, string cat, string man, double price)
        {
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                PRODUCT pro = data.PRODUCTs.FirstOrDefault(p => p.PRODUCT_ID == id);
                pro.MODEL = name;
                pro.MANUFACTURE_ID = (int)getIdMan(man, data);
                pro.PRICE = price;
                int idCat = getIdCat(cat, data);
                PRODUCT_CATEGORY pcd = data.PRODUCT_CATEGORY.FirstOrDefault(pcds => pcds.PRODUCT_ID == id);
                data.PRODUCT_CATEGORY.Remove(pcd);
                PRODUCT_CATEGORY pc = new PRODUCT_CATEGORY();
                pc.PRODUCT_ID = id;
                pc.CATEGORY_ID = idCat;
                data.PRODUCT_CATEGORY.Add(pc);

                data.SaveChanges();
            }

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/productadmin/addproduct")]
        public IHttpActionResult AddProduct(AddProductViewModel p)
        {
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                PRODUCT pro = new PRODUCT();
                pro.MODEL = p.Name;
                pro.MANUFACTURE_ID = (int)getIdMan(p.NSX, data);
                pro.PRICE = p.Price;
                pro.QUANTITY = 100;
                pro.MODEL_ID = "MSP_01";
                pro.DELETED = 0;

                data.PRODUCTs.Add(pro);
                data.SaveChanges();

                PRODUCT_CATEGORY pc = new PRODUCT_CATEGORY();
                PRODUCT newPro = data.PRODUCTs.ToList()[data.PRODUCTs.Count() - 1];
                pc.PRODUCT_ID = newPro.PRODUCT_ID;
                pc.CATEGORY_ID = getIdCat(p.Category, data);

                data.PRODUCT_CATEGORY.Add(pc);
                data.SaveChanges();
            }

            return Ok();
        }

        private int getIdCat(string cat, MobileStoreServiceEntities data)
        {
            CATEGORY c = data.CATEGORies.FirstOrDefault(ca => ca.CATEGORY_NAME == cat);
            return c.CATEGORY_ID;
        }

        private int getIdMan(string man, MobileStoreServiceEntities data)
        {
            MANUFACTURE mans = data.MANUFACTUREs.FirstOrDefault(m => m.MANUFACTURE_NAME == man);
            return mans.MANUFACTURE_ID;
        }

    }
}
