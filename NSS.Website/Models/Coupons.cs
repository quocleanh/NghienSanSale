using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSS.Website.Models
{
    public class Coupons
    {
        public Int64 count { get; set; } = 1;
        public int column { get; set; }
        public List<Coupon> data { get; set; }
    }
    public class Coupon
    {
        public string id { get; set; }
        public string aff_link { get; set; }
        public string aff_link_campaign_tag { get; set; }
        public string[] banner { get; set; }
        public string campaign { get; set; }
        public string campaign_id { get; set; }
        public string campaign_name { get; set; }
        public List<category> categories { get; set; }
        public string coin_percentage { get; set; }
        public string coin_cap { get; set; }
        public string content { get; set; }
        public List<Coupon_Item> coupons { get; set; }
        public string discount_percentage { get; set; }
        public string discount_value { get; set; }
        public string domain { get; set; }
        public string end_time { get; set; }
        public string image { get; set; }
        public bool is_hot { get; set; }
        public string[] keyword { get; set; }
        public string link { get; set; }
        public string max_value { get; set; }
        public string merchant { get; set; }
        public string min_spend { get; set; }
        public string name { get; set; }
        public string prior_type { get; set; }
        public string remain { get; set; }
        public bool remain_true { get; set; }
        public string shop_id { get; set; }
        public DateTime start_time { get; set; }
        public int status { get; set; }
        public string time_left { get; set; }
    }
    
    public class Coupon_Item
    {

        public string coupon_code { get; set; }
        public string coupon_desc { get; set; }
        public string coupon_save { get; set; }
        public string coupon_type { get; set; }
    }
    public class category
    { 
        public string merchant { get; set; }
        public string category_name { get; set; }
        public string category_name_show { get; set; }
        public string category_no { get; set; }
        public string slug { get; set; }
        public string image { get; set; }
    }
}