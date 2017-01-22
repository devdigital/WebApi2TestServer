namespace WebApiTestServer
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Owin.Testing;

    public class TestServerFactory
    {
        private readonly ITestStartup testStartup;

        private Dictionary<Type, Type> TypeRegistrations { get; }
        
        private Dictionary<Type, object> InstanceRegistrations { get; }

        public TestServerFactory(ITestStartup testStartup)
        {
            if (testStartup == null)
            {
                throw new ArgumentNullException(nameof(testStartup));
            }

            this.testStartup = testStartup;

            this.TypeRegistrations = new Dictionary<Type, Type>();
            this.InstanceRegistrations = new Dictionary<Type, object>();
        }

        public TestServerFactory With<TInterface, TImplementation>()
        {
            if (this.TypeRegistrations.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException($"The type {typeof(TInterface).Name} has already been registered");
            }

            this.TypeRegistrations[typeof(TInterface)] = typeof(TImplementation);
            return this;
        }

        public TestServerFactory With<TInterface>(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (this.InstanceRegistrations.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException($"The type {typeof(TInterface).Name} has already been registered");                
            }

            this.InstanceRegistrations[typeof(TInterface)] = instance;
            return this;
        }

        public virtual TestServer Create()
        {
            var registrations = new Registrations(this.TypeRegistrations, this.InstanceRegistrations);
            return TestServer.Create(app => this.testStartup.Bootstrap(app, registrations));
        }
    }
}