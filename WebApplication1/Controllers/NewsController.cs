using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NewsController : Controller
    {

        
        
        private NEW2101Entities1 db = new NEW2101Entities1();
       
        // GET: News
        public ActionResult Index()
        {
            string path = Request.Url.AbsolutePath;
           // Console.WriteLine(path);
            
            var news = db.News.Include(n => n.AccountData);
            return View(news.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {



            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Username");
            return View();
        }

        // POST: News/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Creat_Time,Account_ID,Category,Address,Contant,Upload")] News news)
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Username", news.Account_ID);
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(long? id)
        {
            //long newFormNumber = GenerateNewFormNumber();
            //ViewBag.ID = newFormNumber;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Username", news.Account_ID);
            return View(news);
        }

        // POST: News/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Creat_Time,Account_ID,Category,Address,Contant,Upload")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Username", news.Account_ID);
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long? id)
        {
            
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            //ViewBag.Success = "此筆資料已刪除成功....！!";
            Session["Success"] = "此筆資料已刪除成功....！！";
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
        public long GenerateNewFormNumber()
        {
            // 實作你的生成單號的邏輯，可以使用資料庫查詢來獲取下一個可用的單號
            // 這只是一個示例，實際實現會根據你的需求而定
            long latestFormNumber = db.News.Max(f => ((long)f.ID));
           
            return latestFormNumber + 1;

        }

       

    }
}
