using Authentication;
using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileSt.Models
{
    public class ProductofCategory
    {
        public CATEGORY category { get; set; }

        public List<PRODUCT> listProduct { get; set; }
    }
}