using System.Web.Mvc;
using TwitterFeedOfSalesForce.Helpers;
using TwitterFeedOfSalesForce.Models;

namespace TwitterFeedOfSalesForce.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// If user is not logged in, open login view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Logs out current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            SessionHelper.IsAuthenticated = false;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Check for username - password entered. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var configManager = ConfigManager.Instance;

                // For the sake of simplicity, config based username - pass pair is defined.
                // @TODO Should be moved to DB or different data source.
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