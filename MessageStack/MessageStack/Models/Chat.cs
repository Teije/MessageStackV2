using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    public interface IChat : IModelBase
    {
        string Name { get; set; }
        List<Message> Messages { get; set; }
        List<Account> Participants { get; set; }
    }

    public class Chat : ModelBase, IChat
    {
        public Chat()
        {

        }

        public Chat(List<Message> messages, List<Account> participants, string name = null)
        {
            Messages = messages;
            Participants = participants;
            Name = name;
        }

        public List<Message> Messages { get; set; }
        public List<Account> Participants { get; set; }
        public string Name { get; set; }
    }
}