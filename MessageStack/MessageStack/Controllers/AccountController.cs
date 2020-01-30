using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;
using System;
using System.Web.Mvc;
using System.Web.Security;
using MessageStack.Context;

namespace MessageStack.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController() : this (RepositoryContext.GetInstance())
        {

        }

        public AccountController(MessageStackContext messageStackContext) : base(messageStackContext)
        {

        }

        public ActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
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

                var session = Session["Loggedin_Account"];

                return RedirectToAction("Index", "Account");
            }

            ModelState.AddModelError("Login error", "Your username or password is incorrect.");

            return View(model);
        }

        public ActionResult AccountChangeName()
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var account = (Account) Session["Loggedin_Account"];

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
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var account = (Account) Session["Loggedin_Account"];

            if (ModelState.IsValid)
            {
                if (account.Password == Helpers.Encrypt.GenerateSHA512String(model.CurrentPassword))
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
            return RedirectToAction("Index","Account")

            ;
        }

        public ActionResult AccountChangePassword()
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var changePasswordModel = new AccountChangePasswordViewModel();
            return View(changePasswordModel);

        }

        [HttpPost]
        public ActionResult AccountChangePassword(AccountChangePasswordViewModel model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            var account = (Account) Session["Loggedin_Account"];
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

        public ActionResult AccountChangePhoneEmail()
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            return View(new AccountChangePhoneEmailViewModel());

        }

        [HttpPost]
        public ActionResult AccountChangePhoneEmail(AccountChangePhoneEmailViewModel model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Index", "Home");
            if (!ModelState.IsValid) return View();

            var account = (Account) Session["Loggedin_Account"];

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

        public ActionResult LogOut()
        {
            if (IsLoggedIn())
            {

                FormsAuthentication.SignOut();
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public Account GetCurrentAccountFromDb(Guid id) => _accountRepository.FirstOrDefault(a => a.Id == id);
    }
}