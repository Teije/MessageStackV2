using System.Collections.Generic;

namespace MessageStack.Models.ViewModels
{
    public class GroupChatViewModel
    {
        public ICollection<GroupMessage> Messages { get; set; }
        public GroupChat Chat { get; set; }
    }
}