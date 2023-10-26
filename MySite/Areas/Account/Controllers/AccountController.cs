using MySite.Models.Model;
using MySite.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MySite.Areas.Account.Controllers
{
    public class AccountController : Controller
    {
        readonly MySiteDataEntities _db = new MySiteDataEntities();


        // GET: Account/Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {

                return Redirect("/Admin/Admin");
            }
            return View();
        }

        // POST: Account/Account
        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,UserPassword")] LoginViewModel loginViewModel, string returnurl = "/Admin/Admin")
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Admin");
            }
            if (ModelState.IsValid)
            {
                var HashPassword =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(loginViewModel.UserPassword, "MD5");
                var user = _db.Users.SingleOrDefault(u =>
                    u.UserEmail == loginViewModel.UserName && u.UserPassword == HashPassword);
                if (user != null)
                {
                    if (user.UserActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserEmail, loginViewModel.Remember);
                        return Redirect(returnurl);
                    }

                    ViewBag.Error = "اکانت شما مسدود شده است";
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "کاربری با اطلاعات وارد شده یافت نشد");
                }
            }

            return View();
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
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