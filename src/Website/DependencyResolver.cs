namespace Website
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;
    using Jwc.Funz;

    public sealed class DependencyResolver : IDependencyResolver
    {
        private Container container;

        public DependencyResolver(Container container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new DependencyResolver(this.container.CreateChild());
        }

        public T GetService<T>()
        {
            return this.container.TryResolve<T>();
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            return this.GetType().GetMethod("GetService", new Type[0])
                .MakeGenericMethod(serviceType)
                .Invoke(this, new object[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[0];
        }

        public void Dispose()
        {
            if (this.container == null)
                return;

            this.container.Dispose();
            this.container = null;
        }
    }
}