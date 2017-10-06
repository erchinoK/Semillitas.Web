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
    public class MarketingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Marketing
        public ActionResult Index()
        {
            return View(db.Marketing.ToList());
        }

        // GET: Marketing/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marketing marketing = db.Marketing.Find(id);
            if (marketing == null)
            {
                return HttpNotFound();
            }
            return View(marketing);
        }

        // GET: Marketing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marketing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email")] HomeIndexViewModel hivm)
        {
            string Type = "MARKETING_0";

            if (ModelState.IsValid)
            {
                try {
                    // If the email is not registered then it is saved
                    if (db.Marketing.Where(m => m.Email == hivm.Email && m.Type == Type).Count() == 0)
                    {
                        Marketing newMarketing = new Marketing();
                        newMarketing.Email = hivm.Email;
                        newMarketing.Date = DateTime.Now;
                        newMarketing.Type = Type;
                        db.Marketing.Add(newMarketing);
                        db.SaveChanges();
                    }                    
                } catch (Exception e)
                {

                }
                
            }

            return PartialView("_MarketingSaved");
        }

        // GET: Marketing/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marketing marketing = db.Marketing.Find(id);
            if (marketing == null)
            {
                return HttpNotFound();
            }
            return View(marketing);
        }

        // POST: Marketing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Name,Type,Date,Notes")] Marketing marketing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marketing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marketing);
        }

        // GET: Marketing/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marketing marketing = db.Marketing.Find(id);
            if (marketing == null)
            {
                return HttpNotFound();
            }
            return View(marketing);
        }

        // POST: Marketing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marketing marketing = db.Marketing.Find(id);
            db.Marketing.Remove(marketing);
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
