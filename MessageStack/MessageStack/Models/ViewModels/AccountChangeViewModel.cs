using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageStack.Models.ViewModels
{
    public class AccountChangeViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [PasswordPropertyText]
        [DisplayName("Enter your current password:")]
        public string CurrentPassword { get; set; }
    }
}