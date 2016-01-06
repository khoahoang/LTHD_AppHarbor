using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class SubmitOrderModel
    {
        public UserOrderInfo user_info;
        public OrderItem[] order_info;
    }
    public class UserOrderInfo
    {
        public string username;
        public string name;
        public string address;
        public string phone;
    }
    public class OrderItem
    {
        public int product_id;
        public int quantity;
        public float unit_price;
    }
}