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
        [AllowAnonymous]
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
        [AllowAnonymous]
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

            // Verifying if the admin is trying to view an unpublished Event
            if (!blog.IsPublished)
            {
                if (User.IsInRole(RoleNames.ROLE_ADMINISTRATOR))
                {
                    ViewBag.Message = "Este blog no esta publicado por lo tanto no es visible al publico";
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

            }

            // Getting the width and height of the image to send them to the fb-share button
            int imageWidth = 0;
            int imageHeight = 0;
            string imageFullPath = Request.MapPath(blog.Image);
            if (System.IO.File.Exists(imageFullPath))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imageFullPath);
                imageWidth = image.Width;
                imageHeight = image.Height;
            }
            ViewBag.ImageWidth = imageWidth.ToString();
            ViewBag.ImageHeight = imageHeight.ToString();

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
        public ActionResult Create([Bind(Include = "ID,Title,Description,Content,ImageFile,ImagePreviewFile,Layout,Notes")] BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string imagePath = "";
                // Checking if there is any file
                if (model.ImageFile != null)
                {
                    // Generating the full paths
                    imagePath = System.IO.Path.Combine(UploadDirectory.path, model.ImageFile.FileName);

                    // Uploading the files
                    model.ImageFile.SaveAs(Server.MapPath(imagePath));
                }

                string imagePreviewPath = "";
                // Checking if there is any file
                if (model.ImagePreviewFile != null)
                {
                    // Generating the full paths
                    imagePreviewPath = System.IO.Path.Combine(UploadDirectory.path, model.ImagePreviewFile.FileName);

                    // Uploading the files
                    model.ImagePreviewFile.SaveAs(Server.MapPath(imagePreviewPath));
                }

                var blog = new Blog()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Image = imagePath,
                    ImagePreview = imagePreviewPath,
                    Layout = model.Layout,
                    IsPublished = false,
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
                return RedirectToAction("List");
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
            BlogEditViewModel model = new BlogEditViewModel()
            {
                Title = blog.Title,
                Description = blog.Description,
                IsPublished = blog.IsPublished,
                Content = blog.Content,
                Image = blog.Image,
                ImagePreview = blog.ImagePreview,
                Layout = blog.Layout,
                Notes = blog.Notes,
            };


            return View(model);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Content,Image,ImageRemove, ImageFile, ImagePreview, ImagePreviewRemove, ImagePreviewFile,Layout,IsPublished,Notes")] BlogEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Blog blog = db.Blog.Find(model.ID);

                string imagePath = blog.Image;

                // Checking if the image must be removed
                if (model.ImageRemove)
                {
                    string imageFullPath = Request.MapPath(blog.Image);
                    if (System.IO.File.Exists(imageFullPath))
                    {
                        System.IO.File.Delete(imageFullPath);
                    }
                    imagePath = "";
                }

                // Checking if there is any file
                if (model.ImageFile != null)
                {
                    // Generating the full paths
                    imagePath = System.IO.Path.Combine(UploadDirectory.path, model.ImageFile.FileName);

                    // Uploading the files
                    model.ImageFile.SaveAs(Server.MapPath(imagePath));
                }

                string imagePreviewPath = blog.ImagePreview;

                // Checking if the image must be removed
                if (model.ImagePreviewRemove)
                {
                    string imagePreviewFullPath = Request.MapPath(blog.ImagePreview);
                    if (System.IO.File.Exists(imagePreviewFullPath))
                    {
                        System.IO.File.Delete(imagePreviewFullPath);
                    }
                    imagePreviewPath = "";
                }

                // Checking if there is any file
                if (model.ImagePreviewFile != null)
                {
                    // Generating the full paths
                    imagePreviewPath = System.IO.Path.Combine(UploadDirectory.path, model.ImagePreviewFile.FileName);

                    // Uploading the files
                    model.ImagePreviewFile.SaveAs(Server.MapPath(imagePreviewPath));
                }

                blog.Title = model.Title;
                blog.Description = model.Description;
                blog.IsPublished = model.IsPublished;
                blog.Content = model.Content;
                blog.Image = imagePath;
                blog.ImagePreview = imagePreviewPath;
                blog.Layout = model.Layout;
                blog.Notes = model.Notes;
                blog.ModifDate = DateTime.Now;
                blog.ModifUserName = User.Identity.Name;

                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(model);
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
