using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;

namespace MessageStack.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ChatRepository _chatRepository = new ChatRepository();
        private readonly AccountRepository _accountRepository = new AccountRepository();
        private readonly MessageRepository _messageRepository = new MessageRepository();
        private readonly ContactRepository _contactRepository = new ContactRepository();
        private readonly ChatAccountRepository _chatAccountRepository = new ChatAccountRepository();

        public ActionResult Index()
        {
            var currentAccount = (Account) Session["current"];

            if (currentAccount == null || !IsAuthorized(currentAccount.Id)) return View("Error");

            var chatAccounts = _chatAccountRepository.GetAllFor(currentAccount.Id);
            var chats = new List<Chat>();

            foreach (var ca in chatAccounts)
            {
                var chat = _chatRepository.Find(c => c.Id == ca.ChatId);
                if(!chats.Contains(chat)) chats.Add(chat);
            }

            var contacts = _contactRepository.GetFor(currentAccount.Id);

            return View("Index", new DashboardViewModel() {CurrentUser = currentAccount, Contacts = contacts, Chats = chats});
        }

    }
}