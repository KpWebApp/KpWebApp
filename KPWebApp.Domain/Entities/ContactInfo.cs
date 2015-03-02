using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.Domain.Entities
{
    public class ContactInfo
    {
        /// <summary>
        /// Gets or sets Skype login.
        /// </summary>
        [Display(Name = "Skype")]
        public string Skype { get; set; }

        /// <summary>
        /// Gets or sets ICQ login.
        /// </summary>
        [Display(Name = "Facebook-посилання")]
        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        /// <summary>
        /// Gets or sets HomeNumber.
        /// </summary>
        [Display(Name = "Домашній телефон")]
        [DataType(DataType.PhoneNumber)]
        public string HomeNumber { get; set; }

        /// <summary>
        /// Gets or sets MobNumber login.
        /// </summary>
        [Display(Name = "Мобільний телефон")]
        [DataType(DataType.PhoneNumber)]
        public string MobNumber { get; set; }
    }
}
