using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace MessageStack.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepository;

        public AccountController() { }

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(Session["Loggedin_Account"]);
        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var account = _accountRepository.Login(model.Email, model.Password);

            if (account != null)
            {
                FormsAuthentication.SetAuthCookie(account.Email, false);
                Session["Loggedin_Account"] = account;
                Session["IsLoggedIn"] = true;

                return RedirectToAction("Index", "Account");
            }

            ModelState.AddModelError("Login error", "Your username or password is incorrect.");

            return View(model);
        }

        [Authorize]
        public ActionResult AccountChangeName()
        {
            var account = (Account)Session["Loggedin_Account"];

            var changeAccountModel = new AccountChangeViewModel
            {
                Firstname = account.Firstname,
                Lastname = account.Lastname
            };

            return View(changeAccountModel);
        }

        [HttpPost]
        public ActionResult AccountChangeName(AccountChangeViewModel model)
        {
            var account = (Account)Session["Loggedin_Account"];

            if (ModelState.IsValid)
            {
                var currentAccount = GetCurrentAccountFromDb(account.Id);

                if (currentAccount?.Password == Helpers.Encrypt.GenerateSHA512String(account.Password))
                {
                    if (model.Firstname != null || model.Lastname != null)
                    {
                        account.Firstname = model.Firstname;
                        account.Lastname = model.Lastname;
                        var result = _accountRepository.Update(account);
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Enter both first and last name");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "The provided password is incorrect");
                }
            }

            var accountChangeViewModel = new AccountChangeViewModel
            {
                Firstname = account.Firstname,
                Lastname = account.Lastname
            };
            return View(accountChangeViewModel);
        }

        [Authorize]
        public ActionResult AccountChangePassword()
        {
            var changePasswordModel = new AccountChangePasswordViewModel();
            return View(changePasswordModel);
        }

        [HttpPost]
        public ActionResult AccountChangePassword(AccountChangePasswordViewModel model)
        {
            var account = (Account)Session["Loggedin_Account"];
            if (!ModelState.IsValid) return View(model);

            var currentAccount = GetCurrentAccountFromDb(account.Id);

            if (currentAccount.Password == Helpers.Encrypt.GenerateSHA512String(model.CurrentPassword))
            {
                if (model.Password != null || model.RepeatPassword != null)
                {
                    account.Password = Helpers.Encrypt.GenerateSHA512String(model.Password);
                    var result = _accountRepository.Update(account);
                }
                else
                {
                    ModelState.AddModelError("Error", "Please enter both password fields");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "The entered passwords must be equal");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult AccountChangePhoneEmail()
        {
            return View(new AccountChangePhoneEmailViewModel());
        }

        [HttpPost]
        public ActionResult AccountChangePhoneEmail(AccountChangePhoneEmailViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var account = (Account)Session["Loggedin_Account"];

            if (!string.IsNullOrEmpty(model.Email))
            {
                account.Email = model.Email;
            }
            if (model.Phonenumber != null && model.Email != null)
            {
                account.Phonenumber = model.Phonenumber;
            }

            return View();
        }

        [Authorize]
        public ActionResult LogUit()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public Account GetCurrentAccountFromDb(Guid id) => _accountRepository.FirstOrDefault(a => a.Id == id).Result;
    }
}