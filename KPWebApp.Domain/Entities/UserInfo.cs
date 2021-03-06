﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.Domain.Entities
{
    public class UserInfo
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="UserInfo"/> class.
        /// </summary>
        public UserInfo()
        {
            this.ContactInfo = new ContactInfo();
            this.GraduateInfo = new GraduateInfo();
            this.Teacher = new Teacher();
            this.Photos = new List<Photo>();
            //this.BirthDate = new DateTime();
        }

        /// <summary>
        /// Gets or sets Id of UserInfo.
        /// </summary>
        public int UserInfoId { get; set; }

        /// <summary>
        /// Gets or sets Email of UserInfo.
        /// </summary>
        public string Email { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// Gets or sets Gender of UserInfo.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets BIO of UserInfo.
        /// </summary>
        public string BIO { get; set; }

        //[Column(TypeName = "DateTime2")]
        //public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets ContactInfo of UserInfo.
        /// </summary>
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets GraduateInfo of UserInfo.
        /// </summary>
        public GraduateInfo GraduateInfo { get; set; }

        /// <summary>
        /// Gets or sets Teacher of UserInfo.
        /// </summary>
        public Teacher Teacher { get; set; }

        /// <summary>
        /// Gets or sets User for UserInfo.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets Photo for UserInfo.
        /// </summary>
        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

    }

    public enum Gender
    {
        Female = 1,
        Male = 2
    }
}
