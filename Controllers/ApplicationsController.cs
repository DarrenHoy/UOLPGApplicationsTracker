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

            var appUser = Request.GetOwinContext().Authentication.User;
            var appIdentity = (ClaimsIdentity)appUser.Identity;

            //Ensure staff are redirected to the appropriate action - this configuratin does not make sense for admin users
            if (appUser.IsInRole("Administrator"))
            {
                return RedirectToAction("ViewAll");
            }

            var username = appIdentity.Name;
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