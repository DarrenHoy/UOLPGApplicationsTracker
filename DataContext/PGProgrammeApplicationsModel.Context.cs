﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PGProgrammeApplicationsEntities : DbContext
    {
        public PGProgrammeApplicationsEntities()
            : base("name=PGProgrammeApplicationsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdmissionTerm> AdmissionTerms { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationStatu> ApplicationStatus { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppUserRoleMember> AppUserRoleMembers { get; set; }
        public virtual DbSet<ModeOfStudy> ModeOfStudies { get; set; }
        public virtual DbSet<ProgrammeOfStudy> ProgrammeOfStudies { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
    }
}
