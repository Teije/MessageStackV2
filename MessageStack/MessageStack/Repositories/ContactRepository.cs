using MessageStack.Context;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IBaseRepository<Contact>
    {
        public ContactRepository(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }
    }
}