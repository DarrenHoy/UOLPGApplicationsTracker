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

        // GET: Applications
        [Authorize]
        public ActionResult Index()
        {
            var appUser = (AppIdentity)Request.GetOwinContext().Authentication.User.Identity;

            //Ensure staff are redirected to the appropriate page
            if (appUser.IdentityType == AppIdentityType.Staff)
            {
                return RedirectToAction("ViewAll");
            }

            var username = appUser.UserName;
            var applications =
                    _dbContext
                    .Applications
                    .Where(a => a.Student.EmailAddress == username)
                    .Select(a => new ApplicationViewModel()
                    {
                        AdmissionTerm = a.AdmissionTerm.Description,
                        ModeOfStudy = a.ModeOfStudy.ToString(),
                        ProgrammeOfStudy = a.ProgrammeOfStudy.Description,
                        Comments = a.Comments,
                        Status = a.ApplicationStatus.ToString(),
                        Timestamp = a.ApplicationTimestamp.ToShortDateString()
                    });
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ViewAll()
        {
            return View();
        }
    }
}