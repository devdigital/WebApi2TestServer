namespace WebApiTestServer.Api.Repositories
{
    using System.Collections.Generic;

    public interface IValuesRepository
    {
        IEnumerable<int> GetValues();
    }
}