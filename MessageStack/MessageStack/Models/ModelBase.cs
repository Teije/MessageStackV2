using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageStack.Models
{
    public interface IModelBase
    {
        Guid Id { get; set; }
    }

    /// <summary>
    /// The base class for each model in this application. It contains properties all models require.
    /// </summary>
    public abstract class ModelBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}