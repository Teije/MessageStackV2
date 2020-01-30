using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Context
{
    public class MessageStackContext : DbContext
    {
        public MessageStackContext() : base("name=MessageStackContext")
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<GroupChat> GChats { get; set; }
    }
}