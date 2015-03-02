using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KPWebApp.WebUI.Controllers
{
    public class HomeNavigationController : Controller
    {

        public PartialViewResult MainMenu()
        {
            return PartialView();
        }

    }
}
