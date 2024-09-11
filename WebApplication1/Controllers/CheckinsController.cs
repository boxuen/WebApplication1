using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CheckinsController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();

        // GET: Checkins
        public ActionResult Index()
        {
            var checkin = db.Checkin.Include(c => c.AccountData);

            return View(checkin.ToList());
            
            
        }

        // account 抓資料



        // GET: Checkins/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkin checkin = db.Checkin.Find(id);
            if (checkin == null)
            {
                return HttpNotFound();
            }
            return View(checkin);
        }

        // GET: Checkins/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.AccountData, "ID", "Account");
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            return View();
        }

        // POST: Checkins/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Account,Work_Date,Getoff_Date,time,Note")] Checkin checkin)
        {
            if (ModelState.IsValid)
            {
                db.Checkin.Add(checkin);
               // Session["msg"] = "此筆紀錄已建立成功......."
                db.SaveChanges();
                ViewBag.msg = "此筆紀錄已建立成功........";
                return RedirectToAction("Index");
            }

            ViewBag.Account = new SelectList(db.AccountData, "ID", "Account", checkin.Account);
            return View(checkin);
        }

        // GET: Checkins/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkin checkin = db.Checkin.Find(id);
            if (checkin == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account = new SelectList(db.AccountData, "ID", "Account", checkin.Account);
            return View(checkin);
        }

        // POST: Checkins/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Account,Work_Date,Getoff_Date,time,Note")] Checkin checkin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkin).State = EntityState.Modified;
                db.SaveChanges();
                //Session["msg"] = "此筆紀錄已更新成功.......";
                ViewBag.msg = "此筆紀錄已更新成功.........";
                return RedirectToAction("Index");
            }
            ViewBag.Account = new SelectList(db.AccountData, "ID", "Account", checkin.Account);
            
            return View(checkin);
        }

        // GET: Checkins/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checkin checkin = db.Checkin.Find(id);
            if (checkin == null)
            {
                return HttpNotFound();
            }
            return View(checkin);
        }

        // POST: Checkins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Checkin checkin = db.Checkin.Find(id);
            db.Checkin.Remove(checkin);
            db.SaveChanges();
           // Session["msg"] = "此筆紀錄已刪除成功.......";
            ViewBag.msg = "此筆紀錄已刪除成功.......";
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
            long latestFormNumber = db.Checkin.Max(f => ((long)f.ID));

            return latestFormNumber + 1;

        }
        public ActionResult End()
        {
            Session["Time"] = DateTime.Now.ToString();
            //ViewBag.Time = DateTime.Now.ToString();
            //return RedirectToAction("Edit");
            ViewBag.show = "此筆單號下班時間為：" + Session["Time"] + "請按編輯將資料送出。";
            return RedirectToAction("Index");
        }



    }
}
