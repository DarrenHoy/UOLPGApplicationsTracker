using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.Models;
using PGProgrammeApplications.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Controllers
{
    public class ApplicationsController : Controller
    {
        private PGProgrammeApplicationsEntities _dbContext;

        public ApplicationsController() 
        {
            _dbContext = new PGProgrammeApplicationsEntities();
            
        }

        private AppIdentity GetUser()
        {
            //return (AppIdentity)Request.GetOwinContext().Authentication.User.Identity;
            Func<AppIdentity> StudentUser = () =>
            {
                var user = _dbContext.Students.FirstOrDefault(s => s.EmailAddress == "steven.lawson@anemailprovider.com");
                return new AppIdentity(AppIdentityType.Student, user.Id, user.EmailAddress, user.EmailAddress, user.UserPassword, new List<string> { "Student" });
            };

            Func<AppIdentity> StaffUser = () =>
            {
                var user = _dbContext.AppUsers.FirstOrDefault(u => u.Username == "AdminSarah");
                return new AppIdentity(AppIdentityType.Staff, user.Id, user.Username, user.Username, user.UserPassword, user.AppUserRoleMembers.Select(r => r.UserRole.Description).ToList());
            };

            return StaffUser();
            //return StudentUser();
        }

        private Func<Application, ApplicationViewModel> ApplicationViewModelFromApplication(bool canChangeStatus, bool showStudent, IEnumerable<SelectListItem> statusValues)
        {
            return a => 
                new ApplicationViewModel()
                {
                    ApplicationId = a.Id,
                    StudentName = a.Student.FirstName + " " + a.Student?.LastName,
                    IsStudentVisible = showStudent,
                    AdmissionTerm = a.AdmissionTerm.Description,
                    ModeOfStudy = a.ModeOfStudy.Description,
                    ProgrammeOfStudy = a.ProgrammeOfStudy.Description,
                    Comments = a.Comments,
                    Status = a.Status.Id,
                    StatusValues = statusValues,
                    DisplayStatus = a.Status.Description,
                    Timestamp = a.ApplicationTimestamp.ToShortDateString(),
                    CanChangeStatus = canChangeStatus
                };
        }

        // GET: Applications
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "My Applications";

            var appUser = GetUser();

            //Ensure staff are redirected to the appropriate action - this configuratin does not make sense for admin users
            if (appUser.IdentityType == AppIdentityType.Staff)
            {
                return RedirectToAction("ViewAll");
            }

            var username = appUser.UserName;
            var applications =
                    _dbContext
                    .Applications
                    .Include("Status")
                    .Where(a => a.Student.EmailAddress == username)
                    .Select(ApplicationViewModelFromApplication(canChangeStatus: false, showStudent: false, statusValues: new List<SelectListItem>()))
                    .ToList();
            return View(applications);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ViewAll()
        {
            ViewBag.Title = "All Applications";

            var statusValues = _dbContext.ApplicationStatus.Include("Status").Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Description }).ToList();
            var applications =
                    _dbContext
                    .Applications
                    .Select(ApplicationViewModelFromApplication(canChangeStatus: true, showStudent:true, statusValues: statusValues))
                    .ToList();
            return View("Index", applications);
        }
    }
}