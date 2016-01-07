using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class ProductAdminViewModel
    {
        public List<ProductSModel> Products { get; set; }
        public List<MANUFACTURE> Mans { get; set; }
        public List<CATEGORY> Cats { get; set; }
    }
}