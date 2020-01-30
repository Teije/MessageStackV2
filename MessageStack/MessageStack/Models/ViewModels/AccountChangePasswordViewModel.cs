using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    public class AccountChangePasswordViewModel
    {
        [Required]
        [PasswordPropertyText]
        [DisplayName("Enter your new password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords must be equal")]
        [PasswordPropertyText]
        [DisplayName("Enter your password again")]
        public string RepeatPassword { get; set; }

        [Required]
        [PasswordPropertyText]
        [DisplayName("Enter your current password")]
        public string CurrentPassword { get; set; }
    }
}