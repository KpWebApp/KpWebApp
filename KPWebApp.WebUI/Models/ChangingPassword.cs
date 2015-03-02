using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KPWebApp.WebUI.Models
{
    public class ChangingPassword
    {
        [DisplayName("Старий пароль")]
        [Required]
        public string OldPassword { get; set; }
        [DisplayName("Ноий пароль")]
        [Required]
        public string NewPassword { get; set; }
    }
}