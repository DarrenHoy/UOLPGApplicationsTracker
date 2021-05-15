using PGProgrammeApplications.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGProgrammeApplications.Models
{
    public class CreateApplicationRequest
    {
        [Required]
        public Guid AdmissionTerm { get; set; }

        [Required]
        public Guid ProgrammeOfStudy { get; set; }

        [Required]
        public ModeOfStudy ModeOfStudy { get; set; }

        public string Comments { get; set; }


    }
}