using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KooyWebApp_MVC.Classes;
using System.Web.UI.WebControls;
using MySite.Models.Model;
using EntityState = System.Data.Entity.EntityState;

namespace MySite.Areas.Admin.Controllers
{
    public class ProjectGroupsController : Controller
    {
        private MySiteDataEntities db = new MySiteDataEntities();

        // GET: Admin/ProjectGroups
        public ActionResult Index()
        {
            return View(db.ProjectGroups.ToList());
        }


        // GET: Admin/ProjectGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProjectGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupName,GroupImage,GroupDescription")] ProjectGroups projectGroups,
            HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.IsImage())
                {
                    projectGroups.GroupImage =
                        Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    imageFile.SaveAs(Server.MapPath("/images/ImageGroups/" + projectGroups.GroupImage));
                }
                else
                {
                    ModelState.AddModelError("GroupImage", "لطفا فایل را انتخاب کنید .");
                    return View(projectGroups);
                }

                db.ProjectGroups.Add(projectGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectGroups);
        }

        // GET: Admin/ProjectGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectGroups projectGroups = db.ProjectGroups.Find(id);
            if (projectGroups == null)
            {
                return HttpNotFound();
            }

            return View(projectGroups);
        }

        // POST: Admin/ProjectGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public ActionResult Edit([Bind(Include = "Id,GroupName,GroupImage,GroupDescription")] ProjectGroups projectGroups, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
               
                    if (imageFile != null && imageFile.IsImage())
                    {
                        System.IO.File.Delete(Server.MapPath("/images/ImageGroups/"+ projectGroups.GroupImage));
                        projectGroups.GroupImage = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        imageFile.SaveAs(Server.MapPath("/images/ImageGroups/" + projectGroups.GroupImage));
                    }
                    db.Entry(projectGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectGroups);
        }

        // GET: Admin/ProjectGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectGroups projectGroups = db.ProjectGroups.Find(id);
            if (projectGroups == null)
            {
                return HttpNotFound();
            }

            return View(projectGroups);
        }

        // POST: Admin/ProjectGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectGroups projectGroups = db.ProjectGroups.Find(id);
            db.ProjectGroups.Remove(projectGroups);
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
    }
}