namespace WebApiTestServer.Api.IntegrationTests.Tests
{
    using Ploeh.AutoFixture.Xunit2;

    using WebApiTestServer.Api.IntegrationTests.Models;

    using Xunit;

    public class ValuesTests
    {
        [Theory]
        [AutoData]
        public void Test(MyTestServerFactory testServerFactory)
        {
            using (var serverFactory = testServerFactory.Create())
            {
                var values = serverFactory.HttpClient.GetAsync("/api/values").Result.AsCollection<int>();
                Assert.Equal(new[] { 1, 2, 3 }, values);
            }
        }
    }    
}
