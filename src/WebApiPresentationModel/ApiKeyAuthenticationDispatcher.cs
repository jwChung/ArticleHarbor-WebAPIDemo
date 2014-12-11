namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Models;
    using DomainModel.Repositories;

    public class ApiKeyAuthenticationDispatcher : DelegatingHandler
    {
        private readonly Func<IUserManager> userManagerFactory;

        public ApiKeyAuthenticationDispatcher(Func<IUserManager> userManagerFactory)
        {
            if (userManagerFactory == null)
                throw new ArgumentNullException("userManagerFactory");

            this.userManagerFactory = userManagerFactory;
        }

        public Func<IUserManager> UserManagerFactory
        {
            get { return this.userManagerFactory; }
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
            using (var serviceFactory = this.userManagerFactory())
                user = await serviceFactory.FindAsync(Guid.Parse(authentication.Parameter));

            if (user != null)
                request.GetRequestContext().Principal = new SimplePrincipal(user.Id, user.Role);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}