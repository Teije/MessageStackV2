using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}