using MessageStack.Context;
using MessageStack.Models;
using System;
using System.Linq;

namespace MessageStack.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IBaseRepository<Account>
    {
        private AccountRepository(MessageStackContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Login to the web application using the supplied email and password
        /// </summary>
        public Account Login(string email, string password)
        {
            string encryptedPassword = Helpers.Encrypt.GenerateSHA512String(password);
            try
            {
                return _databaseContext.Accounts.FirstOrDefault(x => (x.Email == email) && (x.Password == encryptedPassword));
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}