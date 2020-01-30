using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class PrivateMessageRepository : BaseRepository<PrivateMessage>, IBaseRepository<PrivateMessage>
    {
        private PrivateMessageRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}