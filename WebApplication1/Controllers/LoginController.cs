using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            

            //ViewBag.Title = "JJHu out";

            if (existingUser != null)
            {
                FormsAuthentication.SetAuthCookie(existingUser.Account, false);
                Session["Username"] = existingUser.Username;
                Session["User_ID"] = existingUser.ID;
                Session["Role_ID"] = existingUser.Role_ID;
                Session["Logtime"] = DateTime.Now.ToString();
                

                // Session["Account"] = existingUser.Account;

                //


                //var Username = existingUser.Username;
                //ViewBag.Username = existingUser.Username;
                //Console.WriteLine(ViewBag.Username);
                if (existingUser.Role_ID == 1) // Admin
                {
                   
                    
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


            ModelState.AddModelError("", "帳號或密碼輸入錯誤，請重新輸入！！");
            return View("Index");



        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}