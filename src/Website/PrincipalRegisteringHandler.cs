namespace ArticleHarbor.Website
{
    using System;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    internal class PrincipalRegisteringHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var dependencyResolver = (DependencyResolver)request.GetDependencyScope();
            dependencyResolver.Container.Register(c => request.GetRequestContext().Principal)
                .ReusedWithinContainer();

            return base.SendAsync(request, cancellationToken);
        }
    }
}