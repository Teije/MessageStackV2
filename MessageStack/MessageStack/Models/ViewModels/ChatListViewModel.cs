using System.Collections.Generic;

namespace MessageStack.Models.ViewModels
{
    public class ChatListViewModel
    {
        public List<PrivateChat> PrivateChats { get; set; }
        public List<GroupChat> GroupChats { get; set; }
    }
}