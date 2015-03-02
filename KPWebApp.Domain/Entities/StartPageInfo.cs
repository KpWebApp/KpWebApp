using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.Domain.Entities
{
    public class StartPageInfo
    {
        public StartPageInfo()
        {
            NavigationElements = new Collection<StartPageNavigationElement>();
        }
        public string MainHeader { get; set; }
        public string SubHeader { get; set; }
        public string MainButtonText { get; set; }
        public string GreatingMassage { get; set; }
        public string GreatingInfo { get; set; }

        public string Chief { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string AddressLineOne { get; set; }
        public string AdressLineTwo { get; set; }

        public virtual ICollection<StartPageNavigationElement> NavigationElements { get; set; }

    }

    public class StartPageNavigationElement
    {
        public string Header { get; set; }
        public string BriefInfo { get; set; }
        public string ButtonText { get; set; }

        public virtual StartPageInfo StartPage { get; set; }
    }
}
