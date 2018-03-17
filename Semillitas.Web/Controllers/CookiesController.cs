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
                if (HttpContext.Request.Cookies.AllKeys.Contains("cookie.consent"))
                {
                    // Modifying the existing cookie
                    HttpCookie semillitasCookie = HttpContext.Request.Cookies["cookie.consent"];
                    semillitasCookie.Value = "true";
                    semillitasCookie.Expires = DateTime.Now.AddDays(30);

                    HttpContext.Response.Cookies.Add(semillitasCookie);

                }
                // If the cookie does not exist
                else
                {
                    // Creating a new cookie
                    HttpCookie newCookie = new HttpCookie("cookie.consent");
                    newCookie.Value = "true";
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
                if (HttpContext.Request.Cookies.AllKeys.Contains("subscription.visited"))
                {
                    // Modifying the existing cookie
                    HttpCookie semillitasCookie = HttpContext.Request.Cookies["subscription.visited"];
                    semillitasCookie.Value = "true";
                    semillitasCookie.Expires = DateTime.Now.AddDays(1);

                    HttpContext.Response.Cookies.Add(semillitasCookie);

                }
                // If the cookie does not exist
                else
                {
                    // Creating a new cookie
                    HttpCookie newCookie = new HttpCookie("subscription.visited");
                    newCookie.Value = "true";
                    newCookie.Expires = DateTime.Now.AddDays(1);

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
            foreach (var cookieName in this.ControllerContext.HttpContext.Request.Cookies.AllKeys)
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies[cookieName];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}