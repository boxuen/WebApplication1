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
    public class TrainingPlansController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();

        // GET: TrainingPlans
        public ActionResult Index()
        {
            var trainingPlan = db.TrainingPlan.Include(t => t.TeamList);
            return View(trainingPlan.ToList());
        }

        // GET: TrainingPlans/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingPlan trainingPlan = db.TrainingPlan.Find(id);
            if (trainingPlan == null)
            {
                return HttpNotFound();
            }
            return View(trainingPlan);
        }

        // GET: TrainingPlans/Create
        public ActionResult Create()
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name");
            return View();
        }

        // POST: TrainingPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Project_Name,Target,Team_ID,Open_Date,End_Date,Schedule,CloseCase_Date")] TrainingPlan trainingPlan)
        {
            if (ModelState.IsValid)
            {
                db.TrainingPlan.Add(trainingPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name", trainingPlan.Team_ID);
            return View(trainingPlan);
        }

        // GET: TrainingPlans/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingPlan trainingPlan = db.TrainingPlan.Find(id);
            if (trainingPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name", trainingPlan.Team_ID);
            return View(trainingPlan);
        }

        // POST: TrainingPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Project_Name,Target,Team_ID,Open_Date,End_Date,Schedule,CloseCase_Date")] TrainingPlan trainingPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Team_ID = new SelectList(db.TeamList, "ID", "Team_name", trainingPlan.Team_ID);
            return View(trainingPlan);
        }

        // GET: TrainingPlans/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingPlan trainingPlan = db.TrainingPlan.Find(id);
            if (trainingPlan == null)
            {
                return HttpNotFound();
            }
            return View(trainingPlan);
        }

        // POST: TrainingPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TrainingPlan trainingPlan = db.TrainingPlan.Find(id);
            db.TrainingPlan.Remove(trainingPlan);
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
            long latestFormNumber = db.TrainingPlan.Max(f => ((long)f.ID));

            return latestFormNumber + 1;

        }
    }
}
