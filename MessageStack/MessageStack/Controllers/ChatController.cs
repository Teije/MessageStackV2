using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;
using MessageStack.Context;

namespace MessageStack.Controllers
{
    public class ChatController : BaseController
    {
        public ChatController() : this (RepositoryContext.GetInstance())
        {

        }

        public ChatController(MessageStackContext messageStackContext) : base(messageStackContext)
        {

        }

        public ActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var currentAccount = (Account)Session["Loggedin_Account"];
            var loggedInAccount = _accountRepository.GetById(currentAccount.Id);

            var chatModel = new ChatListViewModel
            {
                GroupChats = _groupChatRepository.GetWhere(gc => gc.Paricipants.Select(p => p.Id).Contains(loggedInAccount.Id)).ToList(),
                PrivateChats = _privateChatRepository.GetWhere(pc => pc.FirstUser.Id == loggedInAccount.Id || pc.SecondUser.Id == loggedInAccount.Id).ToList()
            };

            foreach (var gc in chatModel.GroupChats)
            {
                var dbEntry = _groupChatRepository.GetById(gc.Id);
                gc.Paricipants = dbEntry.Paricipants;
            }

            foreach (var pc in chatModel.PrivateChats)
            {
                var dbEntry = _privateChatRepository.GetById(pc.Id);

                pc.FirstUser = dbEntry.FirstUser;
                pc.SecondUser = dbEntry.SecondUser;
            }

            return View(chatModel);
        }

        [HttpGet]
        public ActionResult PrivateChat(string otherAccountId)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");

            var loggedinAccount = (Account)Session["Loggedin_Account"];
            var otherAccount = _accountRepository.FirstOrDefault(a => a.Id == new Guid(otherAccountId));

            var privateChat = _privateChatRepository.FirstOrDefault(pc =>
                pc.FirstUser.Id == otherAccount.Id || pc.SecondUser.Id == otherAccount.Id);

            if (privateChat == null)
            {
                return View(CreatePrivateChat(loggedinAccount, otherAccount));
            }

            ViewBag.OtherAccount = otherAccount;

            return View(privateChat);

        }

        [HttpPost]
        public ActionResult GroupChat(Guid id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var groupChatViewModel = new GroupChatViewModel
            {
                Messages = _groupMessageRepository.GetWhere(gm => gm.Id == id).ToList(),
                Chat = _groupChatRepository.FirstOrDefault(gc => gc.Id == id)
            };

            return View(groupChatViewModel);

        }

        [HttpPost]
        public ActionResult SendPrivateMessage(string message, string chatId, string otherAccountId)
        {
            if (IsLoggedIn())
            {
                var chat = _privateChatRepository.FirstOrDefault(pc => pc.Id == new Guid(chatId));

                var privateMessage = new PrivateMessage
                {
                    Content = message,
                    SendDate = DateTime.Now,
                    Sender = (Account)Session["Loggedin_Account"],
                    PrivateChatId = chat.Id
                };
                //chat.Messages.Add(privateMessage);

                var result = _privateMessageRepository.Add(privateMessage);

                return PrivateChat(otherAccountId);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SendGroupMessage(GroupMessage message)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            return View();

        }

        private PrivateChat CreatePrivateChat(Account loggedinAccount, Account otherAccount)
        {
            if (!IsLoggedIn()) return null;
            var newChat = new PrivateChat
            {
                FirstUser = loggedinAccount,
                SecondUser = otherAccount,
                CreateDate = DateTime.Now,
            };

            newChat = _privateChatRepository.Add(newChat);

            var message = new PrivateMessage
            {
                PrivateChatId = newChat.Id,
                Content = "No messages have been sent yet!",
                SendDate = DateTime.Now,
            };

            message = _privateMessageRepository.Add(message);
            newChat.Messages.Add(message);

            ViewBag.OtherAccount = newChat.FirstUser == (Account)Session["Loggedin_Account"]
                ? newChat.SecondUser
                : newChat.FirstUser;

            return newChat;

        }
    }
}