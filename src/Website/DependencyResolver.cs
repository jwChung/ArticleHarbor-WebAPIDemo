﻿namespace ArticleHarbor.Website
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;
    using Jwc.Funz;

    public sealed class DependencyResolver : IDependencyResolver
    {
        private readonly Container container;
        private bool disposed = false;

        public DependencyResolver(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
        }

        public Container Container
        {
            get { return this.container; }
        }

        public IDependencyScope BeginScope()
        {
            return new DependencyResolver(this.Container.CreateChild());
        }

        public T GetService<T>()
        {
            return this.Container.TryResolve<T>();
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
            var services = (IEnumerable<object>)this.GetService(
                typeof(IEnumerable<>).MakeGenericType(serviceType));

            return services == null
                ? (IEnumerable<object>)Array.CreateInstance(serviceType, 0)
                : services;
        }

        public void Dispose()
        {
            if (this.disposed)
                return;

            this.Container.Dispose();
            this.disposed = true;
        }
    }
}