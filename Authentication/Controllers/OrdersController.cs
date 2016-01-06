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
