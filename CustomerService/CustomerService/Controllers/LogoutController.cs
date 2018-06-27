using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerService.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session.Clear();
            return PartialView("DeniedPage");
        }

    }
}
