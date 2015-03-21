using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Entities;

namespace KPWebApp.DAL
{
    public interface IUnitOfWork: IDisposable
    {
        Repository<User> UserRepository { get; }
        Repository<Post> PostRepository { get; }
        Repository<Photo> PhotoRepository { get; }
        Repository<Course> CourseRepository { get; }
        void Save();
    }
}
