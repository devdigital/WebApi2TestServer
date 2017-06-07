# WebApi2TestServer

WebApi2TestServer is a simple test helper for testing APIs developed with ASP.NET Web API 2 and OWIN/Katana. 
It uses the `Microsoft.Owin.Testing` package and provides a builder pattern for registering overrides within your IoC container.

# Usage

Create a test project for your API and install the `WebApi2TestServer` package:

```
install-package WebApi2TestServer
```

The package provides a `TestServerFactory` which creates a Microsoft.Owin.Testing `TestServer`. It allows you to register types and instances via a builder pattern. 
These registrations are then passed to a startup class that you define. You should register these types and instances with your IoC container (see below).

> Note in this example we will use Autofac as our IoC container, but you can use any container. With Autofac, the last regsitration wins, so we just 
have to ensure that the registrations that are passed to our startup class are registered *after* our standard production registrations.

First, we implement `ITestStartup`. This requires a single method implementation which receives the registrations. 
Here, we convert the registrations to a domain equivalent, and pass them onto a custom bootstrapper:

```
public class SampleTestStartup : ITestStartup
{
    public void Bootstrap(IAppBuilder app, WebApiTestServer.Registrations registrations)
    {
        var domainRegistrations = new Registrations(
            registrations.TypeRegistrations,
            registrations.InstanceRegistrations);

        new Bootstrapper(app, domainRegistrations).Run();
    }
}
```

The `Bootstrapper` type here then configures our container (Autofac in this case) and registers the provided registrations *after* all other registrations:

```
public Bootstrapper(IAppBuilder app, Registrations registrations = null)
{
    if (app == null)
    {
        throw new ArgumentNullException(nameof(app));
    }

    this.app = app;
    this.registrations = registrations;
}

public void Run()
{
    var builder = new ContainerBuilder();
    
    //... normal application container registrations here
    
    if (this.registrations != null)
    {
        // Register additional registrations provided to the bootstrapper
        // These will override any previous registrations
        foreach (var typeRegistration in this.registrations.TypeRegistrations)
        {
          builder.RegisterType(typeRegistration.Value).As(typeRegistration.Key);
        }

        foreach (var instanceRegistration in this.registrations.InstanceRegistrations)
        {
          builder.RegisterInstance(instanceRegistration.Value).As(instanceRegistration.Key);
        }
    }
    
    // Register our Autofac based dependency resolver
    var container = builder.Build();
    configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
}
```

# Writing Tests

With a bootstrapper in place which takes additional registrations, and an `ITestStartup` implementation which uses the bootstrapper during tests, the next step is to use the `TestServerFactory` during tests to use your production bootstrapping with additional overrides. 

The `TestServerFactory` constructor takes an instance of `ITestStartup`. It's recommended to reduce boilerplace by creating your own test server factory which passes your `ITestStartup` implementation to the base constructor:

```
namespace WebApiTestServer.Api.IntegrationTests.Models
{
    public class SampleTestServerFactory : TestServerFactory
    {
        public SampleTestServerFactory() : base(new SampleTestStartup())
        {
        }
    }
}
```

You can then use your test server factory within your tests:

```
[Theory]
[AutoData]
public void Test(SampleTestServerFactory testServerFactory)
{
  var instance = new MyOtherService(...);
  using (var serverFactory = testServerFactory.With<IMyService, MyService>().With<IMyOtherService>(instance).Create())
  {
    var values = serverFactory.HttpClient.GetAsync("/api/values").Result.AsCollection<int>();
    Assert.Equal(new[] { 1, 2, 3 }, values);
  }
}
```

> Note this sample uses AutoFixture to allow us to inject the test server factory into the test method. This reduces the arrange part of the test.

The `TestServerFactory` provides `With<TServiceInterface, TServiceImplementation>` and `With<TServiceInterface>(object instance)` methods - these are passed to the `ITestStartup Bootstrap` method as type and instance registrations respectively. You can continue to chain the `With` calls to register additional service implementations for your tests.

The `Create` method returns a `TestServer` instance which can then be used to make HTTP requests.

If you wish to provide more specialist helper methods within the builder, then you can add them to your derived class (e.g. `SampleTestServerFactory`.

