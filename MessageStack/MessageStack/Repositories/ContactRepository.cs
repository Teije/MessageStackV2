using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class ContactRepository : GenericRepository<Contact>
    {
        public List<Contact> GetFor(Guid accountId) => Context.Contacts.Where(c => c.OwnerAccountId == accountId).ToList();
    }
}