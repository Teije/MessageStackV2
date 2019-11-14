using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    /// <summary>
    /// The ContactViewModel is a representation of the data shown on the contact aside.
    /// </summary>
    public class ContactListViewModel : ViewModelBase
    {
        public List<Contact> Contacts { get; set; }
    }
}