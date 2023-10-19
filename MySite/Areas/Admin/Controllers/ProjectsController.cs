using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InsertShowImage;
using KooyWebApp_MVC.Classes;
using MySite.Models.Model;
using MySite.Models.ViewModel;
using EntityState = System.Data.Entity.EntityState;

namespace MySite.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly MySiteDataEntities _db = new MySiteDataEntities();

        // GET: Admin/Projects
        public ActionResult Index()
        {
            return View(_db.Project.ToList());
        }

        // GET: Admin/Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = _db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            ViewBag.Groups = _db.ProjectGroups.ToList();
            return View();
        }

        // POST: Admin/Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include =
                "Id,TitleProject,ImageProject,DescriptionProject,Tag,MetaTitle,MetaKeywords,MetaDescription,LinkProject,VideoProject")]
            ProjectViewModel project, HttpPostedFileBase imageUpload, HttpPostedFileBase videoUpload, int groupId,
            string tags)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(groupId.ToString()))
                {
                    ViewBag.ErorrSelectedGroups = true;
                    ViewBag.Groups = _db.ProjectGroups.ToList();
                    return View(project);
                }

                if (videoUpload == null)
                {
                    project.VideoProject = "Default.mp4";
                }
                else
                {
                    project.VideoProject =
                        Guid.NewGuid().ToString() + Path.GetExtension(videoUpload.FileName);
                    videoUpload.SaveAs(Server.MapPath("/images/VideoProject/" + project.VideoProject));
                }

                if (imageUpload != null && imageUpload.IsImage())
                {
                    project.ImageProject =
                        Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/images/ImageProject/" + project.ImageProject));
                }
                else
                {
                    ViewBag.Groups = _db.ProjectGroups.ToList();
                    ModelState.AddModelError("ImageProject", "لطفا فایل را انتخاب کنید .");
                    return View(project);
                }

                _db.Project.Add(new Project()
                {
                    TitleProject = project.TitleProject,
                    LinkProject = project.LinkProject,
                    VideoProject = project.VideoProject,
                    ImageProject = project.ImageProject,
                    DescriptionProject = project.DescriptionProject,
                });
                _db.Seo.Add(new Seo()
                {
                    MetaTitle = project.MetaTitle,
                    MetaDescription = project.MetaDescription,
                    MetaKeywords = project.MetaKeywords,
                    ParentId = project.Id,
                    ParentTitle = "project",
                    Tag = project.Tag
                });
                _db.SelectProjectGroup.Add(new SelectProjectGroup()
                {
                    GroupId = groupId,
                    ProjectId = project.Id,
                });
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }

                return RedirectToAction("Index");
            }

            ViewBag.Groups = _db.ProjectGroups.ToList();
            return View(project);
        }

        // GET: Admin/Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.SelectedGroups = _db.SelectProjectGroup.Single(g => g.ProjectId == id);
            ViewBag.Groups = _db.ProjectGroups.ToList();
            Project project = _db.Project.Find(id);
            Seo seo = _db.Seo.Single(s => s.ParentId == id && s.ParentTitle == "project");

            if (project == null)
            {
                return HttpNotFound();
            }

            ProjectViewModel projectViewModel = new ProjectViewModel()
            {
                Id = project.Id,
                TitleProject = project.TitleProject,
                ImageProject = project.ImageProject,
                VideoProject = project.VideoProject,
                DescriptionProject = project.DescriptionProject,
                LinkProject = project.LinkProject,
                Tag = seo.Tag,
                MetaDescription = seo.MetaDescription,
                MetaKeywords = seo.MetaKeywords,
                MetaTitle = seo.MetaTitle,
                IdSeo = seo.Id
            };
            return View(projectViewModel);
        }

        // POST: Admin/Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include =
                "Id,TitleProject,ImageProject,DescriptionProject,Tag,MetaTitle,MetaKeywords,MetaDescription,LinkProject,VideoProject,IdSeo")]
            ProjectViewModel projectModel, HttpPostedFileBase imageUpload, HttpPostedFileBase videoUpload, int groupId,
            string tags)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(groupId.ToString()))
                {
                    ViewBag.ErorrSelectedGroups = true;
                    ViewBag.Groups = _db.ProjectGroups.ToList();
                    return View(projectModel);
                }

                if (videoUpload != null)
                {
                    System.IO.File.Delete(Server.MapPath("/images/VideoProject/" + projectModel.VideoProject));
                    projectModel.VideoProject =
                        Guid.NewGuid().ToString() + Path.GetExtension(videoUpload.FileName);
                    videoUpload.SaveAs(Server.MapPath("/images/VideoProject/" + projectModel.VideoProject));
                }

                if (imageUpload != null && imageUpload.IsImage())
                {
                    System.IO.File.Delete(Server.MapPath("/images/ImageProject/" + projectModel.ImageProject));
                    projectModel.ImageProject =
                        Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/images/ImageProject/" + projectModel.ImageProject));
                }

                Project project = _db.Project.Find(projectModel.Id);
                project.ImageProject = projectModel.ImageProject;
                project.LinkProject = projectModel.LinkProject;
                project.DescriptionProject = projectModel.DescriptionProject;
                project.TitleProject = projectModel.TitleProject;
                project.VideoProject = projectModel.VideoProject;

                Seo seo = _db.Seo.Find(projectModel.IdSeo);
                seo.MetaTitle = projectModel.MetaTitle;
                seo.MetaKeywords = projectModel.MetaKeywords;
                seo.MetaDescription = projectModel.MetaDescription;
                seo.ParentId = projectModel.Id;
                seo.Tag = projectModel.Tag;

                SelectProjectGroup selectListGroup = _db.SelectProjectGroup.Single(g => g.ProjectId == projectModel.Id);
                selectListGroup.GroupId = groupId;


                _db.Entry(project).State = EntityState.Modified;
                _db.Entry(selectListGroup).State = EntityState.Modified;
                _db.Entry(seo).State = EntityState.Modified;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Groups = _db.ProjectGroups.ToList();
            return View(projectModel);
        }

        // GET: Admin/Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = _db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = _db.Project.Find(id);
            _db.Project.Remove(project);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Gallery(int id)
        {
            ViewBag.Gallery = _db.Gallery.Where(g => g.ProjectId == id).ToList();
            return View(new Gallery()
            {
                ProjectId = id,
            });
        }

        [HttpPost]
        public ActionResult Gallery(Gallery gallery, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null && imageUpload.IsImage())
                {
                    gallery.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/images/ImageProject/" + gallery.ImageName));
                    ImageResizer imgResizer = new ImageResizer();
                    _db.Gallery.Add(gallery);
                    _db.SaveChanges();
                    return RedirectToAction("Gallery", new { id = gallery.ProjectId });
                }
                else
                {
                    ViewBag.Gallery = _db.Gallery.Where(g => g.Id == gallery.Id).ToList();
                    ModelState.AddModelError("ImageName", "لطفا فایل را انتخاب کنید .");
                    return View(gallery);
                }
            }

            return RedirectToAction("Gallery", new { id = gallery.ProjectId });
        }

        public ActionResult DeleteGallery(int id)
        {
            var gallery = _db.Gallery.Find(id);
            if (gallery != null)
            {
                System.IO.File.Delete(Server.MapPath("/images/ImageProject/" + gallery.ImageName));
                _db.Gallery.Remove(gallery);
                _db.SaveChanges();
            }

            return RedirectToAction("Gallery", new { id = gallery.ProjectId });
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