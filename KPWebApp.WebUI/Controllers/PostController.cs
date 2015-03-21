using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPWebApp.DAL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    public class PostController : Controller
    {
        private IUnitOfWork unitOfWork;
        public int PageCapacity { get; set; }
        public PostController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.PageCapacity = 3;
        }

        public ViewResult List(string category, int page = 1)
        {
            PostsListViewModel model = new PostsListViewModel()
            {
                Posts = unitOfWork.PostRepository.Get(p => (category == null || p.Category.ToString() == category) && (p.User.Role == Role.Administrator), q=>q.OrderByDescending(p=>p.Time)).Skip((page - 1) * PageCapacity).Take(PageCapacity),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageCapacity,
                    TotalItems = category == null ? unitOfWork.PostRepository.Get().Count() : unitOfWork.PostRepository.Get().Count(e => e.Category.ToString() == category && e.User.Role==Role.Administrator)
                },
                CurrentCategory = category

            };
            return View(model);
        }

        public FileContentResult GetImage(int postId)
        {
            Post post = unitOfWork.PostRepository.Get(p => p.PostId == postId).FirstOrDefault();
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
            User teacher = unitOfWork.UserRepository.GetById(userId);
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
            User user = unitOfWork.UserRepository.Get(u => u.UserId == userId).FirstOrDefault();
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
