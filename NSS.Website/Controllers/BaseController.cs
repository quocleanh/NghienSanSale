using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NSS.Website.Controllers
{
    public class BaseController : Controller
    {
        public string api_token = "Token lU1TGl1fU_iNvJ2OzPZ8qVhfHF9pr9im";
        public string api_endpoint = "https://api.accesstrade.vn/v1/";
        public bool IsSyncMasterData = Boolean.Parse(ConfigurationManager.AppSettings["IsSyncMasterData"]);

        public ActionResult DirectLink(string url)
        {
            return Redirect(url);
        }

        public string SeoTitle
        {
            get
            {
                return ViewBag.SeoTitle;
            }
            set
            {
                ViewBag.SeoTitle = value;
            }
        }

        public string SeoImage
        {
            get
            {
                return ViewBag.SeoImage;
            }
            set
            {
                ViewBag.SeoImage = value;
            }
        }

        public string SeoDescription
        {
            get
            {
                return ViewBag.SeoDescription;
            }
            set
            {
                ViewBag.SeoDescription = value;
            }
        }

        public string SeoUrl
        {
            get
            {
                return ViewBag.SeoUrl;
            }
            set
            {
                ViewBag.SeoUrl = value;
            }
        }

        public string SeoSiteName
        {
            get
            {
                if (ViewBag.SeoSiteName == null)
                {
                    ViewBag.SeoSiteName = "https://nghiensansale.vn/";
                }
                return ViewBag.SeoSiteName;
            }
            set
            {
                ViewBag.SeoSiteName = value;
            }
        }

        public string SeoKeywords
        {
            get
            {
                return ViewBag.SeoKeywords;
            }
            set
            {
                ViewBag.SeoKeywords = value;
            }
        }

        public string ScriptPixelTracking
        {
            get
            {
                return ViewBag.PixelTracking;
            }
            set
            {
                ViewBag.PixelTracking = value;
            }
        }
        public BaseController()
        {
            ViewBag.IsHome = "0";
            ViewBag.IsSearch = false;
            this.SeoTitle = "Nghiện Săn Sale";
            this.SeoUrl = "https://nghiensansale.vn/";
            this.SeoSiteName = "Nghiện Săn Sale";
            this.SeoImage = "https://nghiensansale.vn/Content/UI/img/bg-meta.jpg?v=20221224";
            this.SeoDescription = "Tìm kiếm, thu thâp Mã Giảm Giá, chương trình Khuyến Mãi, Coupon, Voucher từ các sàn TMDT Tiki Shopee Lazada, mua hàng trực tuyến, dịch vụ online....";
            this.SeoKeywords = "";
            this.ScriptPixelTracking = "fbq('track', 'ViewContent');";
        }
        protected void SetSEO(string seoTitle, string seoImage, string seoDescription, string seoSiteName, string seoKeywords)
        {
            this.SeoUrl = "https://nghiensansale.vn/";
            if (Request != null && Request.Url != null)
            {
                this.SeoUrl = Request.Url.AbsoluteUri;
            }
            if (!string.IsNullOrEmpty(seoTitle))
            {
                this.SeoTitle = seoTitle; 
            }
            if (!string.IsNullOrEmpty(seoImage))
            {
                seoImage = seoImage.Replace("//", "/");
                seoImage = seoImage.Replace("http:/", "http://");
                this.SeoImage = seoImage;
            }
            if (!string.IsNullOrEmpty(seoDescription))
            {
                this.SeoDescription = seoDescription;
            }
            if (!string.IsNullOrEmpty(seoSiteName))
            {
                this.SeoSiteName = seoSiteName;
            }
            if (!string.IsNullOrEmpty(seoKeywords))
            {
                this.SeoKeywords = seoKeywords;
            }
        }

    }
}