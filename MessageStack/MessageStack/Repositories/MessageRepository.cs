using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public interface IMessageRepository { }

    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public List<Message> FindAllFor(string chatId) => Context.Messages.Where(m => m.ChatId.ToString() == chatId).ToList();
    }
}