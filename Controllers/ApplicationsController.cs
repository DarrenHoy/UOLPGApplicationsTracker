using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.Filters;
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
    [Authorize]
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
        [RowLevelAuth(ExcludeRoles: "Administrator")]
        public ActionResult Student(Guid? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            ViewBag.Title = "My Applications";
            
            var applications =
                    _dbContext
                    .Applications
                    .Include("Status")
                    .Where(a => a.Student.Id == id.Value)
                    .Select(ApplicationViewModelFromApplication(canChangeStatus: false, showStudent: false, statusValues: new List<SelectListItem>()))
                    .ToList();
            return View("Index",applications);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
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

        [RowLevelAuth(ExcludeRoles: "Administrator")]
        public async Task<ActionResult> Create(Guid? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var studentId = id.Value;

            var modesOfStudy = await _dbContext.ModeOfStudies.OrderBy(m=>m.Description).Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.Description }).ToListAsync();
            var programmes = await _dbContext.ProgrammeOfStudies.OrderBy(p=>p.Description).Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Description }).ToListAsync();
            var admissionTerms = await _dbContext.AdmissionTerms.OrderBy(a=>a.Description).Where(a=>a.StartDate > DateTime.Now).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Description }).ToListAsync();

            return View(
                new CreateApplicationViewModel()
                {
                    ModesOfStudy=modesOfStudy,
                    Programmes=programmes,
                    AdmissionTerms=admissionTerms
                });
        }

        [HttpPost]
        [RowLevelAuth(ExcludeRoles: "Administrator")]
        public async Task<ActionResult> Create(Guid? id, CreateApplicationViewModel request)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            
            var defaultStatus = _dbContext.ApplicationStatus.FirstOrDefault(s => s.Description == "Submitted");
            var application = request.ToApplication(id.Value, defaultStatus.Id);
            _dbContext.Applications.Attach(application);
            _dbContext.Entry(application).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Student", new { id = id });
        }
    }
}