using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KPWebApp.Domain.Entities;

namespace KPWebApp.WebUI.Models
{
    public class PersonalData
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Біографія")]
        public string Biography { get; set; }

        [UIHint("Enum")]
        [Display(Name = "Стать")]
        public Gender Gender { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime? BirthDate { get; set; }
    }
}