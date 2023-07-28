using Newtonsoft.Json;
using NSS.Website.Bussiniess;
using NSS.Website.Models;
using NSS.Wesite.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace NSS.Website.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.IsHome = "1";
            List<category> categories = new CategotyRepository().GetAll();
            ViewBag.Categorie = categories;
            //(List<category>)categories
            SetSEO("Săn sale Shopee Lazada Tiki và hàng ngàn ưu đãi", "", "", "", "");
            return View();
        }
        public async Task<ActionResult> _Coupons()
        {
            try
            {
                Coupons coupon;
                var client = new RestClient(api_endpoint + "offers_informations/coupon?limit=20")
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
        public async Task<PartialViewResult> _Merchant()
        {
            //merchant_list merchant_list;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api_endpoint);
                client.DefaultRequestHeaders.Add("Authorization", api_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("offers_informations/merchant_list");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    merchant_list batchJson = JsonConvert.DeserializeObject<merchant_list>(result);
                    ViewBag.merchant_list = batchJson;
                }
            }
            return PartialView("~/Views/Shared/Partial/_RightSidebar.cshtml");
        }
        public async Task<ActionResult> _Categoties()
        {
            try
            {
                Coupons coupon;
                var client = new RestClient(api_endpoint + "offers_informations?merchant=fahasa")
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
                    if (IsSyncMasterData)
                    {
                        // ko xoá
                        var categories = coupon.data[1].categories;
                        //int totalInsert = new CommonRepository().InsertCategories(categories, "fahasa");
                        ViewBag.Categorie = (List<category>)categories;
                        return View("~/Views/Shared/Partial/_Categories.cshtml", categories);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return View("~/Views/Shared/Partial/_Categories.cshtml", null);
        }
    }
}