namespace WebApiTestServer.Api.Controllers
{
    using System.Web.Http;

    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("api/values")]
        public IHttpActionResult GetValues()
        {
            return this.Ok(new[] { 1, 2, 3 });
        }
    }
}