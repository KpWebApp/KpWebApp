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
    public class PersonalController : Controller
    {
        private IRepository repository;

        public PersonalController(IRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Profile(string username)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            model.Username =  username;
            if (!string.IsNullOrEmpty(repository.GetUserByName(username).FullName))
            {
                string[] names = repository.GetUserByName(username).FullName.Split(new char[] {' '});
                if (names.Length == 3)
                {
                    model.FullName = names[1] + " " + names[0];
                }
                else
                {
                    model.FullName = repository.GetUserByName(username).FullName;
                }
            }
            else
            {
                model.FullName = username;
            }
            if (repository.GetUserByName(username).UserInfo != null)
            {
                model.Photos = repository.GetUserByName(username).UserInfo.Photos ?? null;
                model.Status = repository.GetUserByName(username).UserInfo.Status ?? null;
            }
            model.Posts = repository.GetUserByName(username).Posts ?? null;
            ViewBag.User = username;
            return View(model);
        }

        public ViewResult Biography(string username)
        {
            UserBiographyViewModel model = new UserBiographyViewModel();
            model.Username = username;
            string[] names = repository.GetUserByName(username).FullName.Split(new char[] { ' ' });
            if (names.Length == 3)
            {
                model.FullName = names[1] + " " + names[0];
            }
            else
            {
                model.FullName = repository.GetUserByName(username).FullName;
            }
            model.Bio = repository.GetUserByName(username).UserInfo.BIO;
            model.Photos = repository.GetUserByName(username).UserInfo.Photos;
            ViewBag.User = username;
            return View(model);
        }

        public ViewResult Contacts(string username)
        {
            UserContactViewModel model = new UserContactViewModel();
            model.Username = username;
            string[] names = repository.GetUserByName(username).FullName.Split(new char[] { ' ' });
            if (names.Length == 3)
            {
                model.FullName = names[1] + " " + names[0];
            }
            else
            {
                model.FullName = repository.GetUserByName(username).FullName;
            }
            model.Email = repository.GetUserByName(username).Email;
            model.Facebook = repository.GetUserByName(username).UserInfo.ContactInfo.Facebook;
            model.Skype = repository.GetUserByName(username).UserInfo.ContactInfo.Skype;
            model.HomeNumber = repository.GetUserByName(username).UserInfo.ContactInfo.HomeNumber;
            model.MobileNumber = repository.GetUserByName(username).UserInfo.ContactInfo.MobNumber;
            ViewBag.User = username;
            return View(model);
        }

        public FileContentResult GetImage(int imageId, string username)
        {
            User user = repository.UserCollection.FirstOrDefault(u => u.Username == username);
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
