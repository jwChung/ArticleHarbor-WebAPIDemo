namespace Website
{
    using System;
    using System.Web.Http.Dispatcher;
    using DomainModel;
    using EFDataAccess;
    using EFPersistenceModel;
    using Jwc.Funz;
    using WebApiPresentationModel;

    public class DependencyRegistrations : IContainerVisitor<object>
    {
        public object Result
        {
            get { throw new NotSupportedException(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "DependencyResolver가 알아서 Dispose함.")]
        public IContainerVisitor<object> Visit(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            // Transient scope
            container.Register<IAssembliesResolver>(
                c => new ArticleHarborAssembliesResolver())
                .ReusedWithinNone();
            container.Register(c => new ArticlesController(c.Resolve<IArticleRepository>()))
                .ReusedWithinNone();

            // Request scope
            container.Register<IDatabaseContext>(
                c => new DatabaseContext(new ArticleHarborContext()))
                .ReusedWithinContainer();
            container.Register<IArticleRepository>(
                c => c.Resolve<IDatabaseContext>().Articles)
                .ReusedWithinContainer();

            return this;
        }
    }
}