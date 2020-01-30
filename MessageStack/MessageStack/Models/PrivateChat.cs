using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageStack.Models
{
    [Table("PrivateChats")]
    public class PrivateChat : BaseModel
    {
        public Account FirstUser { get; set; }
        public virtual Account SecondUser { get; set; }

        public virtual ICollection<PrivateMessage> Messages { get; set; }

        public DateTime CreateDate { get; set; }
    }
}