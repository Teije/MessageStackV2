using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    public interface IMessage : IModelBase
    {
        [Required]
        Guid ChatId { get; set; }
        [Required]
        Guid SenderId { get; set; }
        [Required]
        string SenderName { get; set; }
        [Required]
        string Text { get; set; }
        [Required]
        DateTime TimeStamp { get; set; }
    }

    /// <summary>
    /// Message is an object that represents a piece of text sent from one user to another, within a chat.
    /// </summary>
    public class Message : ModelBase, IMessage
    {
        public Message() { }

        public Message(Guid chatId, Guid senderId, string senderName, string text, DateTime timeStamp)
        {
            ChatId = chatId;
            SenderId = senderId;
            SenderName = senderName;
            Text = text;
            TimeStamp = timeStamp;
        }

        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}