namespace Website
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.ExceptionHandling;
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

            // IUnitOfWork & Repository interfaces
            container.Register<IDatabaseInitializer<ArticleHarborDbContext>>(
                c => new ArticleHarborDbContextTestInitializer())
                .ReusedWithinContainer();

            container.Register(
                c => new ArticleHarborDbContext(
                    c.Resolve<IDatabaseInitializer<ArticleHarborDbContext>>()))
                .ReusedWithinContainer();

            container.Register(
                c => new LazyUnitOfWork(
                    () => new UnitOfWork(c.Resolve<ArticleHarborDbContext>())))
                .ReusedWithinContainer();

            container.Register<IUnitOfWork>(
                c => c.Resolve<LazyUnitOfWork>().Value)
                .ReusedWithinContainer();

            container.Register(
                c => c.Resolve<IUnitOfWork>().Articles)
                .ReusedWithinContainer();

            container.Register(
                c => c.Resolve<IUnitOfWork>().ArticleWords)
                .ReusedWithinContainer();

            // Domain services
            container.Register<IArticleService>(
                c => new ArticleService(
                    c.Resolve<IArticleRepository>(),
                    c.Resolve<IArticleWordRepository>(),
                    KoreanNounExtractor.Execute))
                .ReusedWithinContainer();

            // Presentation controllers
            container.Register(
                c => new ArticlesController(c.Resolve<IArticleService>()))
                .ReusedWithinContainer();

            return this;
        }
    }
}