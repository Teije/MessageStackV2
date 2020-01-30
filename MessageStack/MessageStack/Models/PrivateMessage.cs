using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    [Table("PrivateMessages")]
    public class PrivateMessage : BaseModel
    {
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public Account Sender { get; set; }
        public PrivateChat PrivateChat { get; set; }
    }
}