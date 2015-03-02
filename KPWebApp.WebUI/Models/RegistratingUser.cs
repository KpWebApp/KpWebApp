using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KPWebApp.WebUI.Models
{
    public class RegistratingUser
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Логін")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Електронна пошта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}