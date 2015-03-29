using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KPWebApp.DAL;
using KPWebApp.Domain.Entities;

namespace KPWebApp.BLL
{
    public class PostManager : IPostManager
    {
        private IUnitOfWork unitOfWork;
        private bool disposed = false;
        public PostManager(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }

        public List<Post> PostsByAdmin(string category, int pageCapacity, int page)
        {
            var posts =
                unitOfWork.PostRepository.Get(
                    p =>
                        (category == null || p.Category.ToString() == category) &&
                        (p.User.Role == Role.Administrator), null, "")
                    .Skip((page - 1) * pageCapacity)
                    .Take(pageCapacity).OrderByDescending(p=>p.Time);
            return posts.ToList();
        }

        public int GetCountOfPosts()
        {
            var countOfPosts = unitOfWork.PostRepository.Get().Count();
            return countOfPosts;
        }

        public int GetCountOfPostsByAdmin(string category)
        {
            var countOfPosts =
                unitOfWork.PostRepository.Get()
                    .Count(e => e.Category.ToString() == category && e.User.Role == Role.Administrator);
            return countOfPosts;

        }

        public int GetCountOfPostsByAdmin()
        {
            var countOfPosts =
                unitOfWork.PostRepository.Get()
                    .Count(e => e.User.Role == Role.Administrator);
            return countOfPosts;

        }

        public FileContentResult GetPhotoForPost(int postId)
        {
            var photo =
                unitOfWork.PostRepository.Get(p => p.PostId == postId)
                    .Select(p => new { Data = p.ImageData, Type = p.ImageMimeType })
                    .FirstOrDefault();
            if (photo != null)
            {
                return new FileContentResult(photo.Data, photo.Type);
            }

            return null;
        }

        public User GetTeacherById(int teacherId)
        {
            return unitOfWork.UserRepository.GetById(teacherId);
        }

        public FileContentResult GetTeachersProfileImage(int teacherId)
        {
            var photo =
                unitOfWork.PhotoRepository.Get(p => p.UserInfo.UserInfoId == teacherId && p.IsProfilePhotoOfTeacher)
                    .Select(p => new { Data = p.ImageData, Type = p.ImageMimeType })
                    .FirstOrDefault();
            if (photo != null)
            {
                return new FileContentResult(photo.Data, photo.Type);
            }

            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
