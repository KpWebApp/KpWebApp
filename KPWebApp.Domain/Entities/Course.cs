using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Abstract;

namespace KPWebApp.Domain.Entities
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }

        public override bool Equals(object obj)
        {
            Course course = obj as Course;
            if (Name == course.Name)
            {
                return true;
            }
            return false;
            
        }
    }
}
