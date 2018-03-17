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
    public class SlideController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Slide
        public ActionResult Index()
        {
            return View(db.Slide.OrderBy(s => s.Position).ToList());
        }

        
        // GET: Slide/Create
        public ActionResult Create()
        {
            var model = new SlideCreateViewModel();

            return View(model);
        }

        // POST: Slide/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,ImageFileEsp,Html")] SlideCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                String imagePathEsp = "";
                //String imagePathCat = "";
                //String imagePathEng = "";

                // Checking if there is any file
                if (model.ImageFileEsp != null)
                {
                    // Generating the full paths
                    imagePathEsp = System.IO.Path.Combine(UploadDirectory.path, "esp_" + model.ImageFileEsp.FileName);
                    //imagePathCat = System.IO.Path.Combine(UploadDirectory.path, "cat_" + model.ImageFileEsp.FileName);
                    //imagePathEng = System.IO.Path.Combine(UploadDirectory.path, "eng_" + model.ImageFileEsp.FileName);

                    // Uploading the files
                    model.ImageFileEsp.SaveAs(Server.MapPath(imagePathEsp));
                    //model.ImageFileEsp.SaveAs(Server.MapPath(imagePathCat));
                    //model.ImageFileEsp.SaveAs(Server.MapPath(imagePathEng));
                } else
                {
                    ModelState.AddModelError("", "Todas las imagenes deben ser adjuntadas. Por favor intente nuevamente.");
                    return View(model);
                }

                // Getting the last position
                int lastPos = 1;
                var last = db.Slide.OrderByDescending(s => s.Position).FirstOrDefault();
                if (last != null)
                {
                    lastPos = last.Position + 1;
                }

                // Creating the object to save in the DB
                var slide = new Slide()
                {
                    Description = model.Description,
                    Position = lastPos,
                    IsPublished = false,
                    ImagePathEsp = imagePathEsp,
                    //ImagePathCat = imagePathCat,
                    //ImagePathEng = imagePathEng,
                    Html = model.Html,
                    CreationDate = DateTime.Now,
                    CreationUserName = User.Identity.Name,
                    ModifDate = DateTime.Now,
                    ModifUserName = User.Identity.Name
                };

                
                db.Slide.Add(slide);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Slide/Create
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }

            var model = new SlideEditViewModel()
            {
                ID = slide.ID,
                Description = slide.Description,
                Position = slide.Position,
                IsPublished = slide.IsPublished,
                Html = slide.Html
            };

            return View(model);
        }

        // POST: Slide/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Position,IsPublished,Html")] SlideEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                var slide = db.Slide.Find(model.ID);

                slide.Description = model.Description;
                slide.Position = model.Position;
                slide.IsPublished = model.IsPublished;
                slide.Html = model.Html;
                slide.ModifDate = DateTime.Now;
                slide.ModifUserName = User.Identity.Name;

                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Slide/Publish/5
        public ActionResult Publish(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }

            // Changing the mode
            slide.IsPublished = !slide.IsPublished;

            if (slide.IsPublished)
            {
                slide.PublishDate = DateTime.Now;
            } else
            {
                slide.PublishDate = null;
            }
            db.SaveChanges();
            return RedirectToAction("Index"); ;
        }

        // GET: Slide/Publish/5
        public ActionResult Move(int? id, String dir)
        {
            if (id == null || String.IsNullOrEmpty(dir))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }

            // Changing the position
            if (dir.Equals("up"))
            {
                slide.Position--;
            } else if (dir.Equals("down"))
            {
                slide.Position++;
            }
            db.SaveChanges();
            return RedirectToAction("Index"); ;
        }

        // GET: Slide/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slide.Find(id);
            db.Slide.Remove(slide);
            db.SaveChanges();

            // Deleting files
            string fullPathEsp = Request.MapPath(slide.ImagePathEsp);
            if (System.IO.File.Exists(fullPathEsp))
            {
                System.IO.File.Delete(fullPathEsp);
            }
            string fullPathCat = Request.MapPath(slide.ImagePathCat);
            if (System.IO.File.Exists(fullPathCat))
            {
                System.IO.File.Delete(fullPathCat);
            }
            string fullPathEng = Request.MapPath(slide.ImagePathEng);
            if (System.IO.File.Exists(fullPathEng))
            {
                System.IO.File.Delete(fullPathEng);
            }

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
