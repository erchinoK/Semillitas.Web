using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Semillitas.Web.Models;

namespace Semillitas.Web.Controllers
{
    [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
    public class VariableController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Variable
        public ActionResult Index()
        {
            return View(db.Variable.ToList());
        }

        // GET: Variable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        // GET: Variable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Variable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Value,Notes")] Variable variable)
        {
            if (ModelState.IsValid)
            {
                variable.CreationDate = DateTime.Now;
                variable.CreationUserName = User.Identity.Name;
                variable.ModifDate = DateTime.Now;
                variable.ModifUserName = User.Identity.Name;
                db.Variable.Add(variable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(variable);
        }

        // GET: Variable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        // POST: Variable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Value,Notes")] Variable model)
        {
            if (ModelState.IsValid)
            {
                var variable = db.Variable.Find(model.ID);
                variable.Description = model.Description;
                variable.Value = model.Value;
                variable.ModifDate = DateTime.Now;
                variable.ModifUserName = User.Identity.Name;
                variable.Notes = model.Notes;
                db.Entry(variable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Variable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variable variable = db.Variable.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        // POST: Variable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Variable variable = db.Variable.Find(id);
            db.Variable.Remove(variable);
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
