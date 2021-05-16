using PGProgrammeApplications.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Models
{
    public class CreateApplicationViewModel
    {
        
        public Guid StudentId { get; set; }

        [Required(ErrorMessage ="An Admission Term is required")]
        [Display(Name ="Admission Term")]
        public Guid AdmissionTermId { get; set; }

        [Required(ErrorMessage = "A Programme of Study is required")]
        [Display(Name ="Programme of Study")]
        public Guid ProgrammeOfStudyId { get; set; }

        [Required(ErrorMessage = "A Mode of Study")]
        [Display(Name = "Mode of Study")]
        public int ModeOfStudyId { get; set; }

        public string Comments { get; set; }

        public IEnumerable<SelectListItem> Programmes { get; set; }
        public IEnumerable<SelectListItem> AdmissionTerms { get; set; }
        public IEnumerable<SelectListItem> ModesOfStudy { get; set; }


        public Application ToApplication(int applicationStatusId)
        {
            return new Application()
            {
                AdmissionTermId = this.AdmissionTermId,
                ApplicationStatusId = applicationStatusId,
                ModeOfStudyId = this.ModeOfStudyId,
                ProgrammeOfStudyId = this.ProgrammeOfStudyId,
                StudentId = this.StudentId,
                ApplicationTimestamp = DateTime.Now,
                Comments = this.Comments
            };
        } 
    }
}