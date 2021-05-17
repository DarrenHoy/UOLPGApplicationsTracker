using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Filters
{
    /// <summary>
    /// Applies row-level security against the DatabaseId claim for a given method. The method must require an Id parameter of type GUID.  Users in roles identified by ExcludeRoles skip all row-level security checks.
    /// </summary>
    public class RowLevelAuth : System.Web.Mvc.AuthorizeAttribute
    {
        private IEnumerable<string> _excludeRoles;

        public RowLevelAuth(params string[] ExcludeRoles)
        {
            _excludeRoles = new List<string>(ExcludeRoles);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var user = filterContext.HttpContext.Request.GetOwinContext().Authentication.User;
            var identity = (ClaimsIdentity)user.Identity;
            if (!identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            if (_excludeRoles.Any(r=> user.IsInRole(r)))
            {
                return;
            }


            var userIdClaim = identity.Claims.FirstOrDefault(c => c.Type == "DatabaseId")?.Value;
            if (userIdClaim == null)
            {
                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            Guid userId = Guid.Parse(userIdClaim);

            var hasId = filterContext.RequestContext.RouteData.Values.Keys.Contains("id");

            if (hasId)
            {
                var id = Guid.Parse(filterContext.RequestContext.RouteData.Values["id"].ToString());
                if (!(id == userId))
                {
                    filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }
            }
        }

        
    }
}