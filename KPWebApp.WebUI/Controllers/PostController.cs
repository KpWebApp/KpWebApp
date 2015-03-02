using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    public class PostController : Controller
    {
        private IRepository repository;
        public int PageCapacity { get; set; }
        public PostController(IRepository postRepository)
        {
            this.repository = postRepository;
            this.PageCapacity = 3;
        }

        public ViewResult List(string category, int page = 1)
        {
            PostsListViewModel model = new PostsListViewModel()
            {
                Posts = repository.PostsCollection
                    .Where(p => (category == null || p.Category.ToString() == category) && (p.User.Role == Role.Administrator))
                    .OrderByDescending(p => p.Time)
                    .Skip((page - 1) * PageCapacity)
                    .Take(PageCapacity),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageCapacity,
                    TotalItems = category == null ? repository.PostsCollection.Count() : repository.PostsCollection.Count(e => e.Category.ToString() == category && e.User.Role==Role.Administrator)
                },
                CurrentCategory = category

            };
            return View(model);
        }

        public FileContentResult GetImage(int postId)
        {
            Post post = repository.PostsCollection.FirstOrDefault(p => p.PostId == postId);
            if (post != null)
            {
                return File(post.ImageData, post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ViewResult TeachersInfoPage(int userId)
        {
            User teacher = repository.GetUserById(userId);
            TeacherInfoPage modelTeacher = new TeacherInfoPage
            {
                UserId = teacher.UserId,
                Chair = teacher.UserInfo.Teacher.Chiar,
                Degree = teacher.UserInfo.Teacher.Degree,
                Courses = teacher.UserInfo.Courses,
                WorkedTill = teacher.UserInfo.Teacher.WorkedTill,
                WorksFrom = teacher.UserInfo.Teacher.WorksFrom,
                FullName = teacher.FullName,
                ProfilePhoto = teacher.UserInfo.Photos.FirstOrDefault(x => x.IsProfilePhotoOfTeacher)
            };

            return View(modelTeacher);
        }

        public FileContentResult GetTeachersProfileImage(int userId)
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

    }
}
