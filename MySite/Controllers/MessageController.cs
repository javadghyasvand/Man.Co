using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MySite.Models.Model;

namespace MySite.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Message()
        {
            return View();
        }
        // POST: Message
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Message([Bind(Include = "Name,Email,PhoneNumber,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var _db = new MySiteDataEntities())
                {
                    contact.MesageDate = DateTime.Now;
                    _db.Contact.Add(contact);
                    _db.SaveChanges();
                    Thread.Sleep(5000);
                    return View();

                }
            }
            return View(contact);
        }
    }
}