using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.DAL;
using KPWebApp.Domain.Entities;

namespace KPWebApp.BLL
{
    public static class Search
    {
        public static List<string> YearsOfGraduation(IUnitOfWork unit)
        {
            using (var unitOfwork = unit)
            {
                var years =
                    unitOfwork.UserRepository.Get(u => u.UserInfo != null && u.UserInfo.GraduateInfo.GraduationYear != 0)
                        .Select(user => user.UserInfo.GraduateInfo.GraduationYear.ToString())
                        .Distinct()
                        .OrderBy(x => x)
                        .ToList();
                return years;
            }
            
        }
    }
}
