namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using DomainModel;

    public class ApiKeyAuthenticationDispatcher : DelegatingHandler
    {
        private readonly Func<IAuthService> authServiceFactory;

        public ApiKeyAuthenticationDispatcher(Func<IAuthService> authServiceFactory)
        {
            if (authServiceFactory == null)
                throw new ArgumentNullException("authServiceFactory");

            this.authServiceFactory = authServiceFactory;
        }

        public Func<IAuthService> AuthServiceFactory
        {
            get { return this.authServiceFactory; }
        }

        public Task<HttpResponseMessage> ExecuteAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            return this.SendAsync(request, cancellationToken);
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authentication = request.Headers.Authorization;

            if (authentication == null || authentication.Scheme != "apikey")
                return await base.SendAsync(request, cancellationToken);

            User user = null;
            using (var serviceFactory = this.authServiceFactory())
                user = await serviceFactory.FindUserAsync(Guid.Parse(authentication.Parameter));

            if (user != null)
                request.GetRequestContext().Principal = new SimplePrincipal(user.Id, user.Role);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}