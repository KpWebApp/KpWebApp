using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    public class SearchController : Controller
    {
        private IRepository repository;

        public SearchController(IRepository reposit)
        {
            this.repository = reposit;
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Changed(string year)
        {
            try
            {
                if (year == "undefined")
                {
                    year = "0";
                }
                List<User> usersFromDb = new List<User>();
                if (year == "Викладачі")
                {
                    usersFromDb = repository.GetAllTeachers().ToList();
                }
                else
                {
                    int yearId = Convert.ToInt32(year);
                    usersFromDb = repository.GetByGraduateYear(yearId).ToList();
                }
                
                List<FoundUser> users = new List<FoundUser>();
                foreach (User user in usersFromDb)
                {
                    users.Add(new FoundUser{ UserId = user.UserId, FullName = user.FullName, Username = user.Username, IsRegistred = user.IsRegistred, IsTeacher = user.IsTeacher});
                }
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {

                return Redirect(@"Error");
            }

        }

        public PartialViewResult YearsOfGraduation()
        {
            return PartialView(repository.GetYearsOfGraduation().ToList());
        }

    }
}
