using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeamListsController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();

        // GET: TeamLists
        public ActionResult Index()
        {
            var teamList = db.TeamList.Include(t => t.AccountData);
            return View(teamList.ToList());
        }

        // GET: TeamLists/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamList teamList = db.TeamList.Find(id);
            if (teamList == null)
            {
                return HttpNotFound();
            }
            return View(teamList);
        }

        // GET: TeamLists/Create
        public ActionResult Create()
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            //ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Account");
            ViewBag.Account_ID = new SelectList(db.AccountData.Where(a => a.Role_ID == 2), "ID", "Username");
            return View();
        }

        // POST: TeamLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Team_name,Account_ID,Note")] TeamList teamList)
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            if (ModelState.IsValid)
            {
                db.TeamList.Add(teamList);
                db.SaveChanges();
                Session["msg"] = "資料新增成功....表單編號：" + ViewBag.ID;

                return RedirectToAction("Index");
            }

            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Account", teamList.Account_ID);
            return View(teamList);
        }

        // GET: TeamLists/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamList teamList = db.TeamList.Find(id);
            if (teamList == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account_ID = new SelectList(db.AccountData.Where(a => a.Role_ID == 2), "ID", "Username",teamList.Account_ID);
            return View(teamList);
        }

        // POST: TeamLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Team_name,Account_ID,Note")] TeamList teamList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamList).State = EntityState.Modified;
                db.SaveChanges();
                Session["msg"] = "此筆資料已更新成功....！！";

                return RedirectToAction("Index");
            }
            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Account", teamList.Account_ID);
            return View(teamList);
        }

        // GET: TeamLists/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamList teamList = db.TeamList.Find(id);
            if (teamList == null)
            {
                return HttpNotFound();
            }
            return View(teamList);
        }

        // POST: TeamLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TeamList teamList = db.TeamList.Find(id);
            db.TeamList.Remove(teamList);
            db.SaveChanges();
            Session["msg"] = "此筆資料已刪除成功....！！";
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
            long latestFormNumber = db.TeamList.Max(f => ((long)f.ID));

            return latestFormNumber + 1;

        }
    }
}
