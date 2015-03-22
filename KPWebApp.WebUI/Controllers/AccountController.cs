using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KPWebApp.BLL;
using KPWebApp.DAL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Infrastructure.Abstract;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthorizationProvider authorizationProvider;
        private IRegistrationProcessor registration;
        private IUnitOfWork unitOfWork;

        public AccountController(IAuthorizationProvider auth, IUnitOfWork uof, IRegistrationProcessor register)
        {
            unitOfWork = uof;
            authorizationProvider = auth;
            registration = register;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Username, Security.HashPassword(model.Password)))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, true);
                    //return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                    return Redirect(Url.Action("Index", "Home"));
                }
                else
                {
                    ModelState.AddModelError("", "Невірне ім'я користувача або пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ViewResult Register()
        {

            return View(new RegistratingUser());
        }

        [HttpPost]
        public ActionResult Register(RegistratingUser user)
        {
            if (ModelState.IsValid)
            {
                User userEntry =
                    unitOfWork.UserRepository.Get(u=>u.FullName==string.Format("{0} {1} {2}", user.LastName, user.FirstName,
                        user.MiddleName)).FirstOrDefault();
                if (userEntry != null)
                {
                    bool isRegistred = Account.Register(userEntry.UserId, user.Username, user.Email, Security.HashPassword(user.Password), this.unitOfWork);
                    if (isRegistred)
                    {
                        registration.ProcessOrder(user.FirstName, user.MiddleName, user.LastName, user.Email, user.Username);
                        FormsAuthentication.SetAuthCookie(user.Username, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Невалідний логін";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Немає випускника з таким ім'ям.";
                    return View();
                }
            }
            
            ViewBag.Message = "Помилка заповнення форми.";
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}
