using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress { get; set; }
        public string MailFromAddress = "kp@kpwebapp.com";
        public bool UseSsl = true;
        public string Username = "kpwebapp@gmail.com";
        public string Password = "kp2014secret";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 465;
        public bool WriteAsFile = false;
        public string FileLocation = @"F:\Education\Emails";
    }
}
