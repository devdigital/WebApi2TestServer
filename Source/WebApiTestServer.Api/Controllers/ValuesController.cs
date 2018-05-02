// <copyright file="ValuesController.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Controllers
{
    using System;
    using System.Web.Http;

    using WebApiTestServer.Api.Repositories;

    /// <summary>
    /// Values controller.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ValuesController : ApiController
    {
        /// <summary>
        /// The values repository
        /// </summary>
        private readonly IValuesRepository valuesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="valuesRepository">The values repository.</param>
        /// <exception cref="ArgumentNullException">valuesRepository</exception>
        public ValuesController(IValuesRepository valuesRepository)
        {
            this.valuesRepository = valuesRepository ?? throw new ArgumentNullException(nameof(valuesRepository));
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns>The values.</returns>
        [HttpGet]
        [Route("api/values")]
        public IHttpActionResult GetValues()
        {
            var values = this.valuesRepository.GetValues();
            return this.Ok(values);
        }

        /// <summary>
        /// Adds a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        [Route("api/values/{value}")]
        public IHttpActionResult AddValue(int value)
        {
            var values = this.valuesRepository.GetValues();
            return this.Ok(values);
        }
    }
}
