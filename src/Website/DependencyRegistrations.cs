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

        public IContainerVisitor<object> Visit(Container container)
        {
            // Transient scope
            container.Register<IAssembliesResolver>(
                c => new ArticleHarborAssembliesResolver())
                .ReusedWithinNone();
            container.Register(c => new ArticlesController())
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