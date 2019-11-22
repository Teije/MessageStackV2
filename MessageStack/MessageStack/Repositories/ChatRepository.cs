using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public interface IChatRepository
    {
    }

    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {

    }
}