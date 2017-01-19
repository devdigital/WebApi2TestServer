namespace WebApiTestServer.Api.IntegrationTests.Models
{
    public class MyTestServerFactory : TestServerFactory
    {
        public MyTestServerFactory() : base(new TestStartup())
        {
        }
    }
}