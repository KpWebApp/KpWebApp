using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KPWebApp.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }


        public string Username { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Role Role { get; set; }

        public string FullName { get; set; }

        public bool IsRegistred { get; set; }

        public bool IsTeacher { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public User()
        {
            Posts = new List<Post>();
        }

    }

    public enum Role
    {
        Administrator = 1, 
        User = 2
    }
}
