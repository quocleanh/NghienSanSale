using HtmlAgilityPack;
using Newtonsoft.Json;
using NSS.Website.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NSS.Website.Controllers
{
    public class SearchController : BaseController
    {
        // GET: SearchCoupon
        public PartialViewResult Index()
        {
            return PartialView();
        }
        public ActionResult SearchCoupons(string url = "")
        {
            string titleDiv = "";
            Product product = new Product();
            if (!string.IsNullOrEmpty(url))
            {
                if (url.Contains("shopee.vn"))
                {
                    Uri strUrl = new Uri(url);
                    titleDiv = HttpUtility.UrlDecode(strUrl.AbsolutePath);
                    titleDiv = titleDiv.Replace("-", " ").Split('.')[0].Trim('/').Trim('i');
                    product.ProductName = titleDiv;
                    product.Link = url;
                    //product.Image = "data:image/png;base64,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";
                    product.Image= "https://nghiensansale.vn/Content/images/logo-black.png";
                    product.Price = "Đang cập nhật";
                    product.PriceDiscount = "Đang cập nhật";
                    product.Merchant = "shopee.vn";

                }
                else if (url.Contains("tiki.vn"))
                {

                    WebClient clientx = new WebClient();
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string resource = clientx.DownloadString(url);
                    HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                    html.LoadHtml(resource);
                    //ProductName
                    string node = "//*[@id='__next']/div[1]/main/div[3]/div[1]/div[3]/div[1]/h1";
                    product.ProductName = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerText));
                    //Price
                    node = "//*[@id='__next']/div[1]/main/div[3]/div[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div/div[2]";
                    try
                    {

                        product.Price = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerText));
                    }
                    catch (Exception)
                    {
                        node = "//*[@id='__next']/div[1]/main/div[3]/div[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div/div[1]";
                        product.Price = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerText));

                    }
                    //Price Sale
                    node = "//*[@id='__next']/div[1]/main/div[3]/div[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div/div[1]";
                    product.PriceDiscount = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerText));
                    //Price Image
                    node = "//*[@id='__next']/div[1]/main/div[3]/div[1]/div[1]/div[1]/div[1]/div/div/div/picture/img";
                    product.Image = Regex.Match(Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).OuterHtml)), "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; ;// Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).OuterHtml));
                    //Price Desc
                    node = "//*[@id='__next']/div[1]/main/div[3]/div[3]/div[1]/div[1]/div/table";
                    product.Description = "";// Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerHtml));
                    product.Merchant = "tiki.vn";
                    product.Link = url;
                }
                else if (url.Contains("lazada.vn"))
                {
                    WebClient clientx = new WebClient();
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string resource = clientx.DownloadString(url);
                    HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                    html.LoadHtml(resource);
                    string node = "//*[@id='module_product_title_1']/div/div/h1";
                     //ProductName
                    node = "//*[@id='module_product_title_1']/div/div/h1";
                    product.ProductName = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerText));
                    //Price
                    node = "//*[@id='module_product_price_1']/div/div/span";
                    product.Price = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerText));
                    //Price Sale
                    node = "//*[@id='module_product_price_1']/div/div/div/span[1]";
                    product.PriceDiscount = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerText));
                    //Price Image
                    node = "//*[@id='module_item_gallery_1']/div/div[1]/div[1]/img";
                    product.Image = Regex.Match(Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).OuterHtml)), "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value; ;// Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).OuterHtml));
                    //Price Desc
                    node = "//*[@id='__next']/div[1]/main/div[3]/div[3]/div[1]/div[1]/div/table";
                    product.Description = "";// Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(node).InnerHtml));
                    product.Merchant = "lazada.vn";
                    product.Link = url;
                }

                SetSEO( product.ProductName, product.Image, "Tìm mã giảm giá " + product.ProductName + ", xem thêm nhiều mã giảm giá, voucher, cuopon "+ product.Merchant + " tại nghiensansale.vn", "", "");
                ViewBag.Product = product;
            }
            return View();
        }

        public async Task<ActionResult> _SearchCoupon(string url)
        {
            Coupons coupon;
            var client = new RestClient(api_endpoint + "offers_informations/coupon?url=" + url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", api_token);
            request.AddHeader("Content-Type", "application/json");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = await client.ExecuteTaskAsync(request);


            if (url.Contains("shopee.vn"))
            {
                Uri strUrl = new Uri(url);
                string titleDiv = HttpUtility.UrlDecode(strUrl.AbsolutePath);
                titleDiv = titleDiv.Replace("-", " ").Split('.')[0].Trim('/').Trim('i');
                ViewBag.ProductName = titleDiv;
            }
            else if (url.Contains("tiki.vn"))
            {
                string titleDiv = "";
                WebClient clientx = new WebClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string resource = clientx.DownloadString(url);
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.LoadHtml(resource);
                string nodeTitle = "//*[@id='__next']/div[1]/main/div[3]/div[1]/div[3]/div[1]/h1";
                titleDiv = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(nodeTitle).InnerText));
                ViewBag.ProductName = titleDiv;
            }
            else if (url.Contains("lazada.vn"))
            {
                string titleDiv = "";
                WebClient clientx = new WebClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string resource = clientx.DownloadString(url);
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.LoadHtml(resource);
                string nodeTitle = "//*[@id='module_product_title_1']/div/div/h1";
                titleDiv = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html.DocumentNode.SelectSingleNode(nodeTitle).InnerText));
                ViewBag.ProductName = titleDiv;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = response.Content;
                coupon = JsonConvert.DeserializeObject<Coupons>(result);
                ViewBag.Coupons = coupon;
            }
            ViewBag.IsSearch = true;
            return PartialView("~/Views/Shared/Partial/_Coupons.cshtml");
        }


    }
}