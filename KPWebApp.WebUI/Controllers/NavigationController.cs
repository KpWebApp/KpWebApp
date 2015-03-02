using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPWebApp.BLL;
using KPWebApp.Domain.Entities;

namespace KPWebApp.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = TextParser.TranslateCategoryToUkrainian(category);
            var res = Enum.GetValues(typeof (PostCategory));
            List<string> categoriesList = new List<string>();
            foreach (var postCategory in res)
            {
                categoriesList.Add(TextParser.TranslateCategoryToUkrainian(postCategory.ToString()));
            }
            return PartialView(categoriesList);
        }

    }
}
