using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class PrivateChatRepository : BaseRepository<PrivateChat>, IBaseRepository<PrivateChat>
    {
        private PrivateChatRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}