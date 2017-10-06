using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semillitas.Web.Controllers
{
    public class CookiesController : Controller
    {
        [HttpPost]
        public bool Accept()
        {
            try
            {
                // If the cookie exists
                if (HttpContext.Request.Cookies.AllKeys.Contains("SemillitasCookie"))
                {
                    // Modifying the existing cookie
                    HttpCookie semillitasCookie = HttpContext.Request.Cookies["SemillitasCookie"];
                    semillitasCookie.Values["IsAccepted"] = "true";
                    semillitasCookie.Values["LastVisit"] = DateTime.Now.ToString();
                    semillitasCookie.Expires = DateTime.Now.AddDays(30);

                    HttpContext.Response.Cookies.Add(semillitasCookie);

                }
                // If the cookie does not exist
                else
                {
                    // Creating a new cookie
                    HttpCookie newCookie = new HttpCookie("SemillitasCookie");
                    newCookie.Values["IsAccepted"] = "true";
                    newCookie.Values["LastVisit"] = DateTime.Now.ToString();
                    newCookie.Expires = DateTime.Now.AddDays(30);

                    HttpContext.Response.Cookies.Add(newCookie);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public bool HideMarketing()
        {
            try
            {
                // If the cookie exists
                if (HttpContext.Request.Cookies.AllKeys.Contains("SemillitasCookie"))
                {
                    // Modifying the existing cookie
                    HttpCookie semillitasCookie = HttpContext.Request.Cookies["SemillitasCookie"];
                    semillitasCookie.Values["LastVisit"] = DateTime.Now.ToString();
                    semillitasCookie.Values["HideMarketing"] = "true";
                    semillitasCookie.Expires = DateTime.Now.AddDays(30);

                    HttpContext.Response.Cookies.Add(semillitasCookie);

                }
                // If the cookie does not exist
                else
                {
                    // Creating a new cookie
                    HttpCookie newCookie = new HttpCookie("SemillitasCookie");
                    newCookie.Values["HideMarketing"] = "true";
                    newCookie.Values["LastVisit"] = DateTime.Now.ToString();
                    newCookie.Expires = DateTime.Now.AddDays(30);

                    HttpContext.Response.Cookies.Add(newCookie);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public ActionResult Remove()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("SemillitasCookie"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["SemillitasCookie"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}