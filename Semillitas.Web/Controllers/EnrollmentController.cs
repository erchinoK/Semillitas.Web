using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Semillitas.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Semillitas.Web.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;

        public EnrollmentController()
        {
            db = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Enrollment
        public ActionResult Index()
        {
            return View(db.Enrollments.ToList());
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
            return View();
        }
        
        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MembershipID, ChildFirstName,ChildLastName,ChildBirthDate,HasSpecialNeed,SpecialNeedNotes,ChildNotes")] EnrollmentUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                // Verifying if the membership input exists
                var selectedMembership = db.Memberships.Find(model.MembershipID);
                if (selectedMembership == null)
                {
                    ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
                    return View(model);
                }

                // Add enrollment
                var enrollment = new Enrollment() {
                    ChildFirstName = model.ChildFirstName,
                    ChildLastName = model.ChildLastName,
                    ChildBirthDate = model.ChildBirthDate,
                    HasSpecialNeed = model.HasSpecialNeed,
                    SpecialNeedNotes = model.SpecialNeedNotes,
                    ChildNotes = model.ChildNotes,
                    IsActive = false,
                    User = currentUser,
                    CreationDate = DateTime.Now,
                    CreationUserName = currentUser.UserName,
                    ModifDate = DateTime.Now,
                    ModifUserName = currentUser.UserName,
                    Membership = selectedMembership
                };
                
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }

            ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
            return View(model);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ChildFirstName,ChildLastName,ChildBirthDate,HasSpecialNeed,SpecialNeedNotes,ChildNotes")] Enrollment model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                //var enrollmentToEdit = db.Enrollments.First(e => e.ID == enrollment.ID);
                var enrollment = db.Enrollments.Find(model.ID);
                if (enrollment != null)
                {
                    //enrollment.ModifDate = DateTime.Now;
                    enrollment.ChildFirstName = model.ChildFirstName;
                    enrollment.ChildLastName = model.ChildLastName;
                    enrollment.ChildBirthDate = model.ChildBirthDate;
                    enrollment.HasSpecialNeed = model.HasSpecialNeed;
                    enrollment.SpecialNeedNotes = model.SpecialNeedNotes;
                    enrollment.ChildNotes = model.ChildNotes;
                    enrollment.ModifDate = DateTime.Now;
                    enrollment.ModifUserName = currentUser.UserName;
                    db.Entry(enrollment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return View(model);
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
