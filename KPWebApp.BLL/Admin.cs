using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.DAL;
using KPWebApp.Domain.Entities;

namespace KPWebApp.BLL
{
    public static class Admin
    {
        public static void SavePost(Post post, User user, IUnitOfWork unit)
        {
            using (var unitOfWork = unit)
            {
                if (post.PostId == 0)
                {
                    post.Time = DateTime.Now;
                    var us = unitOfWork.UserRepository.Get(u => u.Username == user.Username).FirstOrDefault();
                    if (us != null)
                    {
                        us.Posts.Add(post);
                    }
                }
                else
                {
                    //Post dbEntry = context.Posts.Find(post.PostId);
                    //if (dbEntry != null)
                    //{
                    //    dbEntry.Header = post.Header;
                    //    dbEntry.Text = post.Text;
                    //    dbEntry.Category = post.Category;
                    //    dbEntry.ImageData = post.ImageData;
                    //    dbEntry.ImageMimeType = post.ImageMimeType;
                    //}
                    unitOfWork.PostRepository.Update(post);
                }
                unitOfWork.Save();
            }
        }

        public static void AddGraduate(int id, string fullName, int enranceYear, int graduationYear,
            Speciality speciality, IUnitOfWork unit)
        {
            using (var unitOfWork = unit)
            {
                User u = new User();
                u.UserInfo = new UserInfo();
                u.UserInfo.GraduateInfo = new GraduateInfo();
                //u.UserInfo.BirthDate = new DateTime(1,1,1,0,0,0);
                u.FullName = fullName;
                u.UserInfo.GraduateInfo.EntranceYear = enranceYear;
                u.UserInfo.GraduateInfo.GraduationYear = graduationYear;
                u.UserInfo.GraduateInfo.Speciality = speciality;
                if (id == 0)
                {
                    unitOfWork.UserRepository.Insert(u);
                }
                else
                {
                    //u.UserInfo.BirthDate = new DateTime(1, 1, 1, 0, 0, 0);
                    u = unitOfWork.UserRepository.GetById(id);
                    u.FullName = fullName;
                    u.UserInfo.GraduateInfo.EntranceYear = enranceYear;
                    u.UserInfo.GraduateInfo.GraduationYear = graduationYear;
                    u.UserInfo.GraduateInfo.Speciality = speciality;
                }

                unitOfWork.Save();
            }
        }

        public static void AddTeacher(int id, string fullName, string chair, string degree, ICollection<string> courses,
            DateTime? from, DateTime? till, byte[] photoData, string imageType, IUnitOfWork unit)
        {
            using (var unitOfWork = unit)
            {
                User dbEntry = new User();
                dbEntry.FullName = fullName;
                dbEntry.IsTeacher = true;
                dbEntry.UserInfo = new UserInfo();
                dbEntry.UserInfo.Teacher = new Teacher();
                dbEntry.UserInfo.Teacher.Chiar = chair;
                dbEntry.UserInfo.Teacher.Degree = degree;
                dbEntry.UserInfo.Teacher.WorksFrom = from;
                dbEntry.UserInfo.Teacher.WorkedTill = till;
                if (photoData != null)
                {
                    dbEntry.UserInfo.Photos.Add(new Photo { AddedByAdmin = true, IsProfilePhotoOfTeacher = true, ImageData = photoData, ImageMimeType = imageType, PhotoHeader = fullName });
                }

                dbEntry.UserInfo.Courses = new Collection<Course>();
                foreach (Course course in unitOfWork.CourseRepository.Get())
                {
                    if (courses.Contains(course.Name))
                    {
                        dbEntry.UserInfo.Courses.Add(course);
                        courses.Remove(course.Name);
                    }
                }
                foreach (string courseName in courses)
                {

                    if (!string.IsNullOrEmpty(courseName))
                    {
                        dbEntry.UserInfo.Courses.Add(new Course { Name = courseName });
                    }
                }

                if (id == 0)
                {

                    unitOfWork.UserRepository.Insert(dbEntry);
                }
                else
                {
                    unitOfWork.UserRepository.Update(dbEntry);
                }
                unitOfWork.Save();
            }
        }

        public static bool ChangePassword(string username, string oldPassword, string newPassword, IUnitOfWork unit)
        {
            using (var unitOfWork = unit)
            {
                User u = unitOfWork.UserRepository.Get(us => us.Username == username).FirstOrDefault();
                if (u.Password != oldPassword)
                {
                    return false;
                }
                else
                {
                    u.Password = newPassword;
                }
                unitOfWork.Save();
                return u.Password == newPassword;
            }
        }
    }
}
