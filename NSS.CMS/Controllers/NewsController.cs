using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NSS.CMS.Models;
using System.IO;

namespace NSS.CMS.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public async Task<ActionResult> Index()
        {
            return View(await db.News.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            News news = new News();
            return View(news);
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CategoryID,Title,Description,Content,Images,MetaTitle,MetaKeyword,MetaDescription,Slug,LinkAffiliate")] News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<ActionResult> Edit([Bind(Include = "ID,CategoryID,Title,Description,Images,Content,MetaTitle,MetaKeyword,MetaDescription,Slug,LinkAffiliate")] News news, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                //file.SaveAs(path);

                db.Entry(news).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
