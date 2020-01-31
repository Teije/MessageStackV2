using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    public class AccountChangePhoneEmailViewModel
    {
        public string Id { get; set; }

        [Phone]
        [DisplayName("Enter your new number")]
        public string Phonenumber { get; set; }

        [EmailAddress]
        [DisplayName("Enter your new email address")]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [DisplayName("Enter your current password")]
        public string CurrentPassword { get; set; }
    }
}