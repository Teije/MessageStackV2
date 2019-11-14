using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    /// <summary>
    /// The ContactViewModel is a representation of the data shown on the contact aside.
    /// </summary>
    public class ChatListViewModel : ViewModelBase
    {
        public List<Chat> Chats { get; set; }
    }
}