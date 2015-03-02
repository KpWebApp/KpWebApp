using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using KPWebApp.WebUI.Infrastructure.Abstract;

namespace KPWebApp.WebUI.Infrastructure.Concrete
{
    public class FormAuthorizationProvider: IAuthorizationProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}