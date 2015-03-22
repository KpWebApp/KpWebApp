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
    public class PersonalController : Controller
    {
        private IUnitOfWork unitOfWork;

        public PersonalController(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public ViewResult Profile(string username)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            model.Username =  username;
            var user = unitOfWork.UserRepository.Get(u=>u.Username==username).FirstOrDefault();
            if (user != null && !string.IsNullOrEmpty(user.FullName))
            {
                string[] names = user.FullName.Split(new char[] {' '});
                if (names.Length == 3)
                {
                    model.FullName = names[1] + " " + names[0];
                }
                else
                {
                    model.FullName = user.FullName;
                }
                if (user.UserInfo != null)
                {
                    model.Photos = user.UserInfo.Photos ?? null;
                    model.Status = user.UserInfo.Status ?? null;
                }
                model.Posts = user.Posts ?? null;
            }
            else
            {
                model.FullName = username;
            }
          
            ViewBag.User = username;
            return View(model);
        }

        public ViewResult Biography(string username)
        {
            UserBiographyViewModel model = new UserBiographyViewModel();
            model.Username = username;
            var user = unitOfWork.UserRepository.Get(u=>u.Username==username).FirstOrDefault();
            if (user != null)
            {
                string[] names = user.FullName.Split(new char[] { ' ' });
                if (names.Length == 3)
                {
                    model.FullName = names[1] + " " + names[0];
                }
                else
                {
                    model.FullName = user.FullName;
                }
                model.Bio = user.UserInfo.BIO;
                model.Photos = user.UserInfo.Photos;
            }
            ViewBag.User = username;
            return View(model);
        }

        public ViewResult Contacts(string username)
        {
            UserContactViewModel model = new UserContactViewModel();
            model.Username = username;
            var user = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                string[] names = user.FullName.Split(new char[] { ' ' });
                if (names.Length == 3)
                {
                    model.FullName = names[1] + " " + names[0];
                }
                else
                {
                    model.FullName = user.FullName;
                }
                model.Email = user.Email;
                model.Facebook = user.UserInfo.ContactInfo.Facebook;
                model.Skype = user.UserInfo.ContactInfo.Skype;
                model.HomeNumber = user.UserInfo.ContactInfo.HomeNumber;
                model.MobileNumber = user.UserInfo.ContactInfo.MobNumber;
            }        
            ViewBag.User = username;
            return View(model);
        }

        public FileContentResult GetImage(int imageId, string username)
        {
            User user = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                Photo photo = user.UserInfo.Photos.FirstOrDefault(p => p.PhotoId == imageId);
                if (photo != null)
                {
                    return File(photo.ImageData, photo.ImageMimeType);
                }
                else
                {
                    return null;
                }
            }
            return null;              
        }

        
    }
}
