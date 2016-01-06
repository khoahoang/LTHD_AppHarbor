using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class ProductSModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string NSX { get; set; }
        public bool Editing { get; set; }
        public bool Delete { get; set; }
    }
}