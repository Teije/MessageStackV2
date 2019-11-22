using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class ChatAccountRepository : GenericRepository<ChatAccount>
    {
        public ChatAccountRepository()
        {

        }

        public List<ChatAccount> GetAllFor(Guid accountId) => Context.ChatAccount.Where(ca => ca.AccountId == accountId).ToList();
    }
}