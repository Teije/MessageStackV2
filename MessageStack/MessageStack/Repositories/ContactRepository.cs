using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IBaseRepository<Contact>
    {
        private ContactRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}