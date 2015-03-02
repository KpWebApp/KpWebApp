using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KPWebApp.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new { controller = "Home", action = "Index" });

            routes.MapRoute(
                null,
                "{category}",
                new { controller = "Post", action = "List", page = 1 }
            );
            routes.MapRoute(
               null,
               "{category}/Page{page}",
               new { controller = "Post", action = "List" },
               new { page = @"\d+" }

           );
            routes.MapRoute(
                null,
                "{controller}/{action}/PostId{postId}",
                 new { controller = "Admin", action = "Edit" }


            );
            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}