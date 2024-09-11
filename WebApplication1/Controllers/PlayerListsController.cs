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
    public class PlayerListsController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();

        // GET: PlayerLists
        public ActionResult Index()
        {
            var playerList = db.PlayerList.Include(p => p.AccountData).Include(p => p.TeamList);
            return View(playerList.ToList());
        }

        // GET: PlayerLists/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerList playerList = db.PlayerList.Find(id);
            if (playerList == null)
            {
                return HttpNotFound();
            }
            return View(playerList);
        }

        // GET: PlayerLists/Create
        public ActionResult Create()
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            //ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Account");
            ViewBag.Account_ID = new SelectList(db.AccountData.Where(a => a.Role_ID == 3), "ID", "Username");
            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name");
            ViewBag.JoinTime = DateTime.Now;
            return View();
        }

        // POST: PlayerLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Account_ID,Age,Sex,Address,Skill,Exp,Join_date,Team_ID,Image,Note")] PlayerList playerList)
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            if (ModelState.IsValid)
            {
                db.PlayerList.Add(playerList);
                db.SaveChanges();
                Session["msg"] = "資料新增成功....表單編號：" + ViewBag.ID;

                return RedirectToAction("Index");
            }

            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Account", playerList.Account_ID);
            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name", playerList.Team_ID);
            return View(playerList);
        }

        // GET: PlayerLists/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerList playerList = db.PlayerList.Find(id);
            if (playerList == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Username", playerList.Account_ID);
            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name", playerList.Team_ID);
            return View(playerList);
        }

        // POST: PlayerLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Account_ID,Age,Sex,Address,Skill,Exp,Join_date,Team_ID,Image,Note")] PlayerList playerList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerList).State = EntityState.Modified;
                db.SaveChanges();
                Session["msg"] = "此筆資料已更新成功....！！";

                return RedirectToAction("Index");
            }
            ViewBag.Account_ID = new SelectList(db.AccountData, "ID", "Account", playerList.Account_ID);
            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name", playerList.Team_ID);
            return View(playerList);
        }

        // GET: PlayerLists/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerList playerList = db.PlayerList.Find(id);
            if (playerList == null)
            {
                return HttpNotFound();
            }
            return View(playerList);
        }

        // POST: PlayerLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PlayerList playerList = db.PlayerList.Find(id);
            db.PlayerList.Remove(playerList);
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
            long latestFormNumber = db.PlayerList.Max(f => ((long)f.ID));

            return latestFormNumber + 1;

        }
    }
}
