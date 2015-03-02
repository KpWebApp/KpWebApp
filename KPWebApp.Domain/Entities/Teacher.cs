﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.Domain.Entities
{
    /// <summary>
    /// Initialize a new instance of the <see cref="Teacher"/> class.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Gets or sets Chiar of Teacher.
        /// </summary>
        public string Chiar { get; set; }

        /// <summary>
        /// Gets or sets Degree of Teacher.
        /// </summary>
        public string Degree { get; set; }

        /// <summary>
        /// Gets or sets WorksFrom of Teacher.
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public DateTime? WorksFrom { get; set; }

        /// <summary>
        /// Gets or sets WorkedTill of Teacher.
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public DateTime? WorkedTill { get; set; }

        
    }
}
