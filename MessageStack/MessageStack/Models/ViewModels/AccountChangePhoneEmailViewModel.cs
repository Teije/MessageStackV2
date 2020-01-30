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