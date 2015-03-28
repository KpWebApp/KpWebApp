using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Entities;

namespace KPWebApp.Domain.Concrete
{
    public class KpWebAppDb : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Course> Courses { get; set; }

        public KpWebAppDb()
            //: base("dbConn")
            //: base(@"workstation id=KpWebApp.mssql.somee.com;packet size=4096;user id=marki27_SQLLogin_1;pwd=fjegbpjpyl;data source=KpWebApp.mssql.somee.com;persist security info=False;initial catalog=KpWebApp")
            : base(@"Server=4b855939-d8aa-47d4-9e37-a46900c5639c.sqlserver.sequelizer.com;Database=db4b855939d8aa47d49e37a46900c5639c;User ID=nmlakpbvyuykiczb;Password=6XuYxLCEiowbNvVNgmqPmTgzwYvUFq2abAnYQnd485ZkockKasrhXoZmQ3AGpQ72;")       
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ComplexType<ContactInfo>();
            modelBuilder.ComplexType<GraduateInfo>();
            modelBuilder.ComplexType<Teacher>();

            modelBuilder.Entity<Photo>().Property(p => p.ImageData).HasColumnType("image");

            modelBuilder.Entity<User>()
                        .HasMany<Post>(s => s.Posts)
                        .WithRequired(s => s.User);

            modelBuilder.Entity<UserInfo>()
                        .HasMany<Photo>(s => s.Photos)
                        .WithRequired(s => s.UserInfo);

            modelBuilder.Entity<UserInfo>().HasRequired(p => p.User).WithRequiredDependent(p => p.UserInfo);
            modelBuilder.Entity<UserInfo>()
                   .HasMany<Course>(s => s.Courses)
                   .WithMany(c => c.UserInfos)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("UserInfoId");
                       cs.MapRightKey("CourseId");
                       cs.ToTable("UserCourse");
                   });


        }
    }
}
