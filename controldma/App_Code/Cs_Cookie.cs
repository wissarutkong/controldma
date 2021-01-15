using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace controldma.App_Code
{
    public class Cs_Cookie
    {
        public Cs_Cookie() {
            //
        }

        public void CreateCookie() {
            HttpCookie cookie = new HttpCookie("dma");
            cookie.Values.Add("_area", "");
            cookie.Values.Add("_wwcode", "");
        }

        public void ClearCookie() {
            HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["dma"];
            HttpContext.Current.Response.Cookies.Remove("dma");
            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
            currentUserCookie.Value = null;
            HttpContext.Current.Response.SetCookie(currentUserCookie);
        }

    }
}