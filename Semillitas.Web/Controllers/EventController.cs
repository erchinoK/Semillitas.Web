using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Semillitas.Web.Classes;
using Semillitas.Web.Models;
using Semillitas.Web.Models.ViewModels;

namespace Semillitas.Web.Controllers
{
    [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
    public class EventController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Event
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Event.Where(e => e.IsPublished).Take(4).ToList());
        }

        // GET: Event
        public ActionResult List()
        {
            return View(db.Event.ToList());
        }

        // GET: Event/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            // Verifying if the admin is trying to view an unpublished Event
            if (!@event.IsPublished) 
            {
                if (User.IsInRole(RoleNames.ROLE_ADMINISTRATOR))
                {
                    ViewBag.Message = "Este evento no esta publicado por lo tanto no es visible al publico";
                } else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                
            }

            // Getting the width and height of the image to send them to the fb-share button
            int imageWidth = 0;
            int imageHeight = 0;
            string imageFullPath = Request.MapPath(@event.ImagePath);
            if (System.IO.File.Exists(imageFullPath))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imageFullPath);
                imageWidth = image.Width;
                imageHeight = image.Height;
            }
            ViewBag.ImageWidth = imageWidth.ToString();
            ViewBag.ImageHeight = imageHeight.ToString();

            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            var model = new EventCreateViewModel();
            model.Date = DateTime.Now;

            return View(model);
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,Date,Content,Timetable,TimetableNotes,Location,LocationNotes,Contact,ContactNotes,File,FileNotes,Image,ImagePreview,Layout,Notes")] EventCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imagePath = "";
                // Checking if there is any file
                if (model.Image != null)
                {
                    // Generating the full paths
                    imagePath = System.IO.Path.Combine(UploadDirectory.path, model.Image.FileName);
                    
                    // Uploading the files
                    model.Image.SaveAs(Server.MapPath(imagePath));
                }

                string imagePreviewPath = "";
                // Checking if there is any file
                if (model.ImagePreview != null)
                {
                    // Generating the full paths
                    imagePreviewPath = System.IO.Path.Combine(UploadDirectory.path, model.ImagePreview.FileName);

                    // Uploading the files
                    model.ImagePreview.SaveAs(Server.MapPath(imagePreviewPath));
                }

                string filePath = "";
                // Checking if there is any file
                if (model.File != null)
                {
                    // Generating the full paths
                    filePath = System.IO.Path.Combine(UploadDirectory.path, model.File.FileName);

                    // Uploading the files
                    model.File.SaveAs(Server.MapPath(filePath));
                }

                Event ev = new Event()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Date = model.Date,
                    IsPublished = false,
                    Content = model.Content,
                    Timetable = model.Timetable,
                    TimetableNotes = model.TimetableNotes,
                    Location = model.Location,
                    LocationNotes = model.LocationNotes,
                    Contact = model.Contact,
                    ContactNotes = model.ContactNotes,
                    FilePath = filePath,
                    FileNotes = model.FileNotes,
                    ImagePath = imagePath, 
                    ImagePreviewPath = imagePreviewPath,
                    Layout = model.Layout,
                    Notes = model.Notes,
                    CreationDate = DateTime.Now,
                    CreationUserName = User.Identity.Name,
                    ModifDate = DateTime.Now,
                    ModifUserName = User.Identity.Name
                };
                db.Event.Add(ev);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(model);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event ev = db.Event.Find(id);
            if (ev == null)
            {
                return HttpNotFound();
            }
            EventEditViewModel model = new EventEditViewModel()
            {
                Title = ev.Title,
                Description = ev.Description,
                Date = ev.Date,
                IsPublished = ev.IsPublished,
                Content = ev.Content,
                Timetable = ev.Timetable,
                TimetableNotes = ev.TimetableNotes,
                Location = ev.Location,
                LocationNotes = ev.LocationNotes,
                Contact = ev.Contact,
                ContactNotes = ev.ContactNotes,
                FilePath = ev.FilePath,
                FileNotes = ev.FileNotes,
                ImagePath = ev.ImagePath,
                ImagePreviewPath = ev.ImagePreviewPath,
                Layout = ev.Layout,
                Notes = ev.Notes,
            };

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Date,Content,Timetable,TimetableNotes,Location,LocationNotes,Contact,ContactNotes,File,FileNotes,FileRemove,Image,ImageRemove,ImagePreview,ImagePreviewRemove,Layout,IsPublished,Notes")] EventEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Getting the data from the database
                Event ev = db.Event.Find(model.ID);

                string imagePath = ev.ImagePath;

                // Checking if the image must be removed
                if (model.ImageRemove)
                {
                    string imageFullPath = Request.MapPath(ev.ImagePath);
                    if (System.IO.File.Exists(imageFullPath))
                    {
                        System.IO.File.Delete(imageFullPath);
                    }
                    imagePath = "";
                }
                
                // Checking if there is any file
                if (model.Image != null)
                {
                    // Generating the full paths
                    imagePath = System.IO.Path.Combine(UploadDirectory.path, model.Image.FileName);

                    // Uploading the files
                    model.Image.SaveAs(Server.MapPath(imagePath));
                }

                string imagePreviewPath = ev.ImagePreviewPath;

                // Checking if the image must be removed
                if (model.ImagePreviewRemove)
                {
                    string imagePreviewFullPath = Request.MapPath(ev.ImagePreviewPath);
                    if (System.IO.File.Exists(imagePreviewFullPath))
                    {
                        System.IO.File.Delete(imagePreviewFullPath);
                    }
                    imagePreviewPath = "";
                }

                // Checking if there is any file
                if (model.ImagePreview != null)
                {
                    // Generating the full paths
                    imagePreviewPath = System.IO.Path.Combine(UploadDirectory.path, model.ImagePreview.FileName);

                    // Uploading the files
                    model.ImagePreview.SaveAs(Server.MapPath(imagePreviewPath));
                }

                string filePath = ev.FilePath;

                // Checking if the file must be removed
                if (model.FileRemove)
                {
                    string fileFullPath = Request.MapPath(ev.FilePath);
                    if (System.IO.File.Exists(fileFullPath))
                    {
                        System.IO.File.Delete(fileFullPath);
                    }
                    filePath = "";
                }
               
                // Checking if there is any file
                if (model.File != null)
                {
                    // Generating the full paths
                    filePath = System.IO.Path.Combine(UploadDirectory.path, model.File.FileName);

                    // Uploading the files
                    model.File.SaveAs(Server.MapPath(filePath));
                }

                ev.Title = model.Title;
                ev.Description = model.Description;
                ev.Date = model.Date;
                ev.IsPublished = model.IsPublished;
                ev.Content = model.Content;
                ev.Timetable = model.Timetable;
                ev.TimetableNotes = model.TimetableNotes;
                ev.Location = model.Location;
                ev.LocationNotes = model.LocationNotes;
                ev.Contact = model.Contact;
                ev.ContactNotes = model.ContactNotes;
                ev.FilePath = filePath;
                ev.FileNotes = model.FileNotes;
                ev.ImagePath = imagePath;
                ev.ImagePreviewPath = imagePreviewPath;
                ev.Layout = model.Layout;
                ev.Notes = model.Notes;
                ev.ModifDate = DateTime.Now;
                ev.ModifUserName = User.Identity.Name;

                db.Entry(ev).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(model);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Event.Find(id);
            db.Event.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Download(string filename)
        {
            string basePath = "~/_Uploaded/";
            string newFilename = filename;
            string path = Server.MapPath(basePath + filename);

            if (!System.IO.File.Exists(path))
            {
                return HttpNotFound();
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string newFileName = filename;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, newFileName);
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
