using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPWebApp.Domain.Entities;

namespace KPWebApp.WebUI.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public string Status { get; set; }
    }
}