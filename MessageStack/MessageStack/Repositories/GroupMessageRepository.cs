using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class GroupMessageRepository : BaseRepository<GroupMessage>, IBaseRepository<GroupMessage>
    {
        private GroupMessageRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}