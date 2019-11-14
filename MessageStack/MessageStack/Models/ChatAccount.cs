using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    public class ChatAccount : ModelBase
    {
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}