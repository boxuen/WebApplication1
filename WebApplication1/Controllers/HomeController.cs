using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AdminDashboard()
        {
            return View(); // 這個操作方法對應到 Admin 儀表板頁面
        }

        public ActionResult ManagerDashboard()
        {
            return View(); // 這個操作方法對應到 Manager 儀表板頁面
        }
        public ActionResult PlayerDashboard()
        {
            return View(); // 這個操作方法對應到 Player 儀表板頁面
        }
    }
}