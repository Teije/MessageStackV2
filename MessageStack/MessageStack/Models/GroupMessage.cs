using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageStack.Models
{
    [Table("GroupMessages")]
    public class GroupMessage : BaseModel
    {
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public Account Sender { get; set; }
        public GroupChat GChat { get; set; }
    }
}