using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class PrivateMessageRepository : BaseRepository<PrivateMessage>, IBaseRepository<PrivateMessage>
    {
        public PrivateMessageRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}