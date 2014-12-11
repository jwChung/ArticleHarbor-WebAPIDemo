namespace ArticleHarbor.Website
{
    using System;
    using System.Web.Hosting;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.ExceptionHandling;
    using ArticleHarbor.WebApiPresentationModel;
    using DomainModel;
    using EFDataAccess;
    using EFPersistenceModel;
    using Jwc.Funz;

    public static class WebApiConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This rule can be suppressed as the place is composition root.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose하지 않아야 DedepencyResolver역할을 할 수 있음.")]
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var serverRoot = HostingEnvironment.MapPath("~");
            if (serverRoot != null)
                Environment.CurrentDirectory = HostingEnvironment.MapPath("~");

            // DependencyResolver
            var container = new Container();
            container.Accept(new DependencyRegistrations());
            config.DependencyResolver = new DependencyResolver(container);

            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Insert(
                0, new JsonCustomMediaTypeFormatter(JsonConstructorDeserializer.Deserialize));

            config.Services.Replace(
                typeof(IAssembliesResolver), new ArticleHarborAssembliesResolver());

            config.Services.Add(
                typeof(IExceptionLogger),
                new UnhandledExceptionLogger(new FileLogger(Environment.CurrentDirectory)));

            config.Filters.Add(new CommitTransactionAttribute());
            config.Filters.Add(new RollbackTransactionAttribute());

            config.MessageHandlers.Add(
                new ApiKeyAuthenticationDispatcher(
                () => new EFPersistenceModel.UserManager(
                    new ArticleHarborDbContext(
                        new ArticleHarborDbContextTestInitializer()))));

            config.MessageHandlers.Add(new PrincipalRegisteringHandler());
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
