using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    /// <summary>
    /// The MessageViewModel is a representation of the data shown in each message.
    /// </summary>
    public class MessageViewModel : ViewModelBase
    {
        public string ChatId { get; set; }

        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}