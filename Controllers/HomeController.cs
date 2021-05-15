using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PGProgrammeApplicationsEntities _dataContext;

        [Authorize]
        public ActionResult Index()
        {
            var user = Request.GetOwinContext().Authentication.User.Identity;
            var viewModel = new HomeViewModel() { UserDisplayName = "" };
            return View(viewModel);
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}