using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class FullProductInfo
    {
        public string NSX { get; set; }
        public PRODUCT product { get; set; }
        public string Price { get; set; }
        public string Status 
        {
            get
            {
                if (product.DELETED == 0)
                    return "Available";
                else
                    return "Not Available";

            }
        }
        public PRODUCT_DESCRIPTION description { get; set; }
        public List<ATTRIBUTE> attribute { get; set; }
    }
}