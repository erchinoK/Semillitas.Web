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
using Semillitas.Web.Models.ViewModels;

namespace Semillitas.Web.Controllers
{
    [Authorize]
    public class EnrollmentAdminController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;

        public EnrollmentAdminController()
        {
            db = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: EnrollmentAdmin
        public ActionResult Index()
        {
            return View(db.Enrollments.ToList());
        }

        // GET: EnrollmentAdmin/Details/5
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

        // GET: EnrollmentAdmin/Create
        public ActionResult Create()
        {
            ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName");
            ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
            return View();
        }

        // POST: EnrollmentAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,MembershipID,ChildFirstName,ChildLastName,ChildBirthDate,HasSpecialNeed,SpecialNeedNotes,ChildNotes,Notes")] EnrollmentAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                // Verifying if the input data exist
                var selectedUser = db.Users.Find(model.UserID);
                if (selectedUser == null)
                {
                    ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName");
                    ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
                    return View(model);
                }
                var selectedMembership = db.Memberships.Find(model.MembershipID);
                if (selectedMembership == null)
                {
                    ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName");
                    ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
                    return View(model);
                }

                // Add enrollment
                var enrollment = new Enrollment()
                {
                    User = selectedUser,
                    Membership = selectedMembership,
                    ChildFirstName = model.ChildFirstName,
                    ChildLastName = model.ChildLastName,
                    ChildBirthDate = model.ChildBirthDate,
                    HasSpecialNeed = model.HasSpecialNeed,
                    SpecialNeedNotes = model.SpecialNeedNotes,
                    ChildNotes = model.ChildNotes,
                    IsActive = false,
                    RenewalNumber = 0,
                    CreationDate = DateTime.Now,
                    CreationUserName = currentUser.UserName,
                    ModifDate = DateTime.Now,
                    ModifUserName = currentUser.UserName,
                    Notes = model.Notes
                };

                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName");
            ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
            return View(model);
        }

        // GET: EnrollmentAdmin/Edit/5
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

            EnrollmentAdminViewModel model = new EnrollmentAdminViewModel()
            {
                ID = enrollment.ID,
                ChildFirstName = enrollment.ChildFirstName,
                ChildLastName = enrollment.ChildLastName,
                ChildBirthDate = enrollment.ChildBirthDate,
                HasSpecialNeed = enrollment.HasSpecialNeed,
                SpecialNeedNotes = enrollment.SpecialNeedNotes,
                ChildNotes = enrollment.ChildNotes,
                Notes = enrollment.Notes,
                UserID = enrollment.User.Id,
                MembershipID = enrollment.Membership.ID,
                StartDate = enrollment.StartDate,
                ExpirationDate = enrollment.ExpirationDate
            };
            ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName", model.UserID);
            ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name", model.MembershipID);
            return View(model);
        }

        // POST: EnrollmentAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,MembershipID,ChildFirstName,ChildLastName,ChildBirthDate,HasSpecialNeed,SpecialNeedNotes,ChildNotes,StartDate,ExpirationDate,Notes")] EnrollmentAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                // Verifying if the user input exists
                var selectedUser = db.Users.Find(model.UserID);
                if (selectedUser == null)
                {
                    ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName");
                    ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
                    return View(model);
                }
                // Verifying if the membership input exists
                var selectedMembership = db.Memberships.Find(model.MembershipID);
                if (selectedMembership == null)
                {
                    ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName");
                    ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
                    return View(model);
                }

                // Verifying if the enrollment exists
                var enrollment = db.Enrollments.Find(model.ID);
                if (enrollment != null)
                {
                    enrollment.User = selectedUser;
                    enrollment.Membership = selectedMembership;
                    enrollment.ChildFirstName = model.ChildFirstName;
                    enrollment.ChildLastName = model.ChildLastName;
                    enrollment.ChildBirthDate = model.ChildBirthDate;
                    enrollment.HasSpecialNeed = model.HasSpecialNeed;
                    enrollment.SpecialNeedNotes = model.SpecialNeedNotes;
                    enrollment.ChildNotes = model.ChildNotes;
                    enrollment.ModifDate = DateTime.Now;
                    enrollment.ModifUserName = currentUser.UserName;
                    enrollment.Notes = model.Notes;
                    enrollment.StartDate = model.StartDate;
                    enrollment.ExpirationDate = model.ExpirationDate;                    
                    db.Entry(enrollment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.UserList = new SelectList(db.Users.ToList(), "ID", "UserName");
            ViewBag.MembershipList = new SelectList(db.Memberships.ToList(), "ID", "Name");
            return View(model);
        }

        // GET: EnrollmentAdmin/Activate/5
        public ActionResult Activate(int? id)
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

            var model = new EnrollmentAdminActivateViewModel()
            {
                ID = enrollment.ID,
                StartDate = DateTime.Now,
                ExpirationDate = Classes.Utility.AddDate(DateTime.Now, enrollment.Membership.Duration),
                Notes = enrollment.Notes,
                Enrollment = enrollment,                
                User = enrollment.User,
                Membership = enrollment.Membership
            };
            return View(model);
        }



        // POST: EnrollmentAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Activate(EnrollmentAdminActivateViewModel model)
        {
            var enrollment = db.Enrollments.Find(model.ID);

            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                // Verifying if the enrollment exists
                if (enrollment != null)
                {
                    enrollment.ModifDate = DateTime.Now;
                    enrollment.ModifUserName = currentUser.UserName;
                    enrollment.Notes = model.Notes;
                    enrollment.StartDate = model.StartDate;
                    enrollment.ExpirationDate = model.ExpirationDate;
                    if (model.StartDate.HasValue) enrollment.IsActive = true;
                    if (TryUpdateModel(enrollment, "",
                        new string[] { "IsActive", "StartDate", "ExpirationDate", "Notes", "ModifDate", "ModifUserName" }))
                    {
                        try
                        {
                            db.SaveChanges();

                            return RedirectToAction("Index");
                        }
                        catch (DataException /* dex */)
                        {
                            //Log the error (uncomment dex variable name and add a line here to write a log.
                            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                        }
                    }
                    
                }
            }

            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            model.Enrollment = enrollment;
            model.User = enrollment.User;
            model.Membership = enrollment.Membership;

            return View(model);
        }

        // GET: EnrollmentAdmin/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: EnrollmentAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
