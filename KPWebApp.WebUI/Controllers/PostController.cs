using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPWebApp.BLL;
using KPWebApp.DAL;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;
using KPWebApp.WebUI.Models;

namespace KPWebApp.WebUI.Controllers
{
    public class PostController : Controller
    {
        private IManagePosts postManager;
        public int PageCapacity { get; set; }
        public PostController(IManagePosts manager)
        {
            this.postManager = manager;
            this.PageCapacity = 3;
        }

        public ViewResult List(string category, int page = 1)
        {
            PostsListViewModel model = new PostsListViewModel()
            {
                Posts = postManager.PostsByAdmin(category, PageCapacity, page),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageCapacity,
                    TotalItems = category == null ? postManager.GetCountOfPostsByAdmin() : postManager.GetCountOfPostsByAdmin(category)
                },
                CurrentCategory = category

            };
            return View(model);
        }

        public FileContentResult GetImage(int postId)
        {
            return postManager.GetPhotoForPost(postId);
        }

        public ViewResult TeachersInfoPage(int userId)
        {
            User teacher = postManager.GetTeacherById(userId);
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
            return postManager.GetTeachersProfileImage(userId);
        }

    }
}
