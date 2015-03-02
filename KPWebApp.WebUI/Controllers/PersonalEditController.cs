using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using KPWebApp.BLL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class PersonalEditController : Controller
    {
        private IRepository repository;
        private string username;

        public PersonalEditController(IRepository repo)
        {
            this.repository = repo;
            this.username = System.Web.HttpContext.Current.User.Identity.Name;
        }

        public ViewResult Index()
        {
            ViewBag.User = username;
            return View();
        }

        public ViewResult Photos()
        {
            var user = (from u in repository.UserCollection
                        where u.Username == username
                        select u).FirstOrDefault();
            if (user != null)
            {
                var photos = user.UserInfo.Photos;
                ViewBag.User = username;
                return View(photos);
            }
            ViewBag.User = username;
            return View();
        }

        public ViewResult HeaderEdit(int photoId)
        {
            ViewBag.User = username;
            return View(repository.GetPhotoById(photoId));
        }

        [HttpPost]
        public ActionResult HeaderEdit(Photo photo)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(photo.PhotoHeader))
                {
                    repository.SavePhoto(photo, username);
                }
                //TempData["message"] = string.Format("Photo header changed");
                ViewBag.User = username;
                return RedirectToAction("Photos");
            }
            ViewBag.User = username;
            return View(photo);
        }

        public FileContentResult GetImage(int imageId)
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

        [HttpPost]
        public ActionResult Delete(int photoId)
        {
            Photo deletetedPhoto = repository.DeletePhoto(photoId);
            if (deletetedPhoto != null)
            {
                //TempData["message"] =
                //        string.Format("Photo with header: '{0}' has been deleted", deletetedPhoto.PhotoHeader);
            }
            ViewBag.User = username;
            return RedirectToAction("Photos");
        }

        public ViewResult AddNewPhoto(int photoId = 0)
        {
            ViewBag.User = username;
            return View(new Photo());
        }

        [HttpPost]
        public ActionResult AddNewPhoto(Photo photo, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(photo.ImageData, 0, image.ContentLength);
                    repository.SavePhoto(photo, username);
                    ViewBag.User = username;
                    return RedirectToAction("Photos");
                }
            }
            ViewBag.User = username;
            return View();
        }

        public ViewResult Posts()
        {
            ViewBag.User = username;
            return View(repository.GetUserByName(username).Posts);
        }

        public ViewResult Edit(int postId)
        {
            if (username == repository.GetPostById(postId).User.Username)
            {
                Post post = repository.GetPostById(postId);
                return View(post);
            }
            else
            {
                throw new Exception();
            }
           
        }

        [HttpPost]
        public ActionResult Edit(Post post, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    post.ImageMimeType = image.ContentType;
                    post.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(post.ImageData, 0, image.ContentLength);
                }

                User user = repository.GetUserByName(username);
                repository.SavePost(post, user);

                TempData["message"] = string.Format("Post with ID: {0} has been saved by {1}", post.PostId, System.Web.HttpContext.Current.User.Identity.Name);
                return RedirectToAction("Posts");
            }
            else
            {
                return View(post);
            }
        }

        public ViewResult CreatePost()
        {
            return View("Edit", new Post());
        }

        [HttpPost]
        public ActionResult DeletePost(int postId)
        {
            Post deletedPost = repository.DeletePost(postId);
            if (deletedPost != null)
            {
                //TempData["message"] =
                //        string.Format("Post with ID: {0} and HEADER: {1}, has been deleted", deletedPost.PostId, deletedPost.Header);
            }
            return RedirectToAction("Posts");
        }

        public ViewResult EditPersonalData()
        {
            PersonalData data = new PersonalData();
            User currentUser = repository.GetUserByName(username);
            if (!string.IsNullOrEmpty(currentUser.FullName))
            {
                string[] names = currentUser.FullName.Split(new char[] { ' ' });
                data.LastName = names[0];
                data.FirstName = names[1];
                if (names.Length == 3)
                {
                    data.MiddleName = names[2];
                }
            }
            data.Status = currentUser.UserInfo.Status;
            data.Gender = currentUser.UserInfo.Gender;
            //data.BirthDate = currentUser.UserInfo.BirthDate;
            data.Biography = currentUser.UserInfo.BIO;
            ViewBag.User = username;
            return View(data);
        }

        [HttpPost]
        public ActionResult EditPersonalData(PersonalData data)
        {
            if (ModelState.IsValid)
            {
                User user = repository.GetUserByName(username);
                user.FullName = data.LastName + " " + data.FirstName + " " + data.MiddleName;
                user.UserInfo.Status = data.Status;
                user.UserInfo.BIO = data.Biography;
                user.UserInfo.Gender = data.Gender;
                //user.UserInfo.BirthDate = data.BirthDate;
                repository.SaveUsersPersonalData(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(data);
            }

        }

        public ViewResult EditContactInfo()
        {
            ContactInfo info = repository.GetUserByName(username).UserInfo.ContactInfo;
            ViewBag.User = username;
            return View(info);
        }

        [HttpPost]
        public ActionResult EditContactInfo(ContactInfo info)
        {
            if (ModelState.IsValid)
            {
                repository.SaveUsersContactInfo(info, username);
                return RedirectToAction("Index");
            }
            else
            {
                return View(info);
            }
        }

        public ViewResult ChangePassword()
        {

            return View(new ChangingPassword());
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangingPassword model)
        {
            if (ModelState.IsValid)
            {
                if (repository.ChangePassword(System.Web.HttpContext.Current.User.Identity.Name, Security.HashPassword(model.OldPassword),
                    Security.HashPassword(model.NewPassword)))
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "Невірний пароль";
            }

            return View();
        }
    }
}
