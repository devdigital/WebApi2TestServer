namespace WebApiTestServer
{    
    using Owin;

    public interface ITestStartup
    {
        void Bootstrap(IAppBuilder app, Registrations registrations);            
    }
}
