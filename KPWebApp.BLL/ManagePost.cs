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
    public class ManagePosts : IManagePosts
    {
        public List<Post> PostsByAdmin(string category, int pageCapacity, int page)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var posts =
                    unitOfWork.PostRepository.Get(
                        p =>
                            (category == null || p.Category.ToString() == category) &&
                            (p.User.Role == Role.Administrator), q => q.OrderByDescending(p => p.Time))
                        .Skip((page - 1) * pageCapacity)
                        .Take(pageCapacity);
                return posts.ToList();
            }
        }

        public int GetCountOfPosts()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var countOfPosts = unitOfWork.PostRepository.Get().Count();
                return countOfPosts;
            }
        }

        public int GetCountOfPostsByAdmin(string category)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var countOfPosts =
                    unitOfWork.PostRepository.Get()
                        .Count(e => e.Category.ToString() == category && e.User.Role == Role.Administrator);
                return countOfPosts;
            }
        }

        public int GetCountOfPostsByAdmin()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var countOfPosts =
                    unitOfWork.PostRepository.Get()
                        .Count(e => e.User.Role == Role.Administrator);
                return countOfPosts;
            }
        }

        public FileContentResult GetPhotoForPost(int postId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var photo =
                    unitOfWork.PostRepository.Get(p => p.PostId == postId)
                        .Select(p => new { Data = p.ImageData, Type = p.ImageMimeType })
                        .FirstOrDefault();
                if (photo != null)
                {
                    return new FileContentResult(photo.Data, photo.Type);
                }
            }

            return null;
        }

        public User GetTeacherById(int teacherId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.UserRepository.GetById(teacherId);
            }
        }

        public FileContentResult GetTeachersProfileImage(int teacherId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var photo =
                    unitOfWork.PhotoRepository.Get(p => p.UserInfo.UserInfoId == teacherId && p.IsProfilePhotoOfTeacher)
                        .Select(p => new { Data = p.ImageData, Type = p.ImageMimeType })
                        .FirstOrDefault();
                if (photo != null)
                {
                    return new FileContentResult(photo.Data, photo.Type);
                }
            }
            return null;
        }
    }
}
