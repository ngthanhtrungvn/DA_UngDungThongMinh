using DoAn_Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web.Controllers
{
    public class ProductsController : Controller
    {
        readonly QLLKDataContext db = new QLLKDataContext();
        readonly int pageSize = 4;
        void ViewBagNoti(List<LINHKIEN> temp, int check, int page)
        {
            if (temp.Count() > 0)
            {
                int last = int.Parse(Math.Ceiling((double)temp.Count() / pageSize).ToString());
                ViewBag.last = last;
                ViewBag.noti = "Showing " + page + "-" + last + " of " + temp.Count() + " results";
                ViewBag.check = check;
            }
        }
        //alias
        public ActionResult Index(int alias, int page = 1)
        {
            var temp = db.LINHKIENs.Where(o=>o.MALOAI == alias).OrderByDescending(x => x.MALINHKIEN).ToList();
            var products = temp.ToPagedList(page, pageSize);
            var category = (from L in db.LOAILINHKIENs where L.MALOAI == alias select L).FirstOrDefault();
            ViewBag.alias = category.TENLOAI;
            ViewBagNoti(temp, 0, page);
            return View(products);
        }
        public ActionResult Detail(int alias)
        {
            var productdetail = new Productdetail
            {
                Product = (from L in db.LINHKIENs where L.MALINHKIEN == alias select L).FirstOrDefault()
            };
            productdetail.LstProducts_Categories = (from L in db.LINHKIENs where L.MALOAI == productdetail.Product.MALOAI select L).ToList();
            return View(productdetail);
        }
        public ActionResult Search(int page = 1)
        {
            try
            {
                
                string keyword = Request["tukhoa"].ToString().ToLower();
                var temp = (from A in db.LINHKIENs select A).Where(x => (x.LOAILINHKIEN.TENLOAI.ToLower().Contains(keyword)                
                || x.TENLINHKIEN.ToLower().Contains(keyword)               
                || x.MALINHKIEN.ToString().ToLower().Equals(keyword)
                )).OrderByDescending(x => x.MALINHKIEN).ToList();
                var products = temp.ToPagedList(page, pageSize);
                ViewBag.alias = "Tìm kiếm: " + keyword;
                ViewBagNoti(temp, 2, page);
                return View("Index", products);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
    }
}