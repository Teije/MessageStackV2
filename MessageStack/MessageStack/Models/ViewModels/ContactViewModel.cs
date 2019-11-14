using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string OwnerAccountId { get; set; }
        public string ContactAccountId { get; set; }

        public bool IsSelected { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}