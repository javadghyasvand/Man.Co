using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InsertShowImage;
using MySite.Models.Model;
using EntityState = System.Data.Entity.EntityState;

namespace MySite.Areas.Admin.Controllers
{
    public class AboutController : Controller
    {
        private readonly MySiteDataEntities _db = new MySiteDataEntities();

        // GET: Admin/About
        public ActionResult Index()
        {
            return View(_db.About.SingleOrDefault());
        }

        // GET: Admin/About/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            About about = _db.About.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }

            return View(about);
        }

        // GET: Admin/About/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/About/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Link,FileName,Description,FileTitle")] About about,
            HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    about.FileName =
                        Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);
                    fileUpload.SaveAs(Server.MapPath("/images/Videos/" + about.FileName));
                }
                else
                {
                    ModelState.AddModelError("FileName", "لطفا فایل را انتخاب کنید .");
                    return View(about);
                }
                _db.About.Add(about);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(about);
        }

        // GET: Admin/About/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            About about = _db.About.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }

            return View(about);
        }

        // POST: Admin/About/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Link,FileName,Description,FileTitle")] About about, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    System.IO.File.Delete(Server.MapPath("/images/VideoProject/" + about.FileName));
                        about.FileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);
                        fileUpload.SaveAs(Server.MapPath("/images/VideoProject/" + about.FileName));
                }
                _db.Entry(about).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
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