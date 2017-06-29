namespace WebApiTestServer.Api.Repositories
{
    using System.Collections.Generic;

    public class DefaultValuesRepository : IValuesRepository
    {
        public IEnumerable<int> GetValues()
        {
            return new[] { 1, 2, 3, 4 };
        }
    }
}