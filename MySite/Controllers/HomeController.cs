using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySite.Models.Model;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        MySiteDataEntities _db = new MySiteDataEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Navbar()
        {
            return View();
        }

        public ActionResult About()
        {
            var about = _db.About.SingleOrDefault();
            return PartialView(about);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Groups()
        {
            return PartialView(_db.ProjectGroups.ToList());
        }

        public ActionResult Blog()
        {
            return PartialView();
        }

        public ActionResult Portfolio()
        {
            return  PartialView(_db.Project.ToList().Take(8));
        }
    }
}