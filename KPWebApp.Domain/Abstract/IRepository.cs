using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Entities;


namespace KPWebApp.Domain.Abstract
{
    public interface IRepository
    {
        IQueryable<Post> PostsCollection { get; }
        IQueryable<User> UserCollection { get; }
        void SavePost(Post post, User user);
        Post DeletePost(int id);
        User GetUserByName(string name);
        Post GetPostById(int id);
        Photo DeletePhoto(int id);
        Photo GetPhotoById(int id);
        void SavePhoto(Photo photo, string username);
        IQueryable<Post> GetPostsByRole(Role role);
        void SaveUsersPersonalData(User user);
        void SaveUsersContactInfo(ContactInfo info, string username);
        void AddGraduate(int id, string fullName, int enranceYear, int graduationYear, Speciality speciality);
        User GetUserByFullName(string fullName);
        bool Register(int id, string username, string email, string password);
        IQueryable<User> GetAllTeachers();

        void AddTeacher(int id, string fullName, string chair, string degree,
            ICollection<string> courses, DateTime? from, DateTime? till, byte[] photoData, string imageType);

        void DeleteAdminsPhotoOfTeacher(int userId);
        User DeleteTeacher(int userId);

        IEnumerable<User> GetByGraduateYear(int year);
        IEnumerable<string> GetYearsOfGraduation();
        IEnumerable<User> GetAllGraduates();
        User GetUserById(int userId);
        bool ChangePassword(string username, string oldPassword, string newPassword);

    }
}
