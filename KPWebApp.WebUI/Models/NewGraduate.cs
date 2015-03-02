using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPWebApp.Domain.Entities;

namespace KPWebApp.WebUI.Models
{
    public class NewGraduate
    {
        [DisplayName("ID випускника")]
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Ім'я")]
        public string FirstName { get; set; }

        [DisplayName("По батькові")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Прізвище")]
        public string LastName { get; set; }

        [DisplayName("Рік вступу")]
        public int EntarnceYear { get; set; }

        [Required]
        [DisplayName("Рік випуску")]
        public int  GraduationYear { get; set; }

        [UIHint("Enum")]
        [Required]
        [DisplayName("Кафедра")]
        public Speciality Speciality { get; set; }
    }
}