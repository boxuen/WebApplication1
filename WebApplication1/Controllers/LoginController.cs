using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private NEW2101Entities1 db = new NEW2101Entities1();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Account, string Password)
        {
            var existingUser = db.AccountData.FirstOrDefault(u => u.Account == Account && u.Password == Password);

            if (existingUser != null)
            {
                Session["Username"] = existingUser.Username;

                if (existingUser.Role_ID == 1) // Admin
                {
                    Debug.WriteLine("User is an Admin.");
                    return RedirectToAction("AdminDashboard", "Home");
                }
                else if (existingUser.Role_ID == 2) // Manager
                {
                    Debug.WriteLine("User is a Manager.");
                    return RedirectToAction("ManagerDashboard", "Home");
                }
                else if (existingUser.Role_ID == 3) // Player
                {
                    Debug.WriteLine("User is a Player.");
                    return RedirectToAction("PlayerDashboard", "Home");
                }
            }

            ModelState.AddModelError("error", "無效的用戶名或密碼");
            return View("Index");
        }
    }
}