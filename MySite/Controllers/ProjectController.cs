using MySite.Models.Model;
using MySite.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySite.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Project(int id)
        {
            using (var _db = new MySiteDataEntities())
            {
                var project = _db.Project.Single(p => p.Id == id);
                ViewBag.gallery = project.Gallery.Where(g => g.ProjectId == project.Id).ToList();
                return View(new ProjectViewModel
                {
                    Id = project.Id,
                    TitleProject = project.TitleProject,
                    VideoProject = project.VideoProject,
                    LinkProject = project.LinkProject,
                    ImageProject = project.ImageProject,
                    DescriptionProject = project.DescriptionProject,
                    MetaDescription = _db.Seo.Single(s => s.ParentId == project.Id && s.ParentTitle == "project")
                        .MetaDescription,
                    MetaKeywords = _db.Seo.Single(s => s.ParentId == project.Id && s.ParentTitle == "project")
                        .MetaKeywords,
                    MetaTitle = _db.Seo.Single(s => s.ParentId == project.Id && s.ParentTitle == "project").MetaTitle,
                    Tag = _db.Seo.Single(s => s.ParentId == project.Id && s.ParentTitle == "project").Tag,
                });
            }
        }

        [Route("Archive")]
        public ActionResult ArchiveProject(int pageId = 1,int? selectGroup =null )

        {
            using (var _db = new MySiteDataEntities())
            {
                ViewBag.groups = _db.ProjectGroups.ToList();
                ViewBag.pageId = pageId;
                ViewBag.select = selectGroup;
                List<Project> list = new List<Project>();
                
                if (selectGroup != null)
                {
                    pageId = 1;
                    list.AddRange(_db.SelectProjectGroup.Where(g => g.GroupId == selectGroup).Select(p => p.Project).ToList());
                        list = list.Distinct().ToList();
                }
                else
                {
                    list.AddRange(_db.Project.ToList());
                }
                //paging
                
                int take = 8;
                ViewBag.pagecount = (int)Math.Ceiling((double)list.Count / take);
               int skip = (pageId - 1) * take;
                //ViewBag.pagecount = list.Count / take;
                return View(list.OrderByDescending(p => p.Id).Skip(skip).Take(take).ToList());

            }
        }
    }
}//