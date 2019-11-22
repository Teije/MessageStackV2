using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    /// <summary>
    /// The ViewModelBase holds properties that all ViewModels require.
    /// </summary>
    public class ViewModelBase
    {
        public ViewModelBase()
        {
            CurrentUser = HttpContext.Current.User.Identity as Account;
        }

        public string PageTitle { get; set; }
        public Account CurrentUser { get; set; }
    }
}