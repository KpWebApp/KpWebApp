using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPWebApp.Domain.Entities;

namespace KPWebApp.WebUI.Models
{
    public class AdminsListOfItems<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}