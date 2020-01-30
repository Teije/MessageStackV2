using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageStack.Context;
using MessageStack.Repositories;

namespace MessageStack.Controllers
{
    public class BaseController : Controller
    {
        protected readonly AccountRepository _accountRepository;
        protected readonly ContactRepository _contactRepository;
        protected readonly GroupChatRepository _groupChatRepository;
        protected readonly GroupMessageRepository _groupMessageRepository;
        protected readonly PrivateChatRepository _privateChatRepository;
        protected readonly PrivateMessageRepository _privateMessageRepository;

        public BaseController(MessageStackContext messageStackContext)
        {
            _accountRepository = new AccountRepository(messageStackContext);
            _contactRepository = new ContactRepository(messageStackContext);
            _groupChatRepository = new GroupChatRepository(messageStackContext);
            _groupMessageRepository = new GroupMessageRepository(messageStackContext);
            _privateChatRepository = new PrivateChatRepository(messageStackContext);
            _privateMessageRepository = new PrivateMessageRepository(messageStackContext);
        }

        public bool IsLoggedIn() => Session["Loggedin_Account"] != null;
    }
}