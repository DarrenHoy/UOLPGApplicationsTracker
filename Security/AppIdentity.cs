using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace PGProgrammeApplications.Security
{
    public class AppIdentity : ClaimsIdentity, IUser
    {
        private string _id;
        private string _username;
        private string _password;


        public AppIdentity(Guid databaseId, string id, string username, string password, IEnumerable<string> roles):base(DefaultAuthenticationTypes.ApplicationCookie)
        {
            _id = id;
            _username = username;
            _password = password;
            

            //Requried for the AntiForgeryToken
            AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "SqlServer"));
            AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            //Add the roles from the database and the Authorize attribute just works
            AddClaims(roles.Select(r => new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", r)));
            AddClaim(new Claim("DatabaseId", databaseId.ToString()));
            
        }

        public string Id => _id;

        public string UserName { get => _username; set => _username=value; }

        public bool IsPasswordValid(string password)
        {
            return _password == password;
        }

        

    }
}