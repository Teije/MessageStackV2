using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    [Table("Contacts")]
    public class Contact : BaseModel
    {
        [Required]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Display(Name = "Number")]
        public string Phonenumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public Account OwnerAccount { get; set; }

        public Account TargetAccount { get; set; }
    }
}