using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    /// <summary>
    /// The Dashboard is a representation of the data shown on the dashboard.
    /// </summary>
    public class DashboardViewModel : ViewModelBase
    {
        public List<Chat> Chats { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}