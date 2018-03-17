using Semillitas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Semillitas.Web.Controllers
{
    [Authorize(Roles = RoleNames.ROLE_ADMINISTRATOR)]
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Semillitas.Web.Models.Email model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(model.To);
                    mail.From = new MailAddress(model.From);
                    mail.Subject = model.Subject;
                    mail.Body = model.Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.ludotecasemillitas.com";
                    smtp.Port = 587; //587
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("hola@ludotecasemillitas.com", "semillitas12345.");
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e) {
                    ModelState.AddModelError(String.Empty, e.Message);
                }
                
            }
            return View(model);
        }
    }
}