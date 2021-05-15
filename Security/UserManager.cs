using Microsoft.AspNet.Identity;
using PGProgrammeApplications.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PGProgrammeApplications.Security
{
    public class UserManager:UserManager<AppIdentity>
    {
        private PGProgrammeApplicationsEntities _dataContext;

        public UserManager(IUserStore<AppIdentity> userStore):base(userStore)
        {
            
        }

        



    }
}