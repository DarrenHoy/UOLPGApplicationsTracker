using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.Models;
using PGProgrammeApplications.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Impersonate(string user, string userType)
        {
            Func<string, AppIdentity> ImpersonateStudent = email =>
            {
                var student = _db.Students.FirstOrDefault(s => s.Username == email);
                return new AppIdentity(student.Id, student.EmailAddress, student.EmailAddress, student.UserPassword, new List<string>() { "Student" });
            };

            Func<string, AppIdentity> ImpersonateStaff = username =>
            {
                var staff = _db.AppUsers.FirstOrDefault(s => s.Username == username);
                return new AppIdentity(staff.Id, staff.Username, staff.Username, staff.UserPassword, staff.AppUserRoleMembers.Select(r => r.UserRole.Description).ToList());
            };

            var impersonate = userType == "Staff" ? ImpersonateStaff : ImpersonateStudent;

            Request.GetOwinContext().Authentication.SignIn(impersonate(user));
            return RedirectToAction("Index", "Home", new { });
        }

        [AllowAnonymous]
        public ActionResult Impersonate()
        {
            return View();           
        }


        private PGProgrammeApplicationsEntities _db;

        public HomeController()
        {
            _db = new PGProgrammeApplicationsEntities();
        }

        
        public async Task<ActionResult> Index()
        {
            var user = Request.GetOwinContext().Authentication.User;
            var identity = (ClaimsIdentity)user.Identity;
            var idClaim = identity.Claims.FirstOrDefault(c => c.Type == "DatabaseId")?.Value;
            if (idClaim == null)
            {
                Request.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var id = Guid.Parse(idClaim);

            if (user.IsInRole("Student"))
            {
                var student = await _db.Students.FindAsync(id);
                var viewModel = new HomeViewModel()
                {
                    HomePageType=AppIdentityType.Student,
                    UserDisplayName = student.FirstName + " " + student.LastName,
                    CanApply = student.Applications.Count() < 2,
                    StudentId = id
                };
                return View(viewModel);
            }

            if (user.IsInRole("Administrator"))
            {
                var staff = await _db.AppUsers.FindAsync(id);
                var viewModel = new HomeViewModel()
                {
                    HomePageType = AppIdentityType.Staff,
                    UserDisplayName = staff.DisplayName,
                    CanApply = false,
                    StudentId = Guid.Empty
                };

                return View(viewModel);
            }

            return View();

            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }



    }
}