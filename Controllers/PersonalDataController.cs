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
    public class PersonalDataController : Controller
    {
        private PGProgrammeApplicationsEntities _db;

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
            
            return View(CreateViewModel(student));
        }

        

        public async Task<ActionResult> Edit ()
        {
            var user = Request.GetOwinContext().Authentication.User;
            var studentIdClaim = ((ClaimsIdentity)user.Identity).Claims.FirstOrDefault(c => c.Type == "DatabaseId")?.Value;

            if (studentIdClaim == null)
            {
                Request.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            var studentId = Guid.Parse(studentIdClaim);
            
            
            var student = await _db.Students.FindAsync(studentId);
            if (student == null)
            {
                return new HttpNotFoundResult();
            }

            return View(CreateViewModel(student));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid? id, StudentDetailViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }


            var student = await _db.Students.FindAsync(id);
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.EmailAddress = request.EmailAddress;
            student.DateOfBirth = request.DateOfBirth;
            student.IsUkResident = request.IsUkResident;
            await _db.SaveChangesAsync();
            

            return RedirectToAction("Index");

        }

        private StudentDetailViewModel CreateViewModel(Student student)
        {
            return new StudentDetailViewModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EmailAddress = student.EmailAddress,
                DateOfBirth = student.DateOfBirth,
                DisplayDateOfBirth = student.DateOfBirth.ToShortDateString(),
                IsUkResident = student.IsUkResident,
                IsUkResidentDisplay = student.IsUkResident == "Y" ? "Yes" : "No"

            };
        }
    }
}