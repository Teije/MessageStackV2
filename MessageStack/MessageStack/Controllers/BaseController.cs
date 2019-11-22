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
    public class BaseController : Controller
    {
        private readonly AccountRoleRepository _repository = new AccountRoleRepository();

        protected bool IsAuthorized(Guid accountId)
        {
            var existingRole = _repository.Find(a => a.AccountId == accountId);
            return existingRole != null;
        }
    }
}