using NSS.Website.Bussiniess;
using NSS.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NSS.Website.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult Index()
        {
            List<NewsCategory> newsCategories = new NewsRepository().GetAllCategories();
            ViewBag.NewsCategory = newsCategories;
            List<News> detail = new NewsRepository().GetAll();
            SetSEO("Blog săn sale", "", "", "Nghiện Săn Sale - Blog săn sale", "");
            return View(detail);
        }
        public ActionResult NewsCategories(string category = "")
        {
            List<NewsCategory> newsCategories = new NewsRepository().GetAllCategories();
            ViewBag.newsCategories = newsCategories;
            NewsCategory newsCategory = newsCategories.Where(x => x.Link == category).FirstOrDefault();
            if (newsCategories != null && newsCategory != null)
            {
                List<News> listNews = new NewsRepository().GetAll().Where(x => x.CategoryID == newsCategory.ID).ToList();
                ViewBag.listNews = listNews;
                SetSEO(newsCategory.CategoryName, newsCategory.Images, newsCategory.Description, "Nghiện Săn Sale - Blog săn sale", "");
            }
            return View(newsCategory);
        }
        public ActionResult Detail(string category, string slug = "")
        {
            if (!string.IsNullOrEmpty(slug))
            {
                List<NewsCategory> newsCategories = new NewsRepository().GetAllCategories();
                ViewBag.newsCategories = newsCategories;
                News detail = new NewsRepository().GetBySlug(slug);
                if (detail != null)
                {
                    SetSEO(detail.MetaTitle, detail.Images, detail.MetaDescription, "Nghiện Săn Sale - Blog săn sale", detail.MetaKeyword);

                }
                return View(detail);
            }
            return View();
        }
    }
}