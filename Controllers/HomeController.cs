using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.Models;
using PGProgrammeApplications.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private PGProgrammeApplicationsEntities _db;

        public HomeController()
        {
            _db = new PGProgrammeApplicationsEntities();
        }

        
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

        public ActionResult Impersonate()
        {
            Func<AppIdentity> ImpersonateStudent = () =>
            {
                var student = _db.Students.FirstOrDefault(s => s.EmailAddress == "steven.lawson@anemailprovider.com");
                return new AppIdentity(student.Id, student.EmailAddress, student.EmailAddress, student.UserPassword, new List<string>() { "Student" });
            };

            Func<AppIdentity> ImpersonateStaff = () =>
            {
                var staff = _db.AppUsers.FirstOrDefault(s => s.Username == "AdminSarah");
                return new AppIdentity(staff.Id, staff.Username, staff.Username, staff.UserPassword, staff.AppUserRoleMembers.Select(r=>r.UserRole.Description).ToList());
            };


            Request.GetOwinContext().Authentication.SignIn(ImpersonateStudent());
            //Request.GetOwinContext().Authentication.SignIn(ImpersonateStaff());
            return RedirectToAction("Create", "Applications", new { });
            //return RedirectToAction("Index", "Applications");
        }
    }
}