using Microsoft.AspNet.Identity;
using PGProgrammeApplications.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PGProgrammeApplications.Security
{
    public class DatabaseUserStore : IUserStore<AppIdentity>
    {
        private PGProgrammeApplicationsEntities _dataContext;

        public DatabaseUserStore()
        {
            _dataContext = new PGProgrammeApplicationsEntities();
        }
        public Task CreateAsync(AppIdentity user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppIdentity user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<AppIdentity> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }


        public async Task<AppIdentity> FindByNameAsync(string userName)
        {
            return await Task.Run(() => { 
                var student = _dataContext.Students.FirstOrDefault(s => s.EmailAddress == userName);
                if (student != null)
                {
                    return new AppIdentity(
                                        student.Id,
                                        student.EmailAddress, 
                                        student.EmailAddress, 
                                        student.UserPassword, 
                                        new List<string> { "Student" });
                }

                var staff = _dataContext.AppUsers.Include("AppUserRoleMembers.UserRole").FirstOrDefault(s => s.Username == userName);
                if (staff != null)
                {
                    return new AppIdentity(
                                            staff.Id,
                                            staff.Username, 
                                            staff.Username, 
                                            staff.UserPassword, 
                                            staff.AppUserRoleMembers.Select(a => a.UserRole.Description).ToList());
                }

                return null;
            });
        }

        public Task UpdateAsync(AppIdentity user)
        {
            throw new NotImplementedException();
        }
    }
}