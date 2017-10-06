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
        public ActionResult Index()
        {
            // Checking if the cookies has been accepted and if marketing should be displayed
            ViewBag.CookiesAccepted = false;
            ViewBag.HideMarketing = false;
            try
            {
                if (HttpContext.Request.Cookies.AllKeys.Contains("SemillitasCookie"))
                {
                    HttpCookie semillitasCookie = HttpContext.Request.Cookies["SemillitasCookie"];

                    bool cookiesAccepted = false;
                    if (semillitasCookie.Values["IsAccepted"] != null)
                        Boolean.TryParse(semillitasCookie.Values["IsAccepted"], out cookiesAccepted);
                    ViewBag.CookiesAccepted = cookiesAccepted;

                    bool hideMarketing = false;
                    if (semillitasCookie.Values["HideMarketing"] != null)
                        Boolean.TryParse(semillitasCookie.Values["HideMarketing"], out hideMarketing);
                    ViewBag.HideMarketing = hideMarketing;
                }

            } catch (Exception e)
            {
            }

            return View();
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