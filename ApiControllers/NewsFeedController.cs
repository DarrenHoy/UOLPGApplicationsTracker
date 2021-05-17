using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PGProgrammeApplications.ApiControllers
{
    public class NewsFeedController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            WebClient client = new WebClient();
            var response = await client.DownloadStringTaskAsync(new Uri("https://www.liverpool.ac.uk/csdsite/test-newsfeed.json"));
            return Ok(response);
        }
    }
}
