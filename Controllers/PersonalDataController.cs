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
    public class PersonalDataController : Controller
    {
        private PGProgrammeApplicationsEntities _db;

        public PersonalDataController()
        {
            _db = new PGProgrammeApplicationsEntities();
            
        }

        // GET: Personal
        [RowLevelAuth(ExcludeRoles: "Administrator")]
        public async Task<ActionResult> Detail(Guid? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var student = await _db.Students.FindAsync(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            
            return View(CreateViewModel(student));
        }


        [RowLevelAuth(ExcludeRoles: "Administrator")]
        public async Task<ActionResult> Edit (Guid? id)
        {
            if(!id.HasValue)
            {
                return HttpNotFound();
            }
            
            var student = await _db.Students.FindAsync(id.Value);
            if (student == null)
            {
                return new HttpNotFoundResult();
            }

            return View(CreateViewModel(student));
        }

        [HttpPost]
        [RowLevelAuth(ExcludeRoles: "Administrator")]
        public async Task<ActionResult> Edit(Guid? id, StudentDetailViewModel request)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

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


            return RedirectToAction("Detail", new { Id = id.Value });
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