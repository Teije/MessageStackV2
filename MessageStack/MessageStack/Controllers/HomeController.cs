using System.Web.Mvc;
using MessageStack.Context;

namespace MessageStack.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController() : this (new MessageStackContext())
        {
        }
        public HomeController(MessageStackContext messageStackContext) : base(messageStackContext)
        {
        }

        public ActionResult Index()
        {
            if (IsLoggedIn()) return RedirectToAction("Index", "Account");

            return View();
        }

    }
}