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
    public class PaymentAdminController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;

        public PaymentAdminController()
        {
            db = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Payment
        public ActionResult Index()
        {
            return View(db.Payments.ToList());
        }

        // GET: Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        public List<PaymentListItem> GetPayments(Enrollment currentEnrollment)
        {
            bool isRegistrationRequired = currentEnrollment.Membership.IsRegistrationRequired;
            int numberPayments = currentEnrollment.Membership.NumberPayments;

            // Creating 12 empty payments to fill later and show to the user
            DateTime expirationDate = (DateTime)currentEnrollment.StartDate;   // Initializing the expiration date of the first month

            var AllPayments = new List<PaymentListItem>();
            if (isRegistrationRequired)
            {
                AllPayments.Add(new PaymentListItem() { IsPaid = false, ExpirationDate = expirationDate, Type = PaymentType.PAYMENT_REGISTRATION, PayingMonth = "0" });   // Adding registration payment
            } else
            {
                AllPayments.Add(new PaymentListItem() { IsPaid = true, ExpirationDate = null, Type = PaymentType.PAYMENT_REGISTRATION, PayingMonth = "0", Notes = "No registration required" });   // Adding registration payment
            }
            for (int i = 1; i <= numberPayments; i++)
            {
                var listItem = new PaymentListItem()
                {
                    PayingMonth = i.ToString(),
                    IsPaid = false,
                    ExpirationDate = expirationDate
                };
                expirationDate = expirationDate.AddMonths(1);
                AllPayments.Add(listItem);
                
            }

            // Obtaining real payments to fill the list of empty payments
            var PaidPayments = db.Payments.Where(p => p.Enrollment.ID == currentEnrollment.ID && p.RenewalNumber == currentEnrollment.RenewalNumber).ToList();
            foreach (var p in PaidPayments)
            {
                int pos = Convert.ToInt32(p.PayingMonth);
                AllPayments.ElementAt(pos).IsPaid = true;
                AllPayments.ElementAt(pos).ID = p.ID;
                AllPayments.ElementAt(pos).Date = p.Date;
                AllPayments.ElementAt(pos).Type = p.Type;
                AllPayments.ElementAt(pos).Amount = p.Amount;
                AllPayments.ElementAt(pos).Notes = p.Notes;              
            }

            return AllPayments;
        }

        // GET: Payment/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment currentEnrollment = db.Enrollments.Find(id);
            if (currentEnrollment == null)
            {
                return HttpNotFound();
            }

            // Creating an empty payment to pass the enrollment ID to the hidden input
            var model = new PaymentAdminViewModel();
            model.EnrollmentID = (int)id;
            model.Enrollment = currentEnrollment;
            model.Date = DateTime.Now;  // Initilializing the payment date as now because is paying at this moment

            model.AllPayments = GetPayments(currentEnrollment);

            return View(model);
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,Date,PaymentMethod,PayingRegistration, AmountRegistration, NotesRegistration, PayingMonth1, AmountMonth1, NotesMonth1, PayingMonth2, AmountMonth2, NotesMonth2, PayingMonth3, AmountMonth3, NotesMonth3, PayingMonth4, AmountMonth4, NotesMonth4, PayingMonth5, AmountMonth5, NotesMonth5, PayingMonth6, AmountMonth6, NotesMonth6, PayingMonth7, AmountMonth7, NotesMonth7, PayingMonth8, AmountMonth8, NotesMonth8, PayingMonth9, AmountMonth9, NotesMonth9, PayingMonth10, AmountMonth10, NotesMonth10, PayingMonth11, AmountMonth11, NotesMonth11, PayingMonth12, AmountMonth12, NotesMonth12")] PaymentAdminViewModel model)
        {
            // Re-populating the last payments in case the view has to be displayed again
            var currentEnrollment = db.Enrollments.Find(model.EnrollmentID);
            model.Enrollment = currentEnrollment;
            model.AllPayments = GetPayments(currentEnrollment);

            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                if (currentEnrollment == null) {
                    
                    return View(model);

                }

                if (model.PayingRegistration)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountRegistration,
                        Type = PaymentType.PAYMENT_REGISTRATION,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "0",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesRegistration,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth1)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth1,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "1",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth1,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth2)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth2,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "2",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth2,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth3)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth3,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "3",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth3,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth4)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth4,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "4",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth4,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth5)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth5,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "5",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth5,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth6)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth6,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "6",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth6,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth7)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth7,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "7",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth7,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth8)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth8,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "8",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth8,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth9)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth9,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "9",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth9,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth10)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth10,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "10",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth10,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth11)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth11,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "11",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth11,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                if (model.PayingMonth12)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountMonth12,
                        Type = PaymentType.PAYMENT_FEE,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "12",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesMonth12,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }
                
                if (model.PayingOther)
                {
                    Payment payment = new Payment
                    {
                        Enrollment = currentEnrollment,
                        Amount = model.AmountOther,
                        Type = PaymentType.PAYMENT_OTHER,
                        Date = model.Date,
                        PaymentMethod = model.PaymentMethod,
                        PayingMonth = "0",
                        RenewalNumber = currentEnrollment.RenewalNumber,
                        Notes = model.NotesOther,
                        CreationDate = DateTime.Now,
                        CreationUserName = currentUser.UserName,
                        ModifDate = DateTime.Now,
                        ModifUserName = currentUser.UserName
                    };
                    db.Payments.Add(payment);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Payment/Renew
        public ActionResult Renew(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment currentEnrollment = db.Enrollments.Find(id);
            if (currentEnrollment == null)
            {
                return HttpNotFound();
            }

            // Creating an empty payment to pass the enrollment ID to the hidden input
            var model = new PaymentAdminRenewViewModel();
            model.EnrollmentID = (int)id;
            model.Enrollment = currentEnrollment;
            model.Date = DateTime.Now;  // Initilializing the payment date as now because is paying at this moment
            model.StartDate = DateTime.Now;     // Changing the new starting date
            model.ExpirationDate = Classes.Utility.AddDate(DateTime.Now, currentEnrollment.Membership.Duration);    // Changing the new expiration date

            model.AllPayments = GetPayments(currentEnrollment);

            return View(model);
        }

        // POST: Payment/Renew
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renew([Bind(Include = "EnrollmentID,Date,StartDate,ExpirationDate,PaymentMethod,Amount,Notes")] PaymentAdminRenewViewModel model)
        {
            // Re-populating the last payments in case the view has to be displayed again
            var currentEnrollment = db.Enrollments.Find(model.EnrollmentID);
            model.Enrollment = currentEnrollment;
            model.AllPayments = GetPayments(currentEnrollment);

            if (ModelState.IsValid)
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());

                if (currentEnrollment == null)
                {

                    return View(model);

                }

                currentEnrollment.RenewalNumber++;
                currentEnrollment.StartDate = model.StartDate;
                currentEnrollment.ExpirationDate = model.ExpirationDate;
                currentEnrollment.ModifDate = DateTime.Now;
                currentEnrollment.ModifUserName = currentUser.UserName;
                db.Entry(currentEnrollment).State = EntityState.Modified;

                Payment payment = new Payment
                {
                    Enrollment = currentEnrollment,
                    Amount = model.Amount,
                    Type = PaymentType.PAYMENT_RENEW,
                    Date = model.Date,
                    PaymentMethod = model.PaymentMethod,
                    PayingMonth = "0",
                    RenewalNumber = currentEnrollment.RenewalNumber,
                    Notes = model.Notes,
                    CreationDate = DateTime.Now,
                    CreationUserName = currentUser.UserName,
                    ModifDate = DateTime.Now,
                    ModifUserName = currentUser.UserName
                };
                db.Payments.Add(payment);
                
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Amount,Type,Date,PaymentMethod,PayingMonth,CreationDate,CreationUserName,ModifDate,ModifUserName,Notes")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
