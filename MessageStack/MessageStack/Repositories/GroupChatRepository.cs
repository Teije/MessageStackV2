using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class GroupChatRepository : BaseRepository<GroupChat>, IBaseRepository<GroupChat>
    {
        private GroupChatRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}