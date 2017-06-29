namespace WebApiTestServer.Api.IntegrationTests.Models
{
    using Xunit.Abstractions;

    public class MyTestServerFactory : TestServerFactory<MyTestServerFactory>
    {
        public MyTestServerFactory() : base(new TestStartup())
        {
        }

        public MyTestServerFactory WithLogging(ITestOutputHelper output)
        {
            return this;
        }
    }
}