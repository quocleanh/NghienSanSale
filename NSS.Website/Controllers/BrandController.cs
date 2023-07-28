using Newtonsoft.Json;
using NSS.Website.Bussiniess;
using NSS.Website.Models;
using NSS.Wesite.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NSS.Website.Controllers
{
    public class BrandController : BaseController
    {
        // GET: Brand
        public async Task<ActionResult> Index(string brand = "", string category = "")
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
        
    }
}