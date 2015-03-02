using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPWebApp.Domain.Entities;

namespace KPWebApp.WebUI.Models
{
    public class TeacherInfoPage
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Chair { get; set; }

        public string Degree { get; set; }

        public DateTime? WorksFrom { get; set; }

        public DateTime? WorkedTill { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        public Photo ProfilePhoto { get; set; }

    }
}