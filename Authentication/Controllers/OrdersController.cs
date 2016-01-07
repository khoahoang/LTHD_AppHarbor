using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Authentication.Models;
using Authentication;
namespace Authentication.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
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
        [Authorize(Roles="Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            List<ORDER> OrderList = new List<ORDER>();
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                OrderList = (from e in data.ORDERS
                             select e).ToList();
            }
            return Ok(OrderList);
        }
        [Authorize]
        [HttpGet]
        [Route("GetUserOrder")]
        public IHttpActionResult GetUserOrder()
        {
            List<ORDER> OrderList = new List<ORDER>();
            using (MobileStoreServiceEntities data = new MobileStoreServiceEntities())
            {
                OrderList = (from e in data.ORDERS
                             where e.USERNAME == User.Identity.Name
                             select e).ToList();
            }
            return Ok(OrderList);
        }

        [Authorize]
        [HttpPost]
        [Route("Submit")]
        public IHttpActionResult Submit(SubmitOrderModel data)
        {
            MobileStoreServiceEntities db = new MobileStoreServiceEntities();
            try
            {
                ORDER order = new ORDER();

                order.NAME = data.user_info.name;
                order.ADDRESS = data.user_info.address;
                order.PHONE = data.user_info.phone;
                order.USERNAME = data.user_info.username;
                order.ORDER_DATE = DateTime.Now;
                order.PAID = 0;
                order.DELETED = 0;
                db.ORDERS.Add(order);
                db.SaveChanges();

                if(CreateOrder(order, data) == false)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)); 
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)); 
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("OrderDetail")]
        public HttpResponseMessage OrderDetail(int id)
        {
            List<OrderDetailViewModel> kq = new List<OrderDetailViewModel>();
            using (MobileStoreServiceEntities db = new MobileStoreServiceEntities())
            {
                List<ORDER_DETAILS> ords = db.ORDER_DETAILS.Where(o => o.ORDER_ID == id).ToList();
                foreach (var item in ords)
                {
                    OrderDetailViewModel ord = new OrderDetailViewModel();
                    ord.Name = getProductName(item.PRODUCT_ID, db);
                    ord.Quantity = item.QUANTITY;
                    ord.Price = item.UNIT_PRICE;

                    kq.Add(ord);
                }
            }
            return CreateResponse(HttpStatusCode.OK, kq);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Remove")]
        public HttpResponseMessage Remove(int id)
        {
            using (MobileStoreServiceEntities db = new MobileStoreServiceEntities())
            {
                ORDER o = db.ORDERS.FirstOrDefault(or => or.ORDER_ID == id);
                o.DELETED = 1;
                db.SaveChanges();
            }
            return CreateResponse(HttpStatusCode.OK);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Paid")]
        public HttpResponseMessage Paid(int id)
        {
            using (MobileStoreServiceEntities db = new MobileStoreServiceEntities())
            {
                ORDER o = db.ORDERS.FirstOrDefault(or => or.ORDER_ID == id);
                o.PAID = 1;
                db.SaveChanges();
            }
            return CreateResponse(HttpStatusCode.OK);
        }

        private string getProductName(int id, MobileStoreServiceEntities db)
        {
            PRODUCT p = db.PRODUCTs.FirstOrDefault(s => s.PRODUCT_ID == id);
            return p.MODEL;
        }

        private bool CreateOrder(ORDER order, SubmitOrderModel data)
        {
            MobileStoreServiceEntities db = new MobileStoreServiceEntities();
            double orderTotal = 0;            
            try
            {
                // Iterate over the items in the cart, 
                // adding the order details for each
                foreach (var item in data.order_info)
                {
                    PRODUCT product = db.PRODUCTs.First(x => x.PRODUCT_ID == item.product_id);
                    product.QUANTITY -= item.quantity;
                    var orderDetail = new ORDER_DETAILS
                    {
                        PRODUCT_ID = item.product_id,
                        ORDER_ID = order.ORDER_ID,
                        UNIT_PRICE = item.unit_price,
                        QUANTITY = item.quantity
                    };
                    // Set the order total of the shopping cart
                    orderTotal += (item.quantity * item.unit_price);

                    db.ORDER_DETAILS.Add(orderDetail);
                }
                // Set the order's total to the orderTotal count
                ORDER nOrder = db.ORDERS.First(o => o.ORDER_ID == order.ORDER_ID);
                nOrder.TOTAL = orderTotal;

                // Save the order
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            // Return the OrderId as the confirmation number
            return true;
        }
    }

}
