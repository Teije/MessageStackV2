using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageStack.Models;
using MessageStack.Models.ViewModels;

namespace MessageStack.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index() => View("Index", new DashboardViewModel()
        {
            CurrentUser = (Account)Session["current"]
        });
    }
}