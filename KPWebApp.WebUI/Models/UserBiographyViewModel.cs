using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPWebApp.Domain.Entities;

namespace KPWebApp.WebUI.Models
{
    public class UserBiographyViewModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}