using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string NSX { get; set; }
    }
}