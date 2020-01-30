using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MessageStack.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly AccountRepository _accountRepository;
        private readonly GroupChatRepository _groupChatRepository;
        private readonly GroupMessageRepository _groupMessageRepository;
        private readonly PrivateChatRepository _privateChatRepository;
        private readonly PrivateMessageRepository _privateMessageRepository;

        public ChatController() { }

        public ChatController(AccountRepository accountRepository, GroupChatRepository groupChatRepository, GroupMessageRepository groupMessageRepository, PrivateChatRepository privateChatRepository, PrivateMessageRepository privateMessageRepository)
        {
            _accountRepository = accountRepository;
            _groupChatRepository = groupChatRepository;
            _groupMessageRepository = groupMessageRepository;
            _privateChatRepository = privateChatRepository;
            _privateMessageRepository = privateMessageRepository;
        }

        // GET: Chat
        public ActionResult Index()
        {
            var currentAccount = (Account)Session["Loggedin_Account"];

            var chatModel = new ChatListViewModel
            {
                GroupChats = _groupChatRepository.GetWhere(gc => currentAccount.GroupChats.Contains(gc)).Result.ToList(),
                PrivateChats = _privateChatRepository.GetWhere(pc => currentAccount.PrivateChats.Contains(pc)).Result.ToList()
            };

            return View(chatModel);
        }

        [HttpGet]
        public ActionResult PrivateChat(Guid chatId, Guid otherAccountId)
        {
            var loggedinAccount = (Account)Session["Loggedin_Account"];
            var otherAccount = _accountRepository.FirstOrDefault(a => a.Id == otherAccountId).Result;

            var privateChat = _privateChatRepository.FirstOrDefault(pc => pc.Id == chatId).Result;

            if (privateChat == null)
            {
                return View(CreatePrivateChat(loggedinAccount, otherAccount));
            }

            ViewBag.OtherAccount = privateChat.FirstUser == (Account)Session["Loggedin_Account"]
                ? privateChat.SecondUser
                : privateChat.FirstUser;

            return View(privateChat);
        }

        [HttpPost]
        public ActionResult GroupChat(Guid id)
        {
            var groupChatViewModel = new GroupChatViewModel
            {
                Messages = _groupMessageRepository.GetWhere(gm => gm.Id == id).Result.ToList(),
                Chat = _groupChatRepository.FirstOrDefault(gc => gc.Id == id).Result
            };

            return View(groupChatViewModel);
        }

        [HttpPost]
        public ActionResult SendMessage(string message, Guid chatId, Guid otherAccountId)
        {
            var chat = _privateChatRepository.FirstOrDefault(pc => pc.Id == chatId).Result;

            var privateMessage = new PrivateMessage
            {
                Content = message,
                SendDate = DateTime.Now,
                Sender = (Account)Session["Loggedin_Account"],
                PrivateChat = chat
            };
            chat.Messages.Add(privateMessage);

            var result = _privateMessageRepository.Add(privateMessage);

            return PrivateChat(chatId, otherAccountId);
        }

        [HttpPost]
        public ActionResult SendMessage(GroupMessage message)
        {
            return View();
        }

        private PrivateChat CreatePrivateChat(Account loggedinAccount, Account otherAccount)
        {
            var newChat = new PrivateChat
            {
                FirstUser = loggedinAccount,
                SecondUser = otherAccount,
                CreateDate = DateTime.Now,
            };

            newChat = _privateChatRepository.Add(newChat).Result;

            var message = new PrivateMessage
            {
                PrivateChat = newChat,
                Content = "No messages have been sent yet!",
                SendDate = DateTime.Now,
            };

            message = _privateMessageRepository.Add(message).Result;
            newChat.Messages.Add(message);

            ViewBag.OtherAccount = newChat.FirstUser == (Account)Session["Loggedin_Account"]
                ? newChat.SecondUser
                : newChat.FirstUser;

            return newChat;
        }
    }
}