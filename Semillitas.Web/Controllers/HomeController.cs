using Semillitas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semillitas.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            // Checking if the cookies has been accepted
            ViewBag.CookiesAccepted = CheckCookiesAccepted();

            // Checking if marketing should be displayed
            ViewBag.HideMarketing = CheckHideMarketing();

            ViewBag.BlogEntries = db.Blog.Where(b => b.IsPublished).OrderBy(b => b.DatePublishment).Take(4).ToList<Models.Blog>();    

            return View();
        }

        public bool CheckCookiesAccepted()
        {
            bool cookiesAccepted = false;
            try
            {
                if (HttpContext.Request.Cookies.AllKeys.Contains("cookie.consent"))
                {
                    HttpCookie semillitasCookie = HttpContext.Request.Cookies["cookie.consent"];

                    if (semillitasCookie.Value != null)
                        Boolean.TryParse(semillitasCookie.Value, out cookiesAccepted);
                }

            }
            catch (Exception e) { }

            return cookiesAccepted;
        }

        public bool CheckHideMarketing()
        {
            bool hideMarketing = false;
            try
            {
                if (HttpContext.Request.Cookies.AllKeys.Contains("subscription.visited"))
                {
                    HttpCookie semillitasCookie = HttpContext.Request.Cookies["subscription.visited"];

                    if (semillitasCookie.Value != null)
                        Boolean.TryParse(semillitasCookie.Value, out hideMarketing);
                }

            }
            catch (Exception e) { }

            return hideMarketing;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "Your photos page.";

            return View();
        }

        public ActionResult Plans()
        {
            ViewBag.Message = "Your plans page.";

            return View();
        }

        public ActionResult Workshops()
        {
            ViewBag.Message = "Your workshop page.";

            return View();
        }

        public ActionResult Events()
        {
            ViewBag.Message = "Your events page.";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your blog page.";

            return View();
        }

        public ActionResult BlogEntry()
        {
            ViewBag.Message = "Your blog page.";

            return View();
        }

        public ActionResult UnderConstruction()
        {
           
            return View();
        }
    }
}