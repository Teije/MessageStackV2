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
    public class ContactController : Controller
    {
        private readonly ContactRepository _contactRepository = new ContactRepository();
        private readonly AccountRepository _accountRepository = new AccountRepository();
        private Account CurrentAccount => (Account)Session["current"];

        /// <summary>
        /// Return a view that allows for creating a new chat for the current user
        /// </summary>
        public ActionResult Create() => View("Create", new ContactViewModel()
        {
            Contacts = _accountRepository.GetAll().Select(a => new Contact()
            {
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                OwnerAccountId = CurrentAccount.Id,
                ContactAccountId = a.Id
            })
                .ToList()
                .Except(_contactRepository.GetFor(CurrentAccount.Id))
                .ToList()

        });

        /// <summary>
        /// Validate & save the "create contact"-form data
        /// </summary>
        [HttpPost]
        public ActionResult Create(ContactViewModel formData)
        {
            //Check if the form is valid
            if (formData.Contacts.Count <= 0)
                return RedirectToAction("Index", "Dashboard", formData.Id);

            var selectedContacts = formData.Contacts.Where(c => c.IsSelected);

            //Add the contacts to the database
            foreach (var contact in selectedContacts)
            {
                _contactRepository.Add(new Contact()
                {
                    Id = Guid.NewGuid(),
                    Name = contact.Name,
                    PhoneNumber = contact.PhoneNumber,
                    OwnerAccountId = CurrentAccount.Id,
                    ContactAccountId = contact.ContactAccountId,
                });
            }

            //Return to the dashboard chat
            return RedirectToAction("Index", "Dashboard", formData.Id);
        }

        /// <summary>
        /// Return a view with all contacts that belong to the specified account id
        /// </summary>
        [ChildActionOnly]
        public ActionResult ContactList()
        {
            var model = new ContactListViewModel()
            {
                Contacts = _contactRepository
                    .GetFor(CurrentAccount.Id)
                    .OrderBy(c => c.Name)
                    .ToList()
            };

            return PartialView("_ContactList", model);
        }
    }
}