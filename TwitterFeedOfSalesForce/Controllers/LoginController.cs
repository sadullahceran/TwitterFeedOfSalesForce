using System.Web.Mvc;
using TwitterFeedOfSalesForce.Helpers;
using TwitterFeedOfSalesForce.Models;

namespace TwitterFeedOfSalesForce.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Logout()
        {
            SessionHelper.IsAuthenticated = false;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var configManager = ConfigManager.Instance;

                if (model.Username == configManager.DefaultUsername && model.Password == configManager.DefaultPassword)
                {
                    SessionHelper.IsAuthenticated = true;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }
    }
}