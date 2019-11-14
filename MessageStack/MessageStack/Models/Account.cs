using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    public interface IAccount : IModelBase
    {
        string Name { get; set; }
        string PhoneNumber { get; set; }
        string Password { get; set; }

        List<Contact> Contacts { get; set; }
        List<Chat> Chats { get; set; }
    }

    /// <summary>
    /// Account represents a registered user.
    /// </summary>
    public class Account : ModelBase, IAccount
    {
        public Account() { }

        public Account(Guid id, string name, string phoneNumber, string password, List<Contact> contacts = null, List<Chat> chats = null)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Password = password;

            Contacts = contacts;
            Chats = chats;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }

        [InverseProperty("OwnerAccount")]
        public List<Contact> Contacts { get; set; }
        public List<Chat> Chats { get; set; }
    }
}