using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using KPWebApp.BLL;
using KPWebApp.DAL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class PersonalEditController : Controller
    {
        private IUnitOfWork unitOfWork;
        private string username;

        public PersonalEditController(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
            this.username = System.Web.HttpContext.Current.User.Identity.Name;
        }

        public ViewResult Index()
        {
            ViewBag.User = username;
            return View();
        }

        public ViewResult Photos()
        {
            var user = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();
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
            return View(unitOfWork.PhotoRepository.GetById(photoId));
        }

        [HttpPost]
        public ActionResult HeaderEdit(Photo photo)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(photo.PhotoHeader))
                {
                    BLL.PersonalEdit.SavePhoto(photo, username, this.unitOfWork);
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

        [HttpPost]
        public ActionResult Delete(int photoId)
        {
            unitOfWork.PhotoRepository.Delete(photoId);
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
                    BLL.PersonalEdit.SavePhoto(photo, username, this.unitOfWork);
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
            var user = unitOfWork.UserRepository.Get(u=>u.Username==username).FirstOrDefault();
            if (user != null)
            {
                return View(user.Posts);
            }
            return View();
        }
        
        public ViewResult Edit(int postId)
        {
            if (username == unitOfWork.PostRepository.GetById(postId).User.Username)
            {
                Post post = unitOfWork.PostRepository.GetById(postId);
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

                User user = unitOfWork.UserRepository.Get(u=>u.Username==username).FirstOrDefault();
                PersonalEdit.SavePost(post, username, this.unitOfWork);

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
            unitOfWork.PostRepository.Delete(postId);
            return RedirectToAction("Posts");
        }

        public ViewResult EditPersonalData()
        {
            PersonalData data = new PersonalData();
            User currentUser = unitOfWork.UserRepository.Get(u=>u.Username==username).FirstOrDefault();
            if (currentUser!=null && !string.IsNullOrEmpty(currentUser.FullName))
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
                User user = unitOfWork.UserRepository.Get(u=>u.Username==username).FirstOrDefault();
                if (user != null)
                {
                    user.FullName = data.LastName + " " + data.FirstName + " " + data.MiddleName;
                    user.UserInfo.Status = data.Status;
                    user.UserInfo.BIO = data.Biography;
                    user.UserInfo.Gender = data.Gender;
                    //user.UserInfo.BirthDate = data.BirthDate;
                }
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(data);
            }

        }

        public ViewResult EditContactInfo()
        {
            ViewBag.User = username;
            var user = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                ContactInfo info = user.UserInfo.ContactInfo;
                return View(info);
            }
            return View();

        }

        [HttpPost]
        public ActionResult EditContactInfo(ContactInfo info)
        {
            if (ModelState.IsValid)
            {
                BLL.PersonalEdit.SaveContactData(info, username, this.unitOfWork);
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
                if (Admin.ChangePassword(System.Web.HttpContext.Current.User.Identity.Name, Security.HashPassword(model.OldPassword),
                    Security.HashPassword(model.NewPassword), this.unitOfWork))
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "Невірний пароль";
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            this.unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
