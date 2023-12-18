using DoAn_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            QLLKDataContext db = new QLLKDataContext();

            var prs = (from p in db.LINHKIENs select p).Take(5).ToList();
            ViewBag.Prs = prs;

            var loai = (from L in db.LOAILINHKIENs select L).ToList();
            Session["Loai"] = loai;

            ViewBag.checkHome = "show";
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
    }
}