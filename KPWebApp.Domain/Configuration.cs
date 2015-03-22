using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.Domain
{
    internal static class Configuration
    {
        internal static string ConnectionString
        {
            get
            {
                string connectionStr = string.Empty;
                try
                {
                    using (StreamReader sr = new StreamReader(@"~/Content/ConnectionString.txt"))
                    {
                        connectionStr = sr.ReadLine();
                    }
                    return connectionStr;
                }
                catch (Exception exc)
                {
                    return @"workstation id=KpWebApp.mssql.somee.com;packet size=4096;user id=marki27_SQLLogin_1;pwd=fjegbpjpyl;data source=KpWebApp.mssql.somee.com;persist security info=False;initial catalog=KpWebApp";
                }
               
            }
        }
    }
}
