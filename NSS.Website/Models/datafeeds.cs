using System.Collections.Generic;

namespace NSS.Website.Models
{
    public class datafeeds
    {
        public List<feeds> data { get; set; }
    }
    public class feeds
    {

        public string aff_link { get; set; }
        public string campaign { get; set; }
        public string cate { get; set; }
        public string desc { get; set; }
        public string discount { get; set; }
        public string discount_amount { get; set; }
        public string discount_rate { get; set; }
        public string domain { get; set; }
        public string image { get; set; }
        public string merchant { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string product_id { get; set; }
        public string promotion { get; set; }
        public string sku { get; set; }
        public string status_discount { get; set; }
        public string update_time { get; set; }
        public string url { get; set; }
    }
}
