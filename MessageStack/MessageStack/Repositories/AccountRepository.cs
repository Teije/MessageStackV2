using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public interface IAccountRepository
    {
        Account FindByLoginDetails(string phoneNumber, string password);
        List<Account> GetAllByChatId(string id);
    }

    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public List<Account> GetAll() => Context.Accounts.Select(c => c).ToList();

        public Account FindByLoginDetails(string phoneNumber, string password)
        {
            return Context.Accounts.FirstOrDefault(a =>
                a.PhoneNumber == phoneNumber &&
                a.Password == password);
        }

        public List<Account> GetAllByChatId(string id)
        {
            var accounts = new List<Account>();
            foreach (var ca in Context.ChatAccount.Where(ca => ca.ChatId.ToString() == id))
            {
                var account = Context.Accounts
                    .FirstOrDefault(a => a.Id == ca.AccountId);


                accounts.Add(account);
            }

            return accounts;
        }
        //Context.Accounts
        //    .Where(a => a.Chats
        //        .Select(c => c.Id.ToString())
        //        .Contains(id)).ToList();
    }
}