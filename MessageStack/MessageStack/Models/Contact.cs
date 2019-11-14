using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    public interface IContact
    {
        [Required]
        string Name { get; set; }
        [Required]
        string PhoneNumber { get; set; }

        [Required]
        Guid OwnerAccountId { get; set; }
        Account OwnerAccount { get; set; }
        Guid? ContactAccountId { get; set; }
        Account ContactAccount { get; set; }
    }

    /// <summary>
    /// Contact is an object that represents the connection between two users.
    /// </summary>
    public class Contact : ModelBase, IContact
    {
        public Contact() { }

        public Contact(string name, string phoneNumber, Guid ownerAccountId, Guid? contactAccountId = null)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            OwnerAccountId = ownerAccountId;
            ContactAccountId = contactAccountId;
        }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Guid OwnerAccountId { get; set; }
        public Guid? ContactAccountId { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }

        //Account that created the contact
        [ForeignKey("OwnerAccountId")]
        public virtual Account OwnerAccount { get; set; }
        //Added account upon creation
        [ForeignKey("ContactAccountId")]
        public virtual Account ContactAccount { get; set; }
    }
}