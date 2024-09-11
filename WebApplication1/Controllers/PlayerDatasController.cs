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
    public class PlayerDatasController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();

        // GET: PlayerDatas
        public ActionResult Index()
        {

            var playerData = db.PlayerData.Include(p => p.PlayerList);
            return View(playerData.ToList());
        }

        // GET: PlayerDatas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerData playerData = db.PlayerData.Find(id);
            if (playerData == null)
            {
                return HttpNotFound();
            }
            return View(playerData);
        }

        // GET: PlayerDatas/Create
        public ActionResult Create()
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            ViewBag.Player_ID = new SelectList(db.PlayerList, "ID", "Account_ID");
            return View();
        }

        // POST: PlayerDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Player_ID,MVP,KDA,出場次數,總擊殺,總助攻,總死亡,場均金錢,場均補刀,場均差眼,場均排眼,場均參圖率,場均對位經濟差,傷害占比,經濟占比")] PlayerData playerData)
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            if (ModelState.IsValid)
            {
                db.PlayerData.Add(playerData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Player_ID = new SelectList(db.PlayerList, "ID", "Account_ID", playerData.Player_ID);
            return View(playerData);
        }

        // GET: PlayerDatas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerData playerData = db.PlayerData.Find(id);
            if (playerData == null)
            {
                return HttpNotFound();
            }
            ViewBag.Player_ID = new SelectList(db.PlayerList, "ID", "Account_ID", playerData.Player_ID);
            return View(playerData);
        }

        // POST: PlayerDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Player_ID,MVP,KDA,出場次數,總擊殺,總助攻,總死亡,場均金錢,場均補刀,場均差眼,場均排眼,場均參圖率,場均對位經濟差,傷害占比,經濟占比")] PlayerData playerData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Player_ID = new SelectList(db.PlayerList, "ID", "Account_ID", playerData.Player_ID);
            return View(playerData);
        }

        // GET: PlayerDatas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerData playerData = db.PlayerData.Find(id);
            if (playerData == null)
            {
                return HttpNotFound();
            }
            return View(playerData);
        }

        // POST: PlayerDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PlayerData playerData = db.PlayerData.Find(id);
            db.PlayerData.Remove(playerData);
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
        public long GenerateNewFormNumber()
        {
            // 實作你的生成單號的邏輯，可以使用資料庫查詢來獲取下一個可用的單號
            // 這只是一個示例，實際實現會根據你的需求而定
            long latestFormNumber = db.PlayerData.Max(f => ((long)f.ID));

            return latestFormNumber + 1;

        }
    }
}
