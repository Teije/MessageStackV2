using MessageStack.Models;
using MessageStack.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;
using MessageStack.Context;

namespace MessageStack.Controllers
{
    public class ContactController : BaseController
    {

        public ContactController() : this (RepositoryContext.GetInstance())
        {

        }

        public ContactController(MessageStackContext messageStackContext) : base(messageStackContext)
        {

        }

        public ActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var loggedInAccount = (Account) Session["Loggedin_Account"];
            var contacts = _contactRepository.GetWhere(c => c.OwnerAccount.Id == loggedInAccount.Id).ToList();

            foreach (var contact in contacts)
            {
                contact.OwnerAccount = loggedInAccount;
                contact.TargetAccount = _accountRepository.FirstOrDefault(a => a.Phonenumber == contact.Phonenumber);
            }

            return View(contacts);
        }

        [HttpPost]
        public ActionResult RemoveContact(Guid id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var result = _contactRepository.Remove(id);
                return View("Index");
            }

            ModelState.AddModelError("Error", "The contact has not been removed. Try again.");
            return View();
        }

        public ActionResult CreateContact()
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                if (Session?["Loggedin_Account"] == null) return View(contact);

                var account = _accountRepository.FirstOrDefault(a => a.Phonenumber == contact.Phonenumber);

                if (account.Email == contact.Email)
                {
                    contact.OwnerAccount = (Account)Session["Loggedin_Account"];
                    contact.TargetAccount = account;

                    var result = _contactRepository.Add(contact);
                    return View("~/Views/Contact/Index.cshtml");
                }

                ModelState.AddModelError("Error", "The supplied contact does not exist on our platform");
            }
            else
            {
                ModelState.AddModelError("Error", "The supplied contact does not exist on our platform");
            }

            return View(contact);
        }

        public ActionResult ChangeContact(Guid id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");

            return View(_contactRepository.GetById(id));
        }



        [HttpPost]
        public ActionResult ChangeContact(Contact contact)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");

            var result = _contactRepository.Update(contact);
            return RedirectToAction("Index");
        }
    }
}