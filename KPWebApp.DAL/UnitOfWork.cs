using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;

namespace KPWebApp.DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private KpWebAppDb context = new KpWebAppDb();
        private Repository<User> userRepository;
        private Repository<Post> postRepository;
        private Repository<Photo> photoRepository;
        private Repository<Course> courseRepository;
        private bool disposed = false;

        public Repository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User>(context);
                }
                return userRepository;
            }
        }

        public Repository<Post> PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new Repository<Post>(context);
                }
                return postRepository;
            }
        }

        public Repository<Photo> PhotoRepository
        {
            get
            {
                if (this.photoRepository == null)
                {
                    this.photoRepository = new Repository<Photo>(context);
                }
                return photoRepository;
            }
        }

        public Repository<Course> CourseRepository
        {
            get
            {
                if (this.courseRepository == null)
                {
                    this.courseRepository = new Repository<Course>(context);
                }
                return courseRepository;
            }
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
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
