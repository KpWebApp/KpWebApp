using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.DAL;
using KPWebApp.Domain.Entities;

namespace KPWebApp.BLL
{
    public static class Account
    {
        public static bool Register(int id, string username, string email, string password, IUnitOfWork unitOfWork)
        {
            using (var unit = unitOfWork)
            {
                if (unit.UserRepository.Get(u => u.Username == username).Any())
                {
                    return false;
                }
                else
                {
                    User entry = unit.UserRepository.GetById(id);
                    if (entry == null)
                    {
                        throw new Exception("Exception occured while registrating a new user.");
                    }
                    if (entry.IsRegistred)
                    {
                        return false;
                    }
                    entry.Username = username;
                    entry.Email = email;
                    entry.Password = password;
                    entry.IsRegistred = true;
                    entry.Role = Role.User;
                    unit.UserRepository.Update(entry);
                    unit.Save();
                    return true;
                }
            }
            
        }

    }
}
