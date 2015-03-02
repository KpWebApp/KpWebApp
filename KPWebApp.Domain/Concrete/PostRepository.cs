using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Abstract;
using KPWebApp.Domain.Entities;

namespace KPWebApp.Domain.Concrete
{
    public class PostRepository : IRepository
    {
        private KpWebAppDb context = new KpWebAppDb();

        public IQueryable<Post> PostsCollection
        {
            get { return context.Posts; }
        }

        public IQueryable<User> UserCollection
        {
            get { return context.Users; }
        }

        public void SavePost(Post post, User user)
        {
            if (post.PostId == 0)
            {
                post.Time = DateTime.Now;
                var us = (from u in context.Users
                          where u.Username == user.Username
                          select u).FirstOrDefault();
                us.Posts.Add(post);
            }
            else
            {
                Post dbEntry = context.Posts.Find(post.PostId);
                if (dbEntry != null)
                {
                    dbEntry.Header = post.Header;
                    dbEntry.Text = post.Text;
                    dbEntry.Category = post.Category;
                    dbEntry.ImageData = post.ImageData;
                    dbEntry.ImageMimeType = post.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Post DeletePost(int id)
        {
            Post dbEntry = context.Posts.Find(id);
            if (dbEntry != null)
            {
                context.Posts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public User GetUserByName(string name)
        {
            var user = (from u in context.Users
                        where u.Username == name
                        select u).FirstOrDefault();
            return user;
        }

        public Post GetPostById(int id)
        {
            var post = (from u in context.Posts
                        where u.PostId == id
                        select u).FirstOrDefault();
            return post;
        }

        public Photo DeletePhoto(int id)
        {
            Photo dbEntry = context.Photos.Find(id);
            if (dbEntry != null)
            {
                context.Photos.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Photo GetPhotoById(int id)
        {
            Photo res = context.Photos.Find(id);
            return res;
        }

        public void SavePhoto(Photo photo, string username)
        {
            if (photo.PhotoId == 0)
            {
                User user = (from u in context.Users
                             where u.Username == username
                             select u).FirstOrDefault();
                photo.UserInfo = user.UserInfo;
                context.Photos.Add(photo);
            }
            else
            {
                Photo dbEntry = context.Photos.Find(photo.PhotoId);
                if (dbEntry != null)
                {
                    dbEntry.PhotoHeader = photo.PhotoHeader;
                }
            }
            context.SaveChanges();
        }

        public IQueryable<Post> GetPostsByRole(Role role)
        {
            return from post in PostsCollection
                   where post.User.Role == role
                   select post;
        }

        public void SaveUsersPersonalData(User user)
        {
            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.FullName = user.FullName;
                    dbEntry.UserInfo.Status = user.UserInfo.Status;
                    dbEntry.UserInfo.BIO = user.UserInfo.BIO;
                    dbEntry.UserInfo.Gender = user.UserInfo.Gender;
                    //dbEntry.UserInfo.BirthDate = user.UserInfo.BirthDate;
                }
            }
            context.SaveChanges();
        }

        public void SaveUsersContactInfo(ContactInfo info, string username)
        {
            var user = (from u in this.UserCollection
                        where u.Username == username
                        select u).FirstOrDefault();
            if (user != null)
            {
                user.UserInfo.ContactInfo = info;
            }
            context.SaveChanges();
        }

        public void AddGraduate(int id, string fullName, int enranceYear, int graduationYear, Speciality speciality)
        {
            if (id == 0)
            {
                User u = new User();
                u.UserInfo = new UserInfo();
                u.UserInfo.GraduateInfo = new GraduateInfo();
                //u.UserInfo.BirthDate = new DateTime(1,1,1,0,0,0);
                u.FullName = fullName;
                u.UserInfo.GraduateInfo.EntranceYear = enranceYear;
                u.UserInfo.GraduateInfo.GraduationYear = graduationYear;
                u.UserInfo.GraduateInfo.Speciality = speciality;
                this.context.Users.Add(u);
            }
            else
            {
                User u = context.Users.Find(id);
                //u.UserInfo.BirthDate = new DateTime(1,1,1,0,0,0);
                u.FullName = fullName;
                u.UserInfo.GraduateInfo.EntranceYear = enranceYear;
                u.UserInfo.GraduateInfo.GraduationYear = graduationYear;
                u.UserInfo.GraduateInfo.Speciality = speciality;
            }

            context.SaveChanges();
        }

        public User GetUserByFullName(string fullName)
        {
            var user = (from u in this.UserCollection
                        where String.Compare(u.FullName, fullName, StringComparison.CurrentCultureIgnoreCase) == 0
                        select u).FirstOrDefault();
            return user;
        }

        public bool Register(int id, string username, string email, string password)
        {
            if (this.UserCollection.Any(u => u.Username == username))
            {
                return false;
            }
            else
            {
                User entry = (from u in this.UserCollection
                              where u.UserId == id
                              select u).FirstOrDefault();
                if (entry == null)
                {
                    throw new Exception("Exception occured while registrating a new user.");
                }
                if (entry.IsRegistred)
                {
                    return false;
                }
                entry.Username = username;
                entry.Email = email;
                entry.Password = password;
                entry.IsRegistred = true;
                entry.Role = Role.User;
                context.SaveChanges();
                return true;
            }
        }

        public IQueryable<User> GetAllTeachers()
        {
            var res = from u in UserCollection
                      where u.IsTeacher
                      select u;
            return res;
        }

        public void AddTeacher(int id, string fullName, string chair, string degree, ICollection<string> courses, DateTime? from, DateTime? till, byte[] photoData, string imageType)
        {
            if (id == 0)
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
                foreach (Course course in context.Courses)
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
                context.Users.Add(dbEntry);
            }
            else
            {

                User dbEntry = context.Users.Find(id);
                foreach (Course course in dbEntry.UserInfo.Courses.ToList())
                {
                    dbEntry.UserInfo.Courses.Remove(course);
                }
                dbEntry.FullName = fullName;
                dbEntry.IsTeacher = true;
                dbEntry.UserInfo.Teacher.Chiar = chair;
                dbEntry.UserInfo.Teacher.Degree = degree;
                dbEntry.UserInfo.Teacher.WorksFrom = from;
                dbEntry.UserInfo.Teacher.WorkedTill = till;
                
                if (photoData != null)
                {
                    DeleteAdminsPhotoOfTeacher(id);
                    dbEntry.UserInfo.Photos.Add(new Photo { AddedByAdmin = true, IsProfilePhotoOfTeacher = true, ImageData = photoData, ImageMimeType = imageType, PhotoHeader = fullName });
                }
                foreach (Course course in context.Courses)
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
            }

            context.SaveChanges();
        }

        public void DeleteAdminsPhotoOfTeacher(int userId)
        {
            User currentTeacher = (from u in context.Users
                                   where u.UserId == userId
                                   select u).FirstOrDefault();
            if (currentTeacher == null)
            {
                return;
            }
            else
            {
                int deletingPhotoId = 0;
                foreach (Photo photo in currentTeacher.UserInfo.Photos)
                {
                    if (photo.AddedByAdmin)
                    {
                        currentTeacher.UserInfo.Photos.Remove(photo);
                        deletingPhotoId = photo.PhotoId;
                        if (photo.IsProfilePhotoOfTeacher)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (photo.IsProfilePhotoOfTeacher)
                    {
                        photo.IsProfilePhotoOfTeacher = false;
                    }
                }
                if (deletingPhotoId != 0)
                {
                    foreach (Photo photo in context.Photos)
                    {
                        if (photo.PhotoId == deletingPhotoId)
                        {
                            context.Photos.Remove(photo);
                            break;
                        }

                    }
                }

            }
            context.SaveChanges();
        }

        public User DeleteTeacher(int userId)
        {
            User dbEntry = context.Users.Find(userId);
            if (dbEntry != null)
            {

                context.UserInfos.Remove(dbEntry.UserInfo);

                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<User> GetByGraduateYear(int year)
        {
            var result = from user in context.Users
                where user.UserInfo.GraduateInfo.GraduationYear == year
                select user;
            return result.ToList();

        }

        public IEnumerable<string> GetYearsOfGraduation()
        {
            var yearsRes = context.UserInfos.Where(x => x.GraduateInfo.GraduationYear!=0).Select(x => x.GraduateInfo.GraduationYear.ToString()).Distinct().OrderBy(x => x);
            return yearsRes;
        }

        public IEnumerable<User> GetAllGraduates()
        {
            var result = context.Users.Where(x=>!x.IsTeacher && x.Role!=Role.Administrator).Select(x => x).OrderByDescending(x => x.UserInfo.GraduateInfo.GraduationYear);
            return result;
        }

        public User GetUserById(int userId)
        {
            var user = context.Users.Find(userId);
            return user;
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            User u = GetUserByName(username);
            if (string.Compare(u.Password, oldPassword, StringComparison.OrdinalIgnoreCase) == 0)
            {
                u.Password = newPassword;
            }
            else
            {
                return false;
            }
            context.SaveChanges();
            return u.Password == newPassword;
        }
    }
}
