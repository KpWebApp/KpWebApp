using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KPWebApp.BLL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Infrastructure.Abstract;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthorizationProvider authorizationProvider;
        private IRepository repository;
        private IRegistrationProcessor registration;

        public AccountController(IAuthorizationProvider auth, IRepository repo, IRegistrationProcessor register)
        {
            repository = repo;
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
                    repository.GetUserByFullName(string.Format("{0} {1} {2}", user.LastName, user.FirstName,
                        user.MiddleName));
                if (userEntry != null)
                {
                    bool isRegistred = repository.Register(userEntry.UserId, user.Username, user.Email, Security.HashPassword(user.Password));
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

    }
}
