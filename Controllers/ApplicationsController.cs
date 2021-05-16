using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.Models;
using PGProgrammeApplications.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        private Func<Application, EditApplicationViewModel> ApplicationViewModelFromApplication(bool canChangeStatus, bool showStudent, IEnumerable<SelectListItem> statusValues)
        {
            return a => 
                new EditApplicationViewModel()
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

        public async Task<ActionResult> Create()
        {
            var appUser = Request.GetOwinContext().Authentication.User;
            if (!appUser.IsInRole("Student"))
            {
                return RedirectToAction("Index");
            }

            var studentIdClaim = ((ClaimsIdentity)appUser.Identity).Claims.FirstOrDefault(c => c.Type == "DatabaseId")?.Value;

            //If there is no Database ID on the claim something has gone wrong - need to sign in again
            if (studentIdClaim == null)
            {
                Request.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var studentId = Guid.Parse(studentIdClaim);

            var modesOfStudy = await _dbContext.ModeOfStudies.OrderBy(m=>m.Description).Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.Description }).ToListAsync();
            var programmes = await _dbContext.ProgrammeOfStudies.OrderBy(p=>p.Description).Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Description }).ToListAsync();
            var admissionTerms = await _dbContext.AdmissionTerms.OrderBy(a=>a.Description).Where(a=>a.StartDate > DateTime.Now).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Description }).ToListAsync();

            return View(
                new CreateApplicationViewModel()
                {
                    StudentId=studentId,
                    ModesOfStudy=modesOfStudy,
                    Programmes=programmes,
                    AdmissionTerms=admissionTerms
                });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateApplicationViewModel request)
        {
            var defaultStatus = _dbContext.ApplicationStatus.FirstOrDefault(s => s.Description == "Submitted");
            var application = request.ToApplication(defaultStatus.Id);
            _dbContext.Applications.Attach(application);
            _dbContext.Entry(application).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}