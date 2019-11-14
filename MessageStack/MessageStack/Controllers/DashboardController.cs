using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;

namespace MessageStack.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly MessageRepository _repository = new MessageRepository();


        /// <summary>
        /// Shows a message requested by the user upon opening a chat.
        /// </summary>
        [HttpGet]
        public ActionResult Message(string id)
        {
            //todo Grab all data via the repository required to show the message
            return View();
        }

        /// <summary>
        /// Validates and accepts or rejects a message submitted by the user.
        /// </summary>
        [HttpPost]
        public ActionResult Message(MessageViewModel model)
        {
            //Check if the form is valid
            if (!ModelState.IsValid) return View(model);

            //Add the account to the database using the repository
            var message = _repository.Add(
                new Message(
                    Guid.NewGuid(),
                    new Guid(model.SenderId),
                    model.SenderName,
                    model.Text,
                    DateTime.Now
                ));

            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult MessageComposer(string id)
        {

            return PartialView("_MessageComposer", new MessageViewModel() { ChatId = id });
        }

        [HttpPost]
        [ChildActionOnly]
        public void MessageComposer(MessageViewModel model)
        {
            var currentAccount = ((Account)Session["current"]);

            //Check if the form is valid
            //if (!ModelState.IsValid) return RedirectToAction("Index", "Chat", currentAccount.Id.ToString());
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Text))
            {
                //Add the account to the database using the repository
                var message = _repository.Add(new Message()
                {
                    Id = Guid.NewGuid(),
                    ChatId = new Guid(model.ChatId),
                    SenderId = new Guid(currentAccount.Id.ToString()),
                    SenderName = currentAccount.Name,
                    Text = model.Text,
                    TimeStamp = DateTime.Now
                }
                );

                //todo Prevent the textbox & send button from disappearing
                RedirectToAction("Index", "Chat", ((Account)Session["current"]).Id.ToString());
            }

            ModelState.AddModelError("message-error", "Could not send this message.");
        }
    }
}