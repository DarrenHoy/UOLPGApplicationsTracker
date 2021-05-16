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
    public class PersonalDataController : Controller
    {
        private PGProgrammeApplicationsEntities _db;
        private AppIdentity _appIdentity;


        public PersonalDataController()
        {
            _db = new PGProgrammeApplicationsEntities();
            
        }

        // GET: Personal
        public async Task<ActionResult> Index()
        {
            var sessionUser = Request.GetOwinContext().Authentication.User;
            var sessionIdentity = (ClaimsIdentity)sessionUser.Identity;

            if (sessionUser.IsInRole("Administrator"))
            {
                var students = await Task.Run(() => _db.Students.ToList());
                return View(students);
            }

            var student = await _db.Students.FindAsync(Guid.Parse(sessionUser.Claims.FirstOrDefault(c=>c.Type=="DatabaseId").Value));
            var EditStudentDetailViewModel = new StudentDetailViewModel()
            {
                Id=student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EmailAddress = student.EmailAddress,
                DateOfBirth = student.DateOfBirth.ToShortDateString(),
                IsUkResident = student.IsUkResident,
                IsUkResidentDisplay = student.IsUkResident == "Y" ? "Yes" : "No"

            };
            return View(EditStudentDetailViewModel);
        }

        

        public async Task<ActionResult> Edit (Guid? id, StudentDetailViewModel request)
        {
            if (!ModelState.IsValid)
            {

            }

            return View();
        }
    }
}