namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net.Http;
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
    }
}