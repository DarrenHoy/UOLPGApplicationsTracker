//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PGProgrammeApplications.DataContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public System.Guid Id { get; set; }
        public System.Guid AdmissionTermId { get; set; }
        public System.Guid ProgrammeOfStudyId { get; set; }
        public System.Guid ModeOfStudyId { get; set; }
        public string Comments { get; set; }
        public System.DateTime ApplicationTimestamp { get; set; }
        public System.Guid StudentId { get; set; }
        public System.Guid ApplicationStatusId { get; set; }
    
        public virtual AdmissionTerm AdmissionTerm { get; set; }
        public virtual ModeOfStudy ModeOfStudy { get; set; }
        public virtual ProgrammeOfStudy ProgrammeOfStudy { get; set; }
        public virtual Student Student { get; set; }
        public virtual ApplicationStatu ApplicationStatu { get; set; }
    }
}