using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using KPWebApp.BLL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Authorize]
    public class AdminController : Controller
    {
        public int PageCapacity { get; set; }

        private IRepository repository;

        public AdminController(IRepository reposit)
        {
            this.repository = reposit;
            this.PageCapacity = 10;
        }

        public ViewResult ListOfPosts(int page = 1)
        {
            AdminsListOfItems<Post> model = new AdminsListOfItems<Post>
            {
                Items = repository.GetPostsByRole(Role.Administrator).OrderByDescending(p => p.Time)
                    .Skip((page - 1) * PageCapacity)
                    .Take(PageCapacity),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageCapacity,
                    TotalItems = repository.PostsCollection.Count(p => p.User.Role == Role.Administrator)
                },
            };
            return View(model);
        }

        public ViewResult EditPost(int postId)
        {
            Post post = repository.GetPostById(postId);
            return View(post);
        }

        [HttpPost]
        public ActionResult EditPost(Post post, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    post.ImageMimeType = image.ContentType;
                    post.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(post.ImageData, 0, image.ContentLength);
                }

                using (var ctx = new KpWebAppDb())
                {
                    User user = (from u in ctx.Users
                                 where u.Username == System.Web.HttpContext.Current.User.Identity.Name
                                 select u).FirstOrDefault();
                    repository.SavePost(post, user);
                }

                //TempData["message"] = string.Format("Post with ID: {0} has been saved by {1}", post.PostId, System.Web.HttpContext.Current.User.Identity.Name);
                return RedirectToAction("ListOfPosts");
            }
            else
            {
                return View(post);
            }
        }

        public ViewResult CreatePost()
        {
            return View("EditPost", new Post());
        }

        [HttpPost]
        public ActionResult DeletePost(int postId)
        {
            Post deletedPost = repository.DeletePost(postId);
            //if (deletedPost != null)
            //{
            //    TempData["message"] =
            //            string.Format("Post with ID: {0} and HEADER: {1}, has been deleted", deletedPost.PostId, deletedPost.Header);
            //}
            return RedirectToAction("ListOfPosts");
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult EditGraduate(int userId)
        {
            User graduate = new User();
            graduate = repository.GetUserById(userId);
            NewGraduate entry = new NewGraduate();
            entry.UserId = graduate.UserId;
            string[] names = graduate.FullName.Split(' ');
            entry.FirstName = names[1];
            entry.LastName = names[0];
            if (names.Length == 3)
            {
                entry.MiddleName = names[2];
            }
            entry.EntarnceYear = graduate.UserInfo.GraduateInfo.EntranceYear;
            entry.GraduationYear = graduate.UserInfo.GraduateInfo.GraduationYear;
            entry.Speciality = graduate.UserInfo.GraduateInfo.Speciality;
            return View(entry);
        }

        [HttpPost]
        public ActionResult EditGraduate(NewGraduate graduate)
        {
            if (ModelState.IsValid)
            {
                string fullName = string.Empty;
                fullName += graduate.LastName;
                fullName += (" " + graduate.FirstName);

                if (!string.IsNullOrEmpty(graduate.MiddleName))
                {
                    fullName += (" " + graduate.MiddleName);
                }

                repository.AddGraduate(graduate.UserId, fullName, graduate.EntarnceYear, graduate.GraduationYear, graduate.Speciality);
                return RedirectToAction("ListOfAllUsers");
            }
            return View("EditGraduate", graduate);
        }

        public ViewResult AddGraduate()
        {
            return View("EditGraduate", new NewGraduate());
        }

        public ViewResult ListOfTeachers()
        {
            return View(repository.GetAllTeachers());
        }

        public ViewResult EditTeacher(int userId)
        {
            User teacher = new User();
            teacher = repository.GetUserById(userId);
            TeacherByAdmin entry = new TeacherByAdmin();
            entry.UserId = teacher.UserId;
            entry.Chair = teacher.UserInfo.Teacher.Chiar;
            entry.Degree = teacher.UserInfo.Teacher.Degree;
            entry.WorkedTill = teacher.UserInfo.Teacher.WorkedTill;
            entry.WorksFrom = teacher.UserInfo.Teacher.WorksFrom;
            Photo photo = (from p in teacher.UserInfo.Photos
                           where p.IsProfilePhotoOfTeacher
                           select p).FirstOrDefault();
            if (photo == null)
            {
                photo = (from p in teacher.UserInfo.Photos
                         where p.AddedByAdmin
                         select p).FirstOrDefault();
            }
            if (photo != null)
            {
                entry.ImageData = photo.ImageData;
                entry.ImageMimeType = photo.ImageMimeType;
            }
            foreach (Course course in teacher.UserInfo.Courses)
            {
                entry.Courses += course.Name + "\n";
            }
            string[] names = teacher.FullName.Split(' ');
            entry.FirstName = names[1];
            entry.LastName = names[0];
            if (names.Length == 3)
            {
                entry.MiddleName = names[2];
            }
            return View(entry);
        }

        public ViewResult AddTeacher()
        {
            return View("EditTeacher", new TeacherByAdmin());
        }

        [HttpPost]
        public ActionResult EditTeacher(TeacherByAdmin teacher, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    teacher.ImageMimeType = image.ContentType;
                    teacher.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(teacher.ImageData, 0, image.ContentLength);
                }
                string fullName = string.Empty;
                fullName += teacher.LastName;
                fullName += " ";
                fullName += teacher.FirstName;
                if (!string.IsNullOrEmpty(teacher.MiddleName))
                {
                    fullName += " ";
                    fullName += teacher.MiddleName;
                }
                List<string> courses = new List<string>();
                if (!string.IsNullOrEmpty(teacher.Courses))
                {
                    string[] addedCourses = teacher.Courses.Split(new char[] { '\n' });

                    courses.AddRange(addedCourses);
                }
                repository.AddTeacher(teacher.UserId, fullName, teacher.Chair, teacher.Degree, courses, teacher.WorksFrom, teacher.WorkedTill, teacher.ImageData, teacher.ImageMimeType);
                return RedirectToAction("ListOfTeachers");
            }
            return View("EditTeacher", teacher);
        }

        public FileContentResult GetImageForTeacher(int userId)
        {
            User user = repository.UserCollection.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                Photo photo = user.UserInfo.Photos.FirstOrDefault();
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
        public ActionResult DeleteTeacher(int userId)
        {
            User deletedUser = repository.DeleteTeacher(userId);
            if (deletedUser != null)
            {
                //TempData["message"] =
                //        string.Format("Post with ID: {0} and FULL NAME: {1}, has been deleted", deletedUser.UserId, deletedUser.FullName);
            }
            return RedirectToAction("ListOfTeachers");
        }

        [HttpPost]
        public ActionResult DeleteGraduate(int userId)
        {
            User deletedUser = repository.DeleteTeacher(userId);
            if (deletedUser != null)
            {
                //TempData["message"] =
                //        string.Format("Post with ID: {0} and FULL NAME: {1}, has been deleted", deletedUser.UserId, deletedUser.FullName);
            }
            return RedirectToAction("ListOfAllUsers");
        }
        public ViewResult ListOfAllUsers(int page = 1)
        {
            AdminsListOfItems<User> model = new AdminsListOfItems<User>
            {
                Items = repository.GetAllGraduates()
                    .Skip((page - 1) * PageCapacity)
                    .Take(PageCapacity),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageCapacity,
                    TotalItems = repository.UserCollection.Count(p => p.Role != Role.Administrator && !p.IsTeacher)
                },
            };
            return View(model);
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
