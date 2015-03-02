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
    public class TeacherByAdmin
    {
        [HiddenInput(DisplayValue = false)]
        [DisplayName("ID викладача")]
        public int UserId { get; set; }

        [DisplayName("Ім'я")]
        public string FirstName { get; set; }

        [DisplayName("По батькові")]
        public string MiddleName { get; set; }

        [DisplayName("Прізвище")]
        public string LastName { get; set; }

        [DisplayName("Посада")]
        public string Chair { get; set; }

        [DisplayName("Вчений ступінь")]
        public string Degree { get; set; }

        [DisplayName("Працює з")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? WorksFrom { get; set; }

        [DisplayName("Працював до")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? WorkedTill { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Навчальні курси (новий курс з нового рядка)")]
        public string Courses { get; set; }

        [DisplayName("Світлина")]
        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}