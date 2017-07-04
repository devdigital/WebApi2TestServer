namespace WebApiTestServer.Api.Controllers
{
    using System;
    using System.Web.Http;

    using WebApiTestServer.Api.Repositories;

    public class ValuesController : ApiController
    {
        private readonly IValuesRepository valuesRepository;

        public ValuesController(IValuesRepository valuesRepository)
        {
            if (valuesRepository == null)
            {
                throw new ArgumentNullException(nameof(valuesRepository));
            }

            this.valuesRepository = valuesRepository;
        }

        [HttpGet]
        [Route("api/values")]
        public IHttpActionResult GetValues()
        {
            var values = this.valuesRepository.GetValues();
            return this.Ok(values);
        }

        [HttpPost]
        [Route("api/values/{value}")]
        public IHttpActionResult AddValue(int value)
        {
            var values = this.valuesRepository.GetValues();
            return this.Ok(values);
        }
    }
}
