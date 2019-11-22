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
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly ChatRepository _chatRepository = new ChatRepository();
        private readonly AccountRepository _accountRepository = new AccountRepository();
        private readonly MessageRepository _messageRepository = new MessageRepository();
        private readonly ContactRepository _contactRepository = new ContactRepository();
        private readonly ChatAccountRepository _chatAccountRepository = new ChatAccountRepository();

        private Account CurrentAccount => (Account)Session["current"];

        /// <summary>
        /// Opens the chat specified by the chat accountId.
        /// </summary>
        public ActionResult Index(string id)
        {
            if (!IsAuthorized(CurrentAccount.Id)) return View("Error");

            //todo return error message: chat does not exist
            if (id == null) return View();

            var chat = _chatRepository
                .Find(c => c.Id.ToString() == id);

            var participants = _accountRepository
                .GetAllByChatId(id);

            var messages = _messageRepository
                .FindAllFor(id)
                .OrderBy(m => m.TimeStamp)
                .ToList();

            return PartialView("_Chat", new ChatViewModel()
            {
                Id = id,
                Name = chat.Name,
                CurrentAccount = CurrentAccount,
                Participants = participants,
                Messages = messages
            });
        }

        [HttpPost]
        public ActionResult Index(ChatViewModel formData)
        {
            //Check if the form is valid
            if (!ModelState.IsValid || string.IsNullOrEmpty(formData.MessageText))
                return RedirectToAction("Index", "Chat", formData.Id);

            //Add the account to the database using the repository
            _messageRepository.Add(new Message()
            {
                Id = Guid.NewGuid(),
                ChatId = new Guid(formData.Id),
                SenderId = new Guid(CurrentAccount.Id.ToString()),
                SenderName = CurrentAccount.Name,
                Text = formData.MessageText,
                TimeStamp = DateTime.Now
            });

            return RedirectToAction("Index", "Chat", formData.Id);
        }

        /// <summary>
        /// Return a view with all chats that belong to the specified account accountId
        /// </summary>
        [ChildActionOnly]
        public ActionResult ChatList()
        {
            var chatAccounts = _chatAccountRepository.GetAllFor(CurrentAccount.Id);

            var y = chatAccounts.Select(ca => ca.Chat).Distinct();

            var z = y.Select(chat => new Chat()
            {
                Id = chat.Id,
                Name = chat.Name,
                Messages = chat.Messages,
                Participants = _accountRepository.GetAllByChatId(chat.Id.ToString())
            });

            var a = z.ToList();


            //Add a new chat to the chat list
            var model = new ChatListViewModel()
            {
                Chats = a
            };

            return PartialView("_ChatList", model);
        }

        /// <summary>
        /// Return a view that allows for creating a new chat for the current user
        /// </summary>
        public ActionResult Create() => View("Create", new ChatViewModel()
        {
            CurrentAccountContacts = _contactRepository.GetFor(CurrentAccount.Id)
        });

        /// <summary>
        /// Validate & save the "create chat"-form data
        /// </summary>
        [HttpPost]
        public ActionResult Create(ChatViewModel formData)
        {
            //Check if the form is valid
            if (formData.CurrentAccountContacts.Count <= 0)
                return RedirectToAction("Index", "Chat", formData.Id);

            var newId = Guid.NewGuid();
            var selectedContacts = formData.CurrentAccountContacts.Where(c => c.IsSelected);

            //Get an account for each selected contact and add it to the participant list
            var participants = selectedContacts
                .Select(contact => _accountRepository
                    .Find(a => a.Id == contact.ContactAccountId))
                .ToList();

            //Add the current account to the participant list
            participants.Add(CurrentAccount);

            //Add a new chat to the database
            _chatRepository.Add(new Chat()
            {
                Id = newId,
                Name = formData.Name
            });

            //For each participant add a new entry in the chat/account junction table
            //todo: Is there a way for Entity Framework to do this automatically? It feels a bit redundant
            foreach (var participant in participants)
            {
                var p = new ChatAccount()
                {
                    Id = Guid.NewGuid(),
                    ChatId = newId,
                    AccountId = participant.Id
                };
                _chatAccountRepository.Add(p);
            }

            //Return to the current chat
            return RedirectToAction("Index", "Dashboard", formData.Id);
            //Form not valid: return to/refresh the current chat

            //todo Replace this refresh/redirect with JS to only refresh the messages container
        }
    }
}