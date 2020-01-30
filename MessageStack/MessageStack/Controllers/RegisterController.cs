using System.Linq;
using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;
using System.Web.Mvc;
using System.Web.Security;
using MessageStack.Context;

namespace MessageStack.Controllers
{
    public class RegisterController : BaseController
    {
        public RegisterController() : this (RepositoryContext.GetInstance())
        {
        }

        public RegisterController(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }

        public ActionResult Index()
        {
            if (Session["IsLoggedIn"] != null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            if (_accountRepository.FirstOrDefault(a => a.Email == registerViewModel.Email || a.Phonenumber == registerViewModel.Phonenumber) == null)
            {
                var account = new Account
                {
                    Firstname = registerViewModel.Firstname,
                    Lastname = registerViewModel.Lastname,
                    Email = registerViewModel.Email,
                    Phonenumber = registerViewModel.Phonenumber,
                    Password = Helpers.Encrypt.GenerateSHA512String(registerViewModel.Password)
                };

                if (_accountRepository.Add(account) != null)
                {
                    Session["Loggedin_Account"] = account;
                    FormsAuthentication.SetAuthCookie(account.Email, false);
                    Session["IsLoggedIn"] = true;

                    return RedirectToAction("Index", "Account");
                }

                ModelState.AddModelError("Error", "You account could not be created, please try again");
            }
            else
            {
                ModelState.AddModelError("Error", "The provided information is already in use");
            }

            return RedirectToAction("Index", "Account");
        }
    }
}