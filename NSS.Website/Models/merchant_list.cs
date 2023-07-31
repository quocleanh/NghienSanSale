using System.Collections.Generic;
namespace NSS.Website.Models
{
    public class merchant_list
    {
        public List<merchant> data { get; set; }
    }
    public class merchant
    {

        public List<string> display_name { get; set; }
        public string id { get; set; }
        public string login_name { get; set; }
        public string logo { get; set; }
        public string total_offer { get; set; }

    }
    public class merchant_db
    {

        public string display_name { get; set; }
        public string id { get; set; }
        public string login_name { get; set; }
        public string logo { get; set; }
        public string total_offer { get; set; }

    }
}
