using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KPWebApp.WebUI.Controllers
{
    public class PersonalNavigationController : Controller
    {
        public PartialViewResult Menu(string username, string category = null)
        {
            ViewBag.Username = username;
            ViewBag.SelectedCategory = category;
            return PartialView();
        }

    }
}
