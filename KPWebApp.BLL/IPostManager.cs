using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KPWebApp.DAL;
using KPWebApp.Domain.Entities;

namespace KPWebApp.BLL
{
    public interface IPostManager: IDisposable
    {
        List<Post> PostsByAdmin(string category, int pageCapacity, int page);
        int GetCountOfPosts();
        int GetCountOfPostsByAdmin(string category);
        int GetCountOfPostsByAdmin();
        FileContentResult GetPhotoForPost(int postId);
        User GetTeacherById(int teacherId);
        FileContentResult GetTeachersProfileImage(int teacherId);
    }
}
