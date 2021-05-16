using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PGProgrammeApplications.ApiControllers
{
    public class ApplicationsController : ApiController
    {
        private PGProgrammeApplicationsEntities _db;

        public ApplicationsController()
        {
            _db = new PGProgrammeApplicationsEntities();
            _db.Configuration.ProxyCreationEnabled=false;
        }

        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public async Task<IHttpActionResult> Post(Guid? id, [FromBody] ChangeStatusRequest request)
        {

            if (!id.HasValue)
            {
                return BadRequest("Invalid Id");
            }

            var application = await _db.Applications.FindAsync(id);
            if(application == null)
            {
                return NotFound();
            }

            var status = await _db.ApplicationStatus.FindAsync(request.Status);

            if(status == null)
            {
                return BadRequest($"Status {request.Status} is invalid");
            }

            application.ApplicationStatusId = status.Id;
            _db.Entry(application).State = EntityState.Modified;
            _db.SaveChanges();

            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
    }
}
