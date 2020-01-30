using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageStack.Models
{
    [Table("GroupChats")]
    public class GroupChat : BaseModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Account> Paricipants { get; set; }

        public virtual ICollection<GroupMessage> Messages { get; set; }

        public DateTime CreateDate { get; set; }
    }
}