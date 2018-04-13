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

            // Checking if modal should be displayed
            ViewBag.ShowModal = CheckShowModal();

            ViewBag.BlogEntries = db.Blog.Where(b => b.IsPublished).OrderBy(b => b.DatePublishment).Take(4).ToList<Models.Blog>();

            ViewBag.Slides = db.Slide.Where(s => s.IsPublished).OrderBy(s => s.Position);

            ViewBag.MetaKeywords = GetKeywords();
            
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


        public bool CheckShowModal()
        {
            bool showModalIndex = false;
            bool showModalIndexForce = false;

            // CHecking if we want to always show the modal
            try
            {
                var variable = db.Variable.Where(v => v.Name == "SHOW_MODAL_INDEX_FORCE").Single();
                showModalIndexForce = variable.Value.Equals("true");
                if (showModalIndexForce) return true;
            } catch (Exception e) {
                showModalIndexForce = false;
            } 

            // Checking if WE want to show the modal (checking later for cookies)
            try
            {
                var variable = db.Variable.Where(v => v.Name == "SHOW_MODAL_INDEX").Single();
                showModalIndex = variable.Value.Equals("true");
            } catch (Exception e) {
                showModalIndex = false;
            }
            
            // Verifying if the USER already saw te modal
            if (showModalIndex)
            {                
                try
                {
                    if (HttpContext.Request.Cookies.AllKeys.Contains("subscription.visited"))
                    {
                        HttpCookie semillitasCookie = HttpContext.Request.Cookies["subscription.visited"];

                        bool subscriptionVisited = Boolean.Parse(semillitasCookie.Value);
                        showModalIndex = !subscriptionVisited;
                    }

                }
                catch (Exception e) {
                    showModalIndex = false;
                }

            }

            return showModalIndex;
        }

        public String GetKeywords()
        {
            String keywords = "";

            try {
                var variable = db.Variable.Where(v => v.Name == "KEYWORDS").Single();
                keywords = variable.Value;
            }
            catch (Exception e)
            {
            }
            
            return keywords;
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

        //public ActionResult Events()
        //{
        //    ViewBag.Message = "Your events page.";

        //    return View();
        //}

        //public ActionResult Blog()
        //{
        //    ViewBag.Message = "Your blog page.";

        //    return View();
        //}

        //public ActionResult BlogEntry()
        //{
        //    ViewBag.Message = "Your blog page.";

        //    return View();
        //}

        public ActionResult UnderConstruction()
        {
           
            return View();
        }
    }
}