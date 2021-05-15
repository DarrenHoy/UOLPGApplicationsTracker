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
        public ActionResult Login(string returnUrl)
        {
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
                return View(model);
            }

            var owinContext = Request.GetOwinContext();

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user.IsPasswordValid(model.Password))
            {
                owinContext.Authentication.SignIn(user);
                if (returnUrl != null)
                {
                    return new RedirectResult(returnUrl);
                }

                return RedirectToAction("Index", "Home", new { });
            }


            ViewBag.ErrorMessage = "Sorry, we couldn't find those details.";
            return View();
            
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}