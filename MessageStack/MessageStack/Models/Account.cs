using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageStack.Models
{
    [Table("Accounts")]
    public class Account : BaseModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Text)] public string Firstname { get; set; }

        [DataType(DataType.Text)] public string Lastname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phonenumber { get; set; }

        //Private Chats of which is account is a part of
        public virtual ICollection<PrivateChat> PrivateChats { get; set; }

        //Group Chats of which is account is a part of
        public virtual ICollection<GroupChat> GroupChats { get; set; }
    }
}