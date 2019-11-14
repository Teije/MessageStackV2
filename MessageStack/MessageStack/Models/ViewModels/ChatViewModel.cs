using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    /// <summary>
    /// The ChatViewModel is a representation of the data shown in each chat.
    /// </summary>
    public class ChatViewModel : ViewModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Account CurrentAccount { get; set; }
        public List<Contact> CurrentAccountContacts { get; set; }
        public string MessageText { get; set; }

        public List<Account> Participants { get; set; }
        public List<Message> Messages { get; set; }
    }
}