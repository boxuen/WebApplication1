using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class STUCheckinsViewController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();

        // GET: CheckinsView
        public ActionResult Index()
        {
            var checkin = db.Checkin.Include(c => c.AccountData);
            return View(checkin.ToList());
        }

        // GET: CheckinsView/Details/5
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

        // GET: CheckinsView/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.AccountData, "ID", "Account");
            return View();
        }

        // POST: CheckinsView/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Account,Work_Date,Getoff_Date,time,Note")] Checkin checkin)
        {
            if (ModelState.IsValid)
            {
                db.Checkin.Add(checkin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account = new SelectList(db.AccountData, "ID", "Account", checkin.Account);
            return View(checkin);
        }

        // GET: CheckinsView/Edit/5
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

        // POST: CheckinsView/Edit/5
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
                return RedirectToAction("Index");
            }
            ViewBag.Account = new SelectList(db.AccountData, "ID", "Account", checkin.Account);
            return View(checkin);
        }

        // GET: CheckinsView/Delete/5
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

        // POST: CheckinsView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Checkin checkin = db.Checkin.Find(id);
            db.Checkin.Remove(checkin);
            db.SaveChanges();
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
