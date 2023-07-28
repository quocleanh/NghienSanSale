using System.Web.Mvc;
using System.Web.Routing;

namespace NSS.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "DirectLink",
               url: "direct/{url}",
               defaults: new { controller = "Base", action = "DirectLink", url = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "News",
               url: "blog-khuyen-mai",
               defaults: new { controller = "News", action = "Index", slug = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "NewsCategory",
               url: "blog-khuyen-mai/{category}",
               defaults: new { controller = "News", action = "NewsCategories", category = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "NewsDetail",
               url: "blog-khuyen-mai/{category}/{slug}",
               defaults: new { controller = "News", action = "Detail" }
           );
            routes.MapRoute(
               name: "Category",
               url: "ma-giam-gia/{brand}",
               defaults: new { controller = "Category", action = "Index", brand = UrlParameter.Optional }
           );
           // routes.MapRoute(
           //    name: "Brand",
           //    url: "thuong-hieu/{brand}",
           //    defaults: new { controller = "Brand", action = "Index", brand = UrlParameter.Optional }
           //);
            routes.MapRoute(
               name: "SearchVoucher",
               url: "tim-ma-giam-gia",
               defaults: new { controller = "Search", action = "SearchCoupons", url = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
