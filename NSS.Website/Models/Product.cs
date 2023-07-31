using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSS.Website.Models
{
    public class Product
    { 
        public string ProductName { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string PriceDiscount { get; set; }
        public string Description { get; set; }
        public string Merchant { get; set; }
    }
}