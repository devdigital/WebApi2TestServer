using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTestServer.Api.IntegrationTests
{
    using Xunit;

    public class Foo
    {
        [Theory]
        public void Test(TestServerFactory testServerFactory)
        {
        }
    }

    public class TestServerFactory
    {
        private Dictionary<Type, Type> TypeRegistrations { get; }
        
        private Dictionary<Type, object> InstanceRegistrations { get; }

        public TestServerFactory()
        {
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
    }
}
