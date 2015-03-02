using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using KPWebApp.Domain.Concrete;
using System.Web.Caching;


namespace KPWebApp.WebUI.Infrastructure.Concrete
{
    public class CustomRoleProvider : RoleProvider
    {
        private int casheTimeoutInMinutes = 720;
        
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        //Place for some error!!!
        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }
            var cacheKey = string.Format("UserRoles_{0}", username);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (string[]) HttpRuntime.Cache[cacheKey];
            }
            var userRoles = new string[] {};
            using (var context = new KpWebAppDb())
            {
                var user = (from u in context.Users
                            where String.Compare(u.Username, username, StringComparison.OrdinalIgnoreCase) == 0
                            select u).FirstOrDefault();

                if (user != null)
                {
                    userRoles = new[] { user.Role.ToString() };
                }

                HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(casheTimeoutInMinutes), Cache.NoSlidingExpiration);
                return userRoles.ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            int val;
            if (!string.IsNullOrEmpty(config["casheTimeoutInMinutes"]) &&
                Int32.TryParse(config["casheTimeoutInMinutes"], out val))
            {
                this.casheTimeoutInMinutes = val;
            }
            base.Initialize(name, config);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}