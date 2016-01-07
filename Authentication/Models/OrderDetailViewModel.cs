using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class OrderDetailViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total
        {
            get
            {
                return Quantity * Price;
            }
        }
    }
}