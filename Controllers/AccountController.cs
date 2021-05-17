using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PGProgrammeApplications.Models;
using PGProgrammeApplications.Security;

namespace PGProgrammeApplications.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager _userManager;

        public AccountController()
        {
            _userManager = new UserManager(new DatabaseUserStore());
        }

        
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string errorMessage)
        {
            if (errorMessage != String.Empty)
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Account", new { errorMessage = "Sorry, we couldn't find those details", returnUrl=returnUrl });
            }

            /*
             * This might seem a bit ostentatious for a demo app, but it buys quite a lot in
             * terms of the ability to use the standard [Authorize(Roles="...")] attributes as well the ability
             * to attach arbitrary data (i.e. the DatabaseId claim) to the authentication cookie.  This is really
             * useful in the ability to abstact the logic of the [RowLevelAuth] attribute out of the controller,
             * because the identity system is managing it and just implicitly passing that data along with every request.
             * 
             * Also, it becomes quite trivial to hand over this methodology to an external Claims provider - you
             * just have the provider attach the DatabaseId (or whatever you like) claim
             */

            var owinContext = Request.GetOwinContext();

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user.IsPasswordValid(model.Password))
            {
                owinContext.Authentication.SignIn(user);
                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Home", new { });
                }

                return new RedirectResult(returnUrl);
            }


            return RedirectToAction("Login", "Account", new { errorMessage = "Sorry, we couldn't find those details", returnUrl = returnUrl });

        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}