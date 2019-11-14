using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public interface IChatRepository
    {
        List<Chat> GetAll(Guid id);
    }

    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        //public List<Account> GetAllParticipants(Guid id) => Context.Chats.FirstOrDefault(c => c.Id == id)?.Participants;
        public List<Chat> GetAll(Guid id) => Context.Chats.Where(c => c.Participants
            .Select(p => p.Id)
            .Contains(id)).ToList();
    }
}