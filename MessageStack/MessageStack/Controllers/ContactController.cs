using MessageStack.Models;
using MessageStack.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MessageStack.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ContactRepository _contactRepository;
        private readonly AccountRepository _accountRepository;

        public ContactController() { }

        public ContactController(ContactRepository contactRepository, AccountRepository accountRepository)
        {
            _contactRepository = contactRepository;
            _accountRepository = accountRepository;
        }

        public ActionResult Index()
        {
            return View(_contactRepository.GetWhere(c => c.OwnerAccount == Session["Loggedin_Account"]).Result.ToList());
        }

        [HttpPost]
        public ActionResult RemoveContact(Guid id)
        {
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
            return View();
        }

        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (Session?["Loggedin_Account"] == null) return View(contact);

                var account = _accountRepository.FirstOrDefault(a => a.Phonenumber == contact.Phonenumber).Result;

                if (account.Email == contact.Email)
                {
                    contact.OwnerAccount = (Account)Session["Loggedin_Account"];
                    contact.TargetAccount = account;

                    var result = _contactRepository.Add(contact).Result;
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

        public ActionResult ChangeContact(Guid id) => View(_contactRepository.GetById(id).Result);


        [HttpPost]
        public ActionResult ChangeContact(Contact contact)
        {
            var result = _contactRepository.Update(contact);
            return RedirectToAction("Index");
        }
    }
}