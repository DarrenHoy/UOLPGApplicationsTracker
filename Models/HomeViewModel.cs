using PGProgrammeApplications.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGProgrammeApplications.Models
{
    public class HomeViewModel
    {
        public AppIdentityType HomePageType { get; set; }
        public string UserDisplayName { get; set; }
        public bool CanApply { get; internal set; }
        public Guid StudentId { get; internal set; }
    }
}