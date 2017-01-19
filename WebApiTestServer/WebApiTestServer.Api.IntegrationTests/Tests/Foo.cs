namespace WebApiTestServer.Api.IntegrationTests.Tests
{
    using WebApiTestServer.Api.IntegrationTests.Models;

    using Xunit;

    public class Foo
    {
        [Theory]
        public void Test(MyTestServerFactory testServerFactory)
        {
            using (var serverFactory = testServerFactory.Create())
            {               
            }
        }
    }
}
