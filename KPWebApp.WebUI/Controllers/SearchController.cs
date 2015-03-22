using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPWebApp.DAL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    public class SearchController : Controller
    {
        private IUnitOfWork unitOfWork;

        public SearchController(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
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
                    usersFromDb = unitOfWork.UserRepository.Get(u=>u.IsTeacher).ToList();
                }
                else
                {
                    int yearId = Convert.ToInt32(year);
                    usersFromDb = unitOfWork.UserRepository.Get(u=>u.UserInfo.GraduateInfo.GraduationYear == yearId).ToList();
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
            return PartialView(BLL.Search.YearsOfGraduation(unitOfWork));
        }

    }
}
