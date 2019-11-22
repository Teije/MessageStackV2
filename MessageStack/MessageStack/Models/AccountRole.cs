using System;

namespace MessageStack.Models
{
    public interface IAccountRole
    {
        Guid AccountId { get; set; }
        string Role { get; set; }
    }

    public class AccountRole : ModelBase, IAccountRole
    {
        public AccountRole()
        {

        }

        public Guid AccountId { get; set; }
        public string Role { get; set; }

    }
}