using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.WebUI.Infrastructure.Abstract
{
    public interface IAuthorizationProvider
    {
        bool Authenticate(string username, string password);
    }
}
