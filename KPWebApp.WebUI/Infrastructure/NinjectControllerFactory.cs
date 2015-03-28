using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KPWebApp.BLL;
using KPWebApp.DAL;
using KPWebApp.Domain.Concrete;
using KPWebApp.Domain.Entities;
using KPWebApp.Domain.Abstract;
using KPWebApp.WebUI.Infrastructure.Abstract;
using KPWebApp.WebUI.Infrastructure.Concrete;
using Moq;
using Ninject;

namespace KPWebApp.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IRepository>().To<PostRepository>();
            ninjectKernel.Bind<IAuthorizationProvider>().To<FormAuthorizationProvider>();
            ninjectKernel.Bind<MembershipProvider>().To<CustomMembershipProvider>();
            ninjectKernel.Bind<RoleProvider>().To<CustomRoleProvider>();
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            ninjectKernel.Bind(typeof (IRepository<>)).To(typeof (Repository<>));
            ninjectKernel.Bind<IManagePosts>().To<ManagePosts>();
            EmailSettings emailSettings = new EmailSettings
            {
                MailFromAddress = "kp@kpwebapp.com",
                UseSsl = true,
                Username = "kpwebapp@gmail.com",
                Password = "kp2014secret",
                ServerName = "smtp.gmail.com",
                ServerPort = 465,
                FileLocation = @"F:\Education\Emails",
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IRegistrationProcessor>()
                .To<EmailRegistrationProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}