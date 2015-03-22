using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.DAL;
using KPWebApp.Domain.Entities;

namespace KPWebApp.BLL
{
    public static class PersonalEdit
    {
        public static void SavePhoto(Photo photo, string username, IUnitOfWork unit)
        {
            using (var unitOfWork = unit)
            {
                if (photo.PhotoId == 0)
                {
                    var userinfo = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();
                    if (userinfo != null)
                    {
                        photo.UserInfo = userinfo.UserInfo;
                    }
                }
                else
                {
                    unitOfWork.PhotoRepository.Update(photo);
                }
                unitOfWork.Save();
            }
        }

        public static void SavePost(Post post, string username, IUnitOfWork unit)
        {
            using (var unitOfWork = unit)
            {
                if (post.PostId == 0)
                {
                    post.Time = DateTime.Now;
                    var user = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();
                    if (user != null)
                    {
                        user.Posts.Add(post);
                    }
                }
                else
                {
                    unitOfWork.PostRepository.Update(post);
                }
                unitOfWork.Save();
            }
        }

        public static void SaveContactData(ContactInfo cInfo, string username, IUnitOfWork unit)
        {
            using (var unitOfWork = unit)
            {
                var user = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();
                if (user != null)
                {
                    user.UserInfo.ContactInfo = cInfo;
                }
                unitOfWork.Save();
            }
        }

    }
}
