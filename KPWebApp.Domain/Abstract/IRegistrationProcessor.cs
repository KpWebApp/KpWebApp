using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPWebApp.Domain.Entities;

namespace KPWebApp.Domain.Abstract
{
    public interface IRegistrationProcessor
    {
        void ProcessOrder(string firstName, string middleName, string lastName, string email, string username);
    }
}
