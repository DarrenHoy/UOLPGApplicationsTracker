using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Models
{
    public class EditApplicationViewModel
    {
        public Guid ApplicationId { get; set; }

        public string StudentName { get; set; }

        public string AdmissionTerm { get; set; }

        public string ProgrammeOfStudy { get; set; }

        public string ModeOfStudy { get; set; }

        public string Comments { get; set; }

        public string Timestamp { get; set; }

        public int Status { get; set; }

        public string DisplayStatus { get; set; }

        public bool CanChangeStatus { get; set; }

        public bool IsStudentVisible { get; set; }

        public IEnumerable<SelectListItem> StatusValues;

    }
}