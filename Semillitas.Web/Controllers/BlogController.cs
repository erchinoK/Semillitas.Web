using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Semillitas.Web.Models;
using Semillitas.Web.Models.ViewModels;

namespace Semillitas.Web.Controllers
{
    [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
    public class BlogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blog.Where(b => b.IsPublished).ToList());
        }

        // GET: Blog/List
        public ActionResult List()
        {
            return View(db.Blog.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            var model = new BlogCreateViewModel();

            return View(model);
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Content,ImageFile,Layout,IsPublished,Notes")] BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                String imagePath = "";
                if (model.ImageFile != null)
                {
                    //string pic = System.IO.Path.GetFileName();
                    //string newPic = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + "_" + System.IO.Path.GetExtension(file.FileName);
                    imagePath = System.IO.Path.Combine(UploadDirectory.path, model.ImageFile.FileName);
                    // file is uploaded
                    model.ImageFile.SaveAs(Server.MapPath(imagePath));                    
                }

                var blog = new Blog()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Image = imagePath,
                    ImagePreview = imagePath,
                    Layout = model.Layout,
                    IsPublished = model.IsPublished,
                    Notes = model.Notes,
                    CreationDate = DateTime.Now,
                    CreationUserName = User.Identity.Name,
                    ModifDate = DateTime.Now,
                    ModifUserName = User.Identity.Name
                };

                blog.DatePublishment = DateTime.Now;
                //if (blog.IsPublished)
                //{
                //    blog.DatePublishment = DateTime.Now;
                //}
                
                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Content,Image,Layout,IsPublished,DatePublishment,CreationDate,CreationUserName,ModifDate,ModifUserName,Notes")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.IsPublished)
                {
                    blog.DatePublishment = DateTime.Now;
                }

                blog.ModifDate = DateTime.Now;
                blog.ModifUserName = User.Identity.Name;

                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
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
