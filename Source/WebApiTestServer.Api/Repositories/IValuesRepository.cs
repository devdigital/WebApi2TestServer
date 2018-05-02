// <copyright file="IValuesRepository.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace WebApiTestServer.Api.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Values repository.
    /// </summary>
    public interface IValuesRepository
    {
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns>The collection of values.</returns>
        IEnumerable<int> GetValues();
    }
}