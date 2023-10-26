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
        readonly MySiteDataEntities _db = new MySiteDataEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            var about = _db.About.SingleOrDefault();
            return PartialView(about);
        }
        public ActionResult Groups()
        {
            return PartialView(_db.ProjectGroups.ToList());
        }
        // public ActionResult Blog()
        // {
        //     return PartialView();
        // }

       
        public ActionResult Portfolio()
        {

            return  PartialView(_db.Project.OrderByDescending(P=>P.Id).ToList().Take(8));
        }
        public ActionResult Contact()
        {
            return PartialView();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}