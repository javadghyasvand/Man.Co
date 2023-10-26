using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using InsertShowImage;
using KooyWebApp_MVC.Classes;
using MySite.Models.Model;
using MySite.Models.ViewModel;
using EntityState = System.Data.Entity.EntityState;

namespace MySite.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly MySiteDataEntities _db = new MySiteDataEntities();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = _db.Users.Include(u => u.Roles);
            return View(users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users users = _db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.Roles = _db.Roles.ToList();
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,UserName,UserEmail,UserPhone,UserPassword,UserImage")]
            Users users, int RolseSelect,
            HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var HashPassword =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(users.UserPassword, "MD5");
                users.UserImage = "no-photo.jpg";
                if (imageFile != null && imageFile.IsImage())
                {
                    users.UserImage =
                        Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    imageFile.SaveAs(Server.MapPath("/images/ImageUser/" + users.UserImage));
                }

                users.UserPassword = HashPassword;
                users.RoleId = RolseSelect;
                users.UserActive = true;
                _db.Users.Add(users);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Roles = _db.Roles.ToList();
            return View(users);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users users = _db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }

            ViewBag.Roles = _db.Roles.ToList();
            return View(users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,UserName,UserEmail,UserPhone,RoleId,UserImage,UserPassword")]
            Users users,
            HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.IsImage())
                {
                    if (users.UserImage != "no-photo.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/images/ImageUser/" + users.UserImage));
                        users.UserImage =
                            Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                        imageFile.SaveAs(Server.MapPath("/images/ImageUser/" + users.UserImage));
                    }
                    else
                    {
                        users.UserImage =
                            Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        imageFile.SaveAs(Server.MapPath("/images/ImageUser/" + users.UserImage));
                    }
                }

                _db.Entry(users).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Roles = _db.Roles.ToList();
            return View(users);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users users = _db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = _db.Users.Find(id);
            users.UserActive = false;
            _db.Entry(users).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserBan(int id)
        {
            Users users = _db.Users.Find(id);
            users.UserActive = false;
            _db.Entry(users).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserunBan(int id)
        {
            Users users = _db.Users.Find(id);
            users.UserActive = true;
            _db.Entry(users).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword(int id)
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                Id = id
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changPasswordViewModel)
        {
            var user = _db.Users.Single(u => u.Id==changPasswordViewModel.Id);
            if (ModelState.IsValid)
            {
              
                    string hashNewPassword =
                        FormsAuthentication.HashPasswordForStoringInConfigFile(changPasswordViewModel.UserPassword, "MD5");
                    user.UserPassword = hashNewPassword;
                    _db.SaveChanges();
                    ViewBag.Success = true;
                    return RedirectToAction("Index");
            }

            return View(changPasswordViewModel);
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