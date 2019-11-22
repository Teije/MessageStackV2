using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using MessageStack.Models;
using MessageStack.Models.ViewModels;
using MessageStack.Repositories;

namespace MessageStack.Controllers
{
    /// <summary>
    /// The AccountController handles all Account related actions, such as Login, Register and Logout
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly AccountRepository _repository = new AccountRepository();

        #region Login
        /// <summary>
        /// Returns the login view (form).
        /// </summary>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Validates login form data and sends it to the repository upon successful validation.
        /// </summary>
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            //Get the Account values from the form
            var account = _repository.FindByLoginDetails(model.PhoneNumber.Replace(" ", ""), model.Password);

            //Check if the values from the account resulted in a valid object
            if (account != null)
            {
                //Set the authentication cookie
                FormsAuthentication.SetAuthCookie(account.PhoneNumber, false);

                account = new Account(account.Id, account.Name, account.PhoneNumber, account.Password, account.Contacts);

                //Set the current session
                Session["current"] = account;

                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError("login-error", "The provided username or password is incorrect");

            return View(model);
        }
        #endregion

        #region Register
        /// <summary>
        /// Returns the registration view (form).
        /// </summary>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Validates registration form data and sends it to the repository upon successful validation.
        /// </summary>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //Check if the form is valid
            if (!ModelState.IsValid) return View(model);
            //Check if the phone number is already in use
            if (_repository.Find(a => a.PhoneNumber == model.PhoneNumber) != null)
            {
                ModelState.AddModelError("register-error", "The phone number is already in use.");
                return View(model);
            }

            //Add the account to the database using the repository
            var account = _repository.Add(
                new Account(
                    Guid.NewGuid(),
                    model.Name,
                    model.PhoneNumber,
                    model.Password
                    ));

            if (account != null)
            {
                FormsAuthentication.SetAuthCookie(account.PhoneNumber, true);
                Session["current"] = account;
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("register-error", "One of the fields was not filled in correctly");
            return View(model);
        }
        #endregion

        #region Logout
        /// <summary>
        /// Log out the current user from its session.
        /// </summary>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}