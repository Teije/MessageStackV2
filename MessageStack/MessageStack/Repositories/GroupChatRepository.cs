using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class GroupChatRepository : BaseRepository<GroupChat>, IBaseRepository<GroupChat>
    {
        public GroupChatRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}