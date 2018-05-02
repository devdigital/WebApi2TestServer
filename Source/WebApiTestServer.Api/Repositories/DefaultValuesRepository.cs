// <copyright file="DefaultValuesRepository.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Default values repository.
    /// </summary>
    /// <seealso cref="WebApiTestServer.Api.Repositories.IValuesRepository" />
    public class DefaultValuesRepository : IValuesRepository
    {
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns>The collection of values.</returns>
        public IEnumerable<int> GetValues()
        {
            return new[] { 1, 2, 3, 4 };
        }
    }
}