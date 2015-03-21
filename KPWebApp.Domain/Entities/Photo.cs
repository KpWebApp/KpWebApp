using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KPWebApp.Domain.Abstract;

namespace KPWebApp.Domain.Entities
{
    /// <summary>
    /// Initialize a new instance of the <see cref="Photo"/> class.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets Id value for Photo.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        [DisplayName("ID світлини")]
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets or sets Data value for Photo.
        /// </summary>
        [DisplayName("Світлина")]
        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Required(ErrorMessage = "Please enter a photo header.")]
        [DisplayName("Підпис до світлини")]
        public string PhotoHeader { get; set; }

        /// <summary>
        /// Gets or sets UserInfo value for Photo.
        /// </summary>
        public virtual UserInfo UserInfo { get; set; }

        public bool IsProfilePhotoOfTeacher { get; set; }

        public bool AddedByAdmin { get; set; }

    }
}
