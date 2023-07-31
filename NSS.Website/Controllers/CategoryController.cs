using Extension;
using Newtonsoft.Json;
using NSS.Website.Bussiniess;
using NSS.Website.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NSS.Website.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        public ActionResult Index(string brand = "", string category = "")
        {
            ViewBag.Brand = brand;

            List<merchant_db> merchant_list = new CommonRepository().GetListMerchant();
            merchant_db curr = new merchant_db();
            List<category> categories = new CategotyRepository().GetAll();
            ViewBag.Categorie = categories;
            if (merchant_list != null && merchant_list.Count > 0)
                curr = merchant_list.Where(x => x.display_name.ToLower() == brand.ToLower()).FirstOrDefault();
            string desc = "Danh mục tổng hợp mã giảm giá " + brand + ", coupon, voucher " + brand + " mới nhất trong tháng. Giúp bạn tổng hợp những mã giảm giá @brand, code " + brand + " nhanh nhất, đầy đủ nhất, tất cả đều vẫn còn sử dụng được.Từ nay, khi mua hàng " + brand + " bạn nên ghé thăm nghiensansale và sau đó lấy mã khuyến mãi " + brand;
            if (curr != null)
            {
                SetSEO("Mã Giảm Giá " + brand + ", Coupon, Khuyến Mãi Tháng " + DateTime.Now.ToString("MM/yyyy"), curr.logo, desc, "", "");
            }
            else
            {
                SetSEO("Mã Giảm Giá " + brand + ", Coupon, Khuyến Mãi Tháng " + DateTime.Now.ToString("MM/yyyy"), "", desc, "", "");
            }
            return View();
        }

        public async Task<ActionResult> _Filter(string brand = "", string category = "", int page = 1, List<string> categories = null, List<int> categoriesMonth = null)
        {
            try
            {
                List<merchant_db> merchant_list = new CommonRepository().GetListMerchant();
                merchant_db merchant_ = merchant_list.Where(x => x.display_name == brand).FirstOrDefault();
                //https://api.accesstrade.vn/v1/offers_informations/coupon?is_next_day_coupon=False&merchant=4742147753565840242&
                Coupons coupon;
                var parameters = string.Empty;
                if(merchant_ != null)
                {
                    parameters = $"&merchant={merchant_.id}";
                }
                if(!string.IsNullOrWhiteSpace(category))
                {
                    parameters += $"&categories={category}";
                }

                #region Xử lý filter nhóm theo tháng

                if (categoriesMonth != null && categoriesMonth.Count > 0)
                {
                    var daysMax = categoriesMonth.Max();
                    var startDate = DateTime.Now.ToStartDate();
                    var endDate = startDate.AddDays(daysMax).ToEndDate();
                    parameters += $"&start_time={startDate.ToTimeStamp()}&end_time={endDate.ToTimeStamp()}";
                     
                }

                #endregion

                var client = new RestClient(api_endpoint + $"offers_informations/coupon?is_next_day_coupon=False{parameters}&page={page}")
                {
                    Timeout = -1
                };

                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", api_token);
                request.AddHeader("Content-Type", "application/json");
                request.AlwaysMultipartFormData = true;
                IRestResponse response = await client.ExecuteTaskAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = response.Content;
                    coupon = JsonConvert.DeserializeObject<Coupons>(result);
                    coupon.column = 2;
                    ViewBag.Coupons = coupon;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return PartialView("~/Views/Shared/Partial/_Coupons.cshtml");
        }
    }
}