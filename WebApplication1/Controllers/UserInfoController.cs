using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserInfoController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();
        public class Password
        {
            public string CheckPassword { get; set; }
        }

        // GET: AccountDatas
        public ActionResult Index()
        {
           
            var accountData = db.AccountData.Include(a => a.Role);
           
            
            return View(accountData.ToList());
        }

        // GET: AccountDatas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountData accountData = db.AccountData.Find(id);
            if (accountData == null)
            {
                return HttpNotFound();
            }
            return View(accountData);
        }

        // GET: AccountDatas/Create
        public ActionResult Create()
        {
            long newFormNumber = GenerateNewFormNumber();
            ViewBag.ID = newFormNumber;
            ViewBag.Role_ID = new SelectList(db.Role, "ID", "Role1");
            return View();
        }

        // POST: AccountDatas/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Account,Username,Password,E_mail,Phone,Role_ID,Static,Creat,Logtime,Note")] AccountData accountData)
        {
            if (ModelState.IsValid)
            {
                long newFormNumber = GenerateNewFormNumber();
                ViewBag.ID = newFormNumber;
                db.AccountData.Add(accountData);
                db.SaveChanges();
                //ModelState.AddModelError("系統通知", "此筆資料新增成功!!");
                Session["msg"] = "資料已新增成功!!";
                return RedirectToAction("Index");
            }

            ViewBag.Role_ID = new SelectList(db.Role, "ID", "Role1", accountData.Role_ID);
            return View(accountData);
        }

        // GET: AccountDatas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountData accountData = db.AccountData.Find(id);
            if (accountData == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_ID = new SelectList(db.Role, "ID", "Role1", accountData.Role_ID);
            return View(accountData);
        }

        // POST: AccountDatas/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,E_mail,Phone")] AccountData accountData)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing record from the database
                var existingAccountData = db.AccountData.Find(accountData.ID);
                if (existingAccountData != null)
                {
                    // Update only the specified fields
                    existingAccountData.Username = accountData.Username;
                    existingAccountData.Password = accountData.Password;
                    existingAccountData.E_mail = accountData.E_mail;
                    existingAccountData.Phone = accountData.Phone;

                    // Mark the entity as modified
                    db.Entry(existingAccountData).State = EntityState.Modified;

                    // Save changes to the database
                    db.SaveChanges();
                    Session["msg"] = "資料已更新成功!!";
                    return RedirectToAction("Index", "Login");
                }
            }
            ViewBag.Role_ID = new SelectList(db.Role, "ID", "Role1", accountData.Role_ID);
            return View(accountData);
        }

       
        // GET: AccountDatas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountData accountData = db.AccountData.Find(id);
            if (accountData == null)
            {
                return HttpNotFound();
            }
            return View(accountData);
        }

        // POST: AccountDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AccountData accountData = db.AccountData.Find(id);
            db.AccountData.Remove(accountData);
            db.SaveChanges();
            Session["msg"] = "資料已刪除成功!!";
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
            long latestFormNumber = db.AccountData.Max(f => ((long)f.ID));

            return latestFormNumber + 1;

        }

    }
}
