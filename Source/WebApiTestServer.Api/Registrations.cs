namespace WebApiTestServer.Api
{
    using System;
    using System.Collections.Generic;

    public class Registrations
    {
        public Registrations(IDictionary<Type, Type> typeRegistrations, IDictionary<Type, object> instanceRegistrations)
        {
            this.TypeRegistrations = typeRegistrations ?? new Dictionary<Type, Type>();
            this.InstanceRegistrations = instanceRegistrations ?? new Dictionary<Type, object>();
        }

        public IDictionary<Type, Type> TypeRegistrations { get; }

        public IDictionary<Type, object> InstanceRegistrations { get; }
    }
}