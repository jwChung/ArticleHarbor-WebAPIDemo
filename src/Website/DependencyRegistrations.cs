namespace ArticleHarbor.Website
{
    using System;
    using System.Data.Entity;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;
    using ArticleHarbor.WebApiPresentationModel;
    using DomainModel.Repositories;
    using DomainModel.Services;
    using Jwc.Funz;
    using WebApiPresentationModel.Controllers;
    using Article = DomainModel.Models.Article;
    using Keyword = DomainModel.Models.Keyword;

    public class DependencyRegistrations : IContainerVisitor<object>
    {
        public object Result
        {
            get { throw new NotSupportedException(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This rule is suppressed to register dependencies in one place.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This rule is suppressed to register dependencies in one place.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "DependencyResolver takes care of it.")]
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
                c => new Lazy<IUnitOfWork>(
                    () => new UnitOfWork(c.Resolve<ArticleHarborDbContext>())))
                .ReusedWithinContainer();

            container.Register<IUnitOfWork>(
                c => c.Resolve<Lazy<IUnitOfWork>>().Value)
                .ReusedWithinContainer();

            container.Register(
                c => c.Resolve<IUnitOfWork>().Articles)
                .ReusedWithinContainer();

            container.Register(
                c => c.Resolve<IUnitOfWork>().Keywords)
                .ReusedWithinContainer();

             container.Register(
                c => c.Resolve<IUnitOfWork>().Users)
                .ReusedWithinContainer();

             container.Register(
                 c => c.Resolve<IUnitOfWork>().Bookmarks)
                 .ReusedWithinContainer();

            // Domain services
            container.Register<IAuthService>(
                c => new AuthService(
                    c.Resolve<IUserRepository>(),
                    new EmptyDisposable()))
                .ReusedWithinContainer();

            container.Register<IArticleService>(
                c => new AuthArticleService(
                    c.Resolve<IAuthService>(),
                    new ArticleService(
                        c.Resolve<IArticleRepository>(),
                        c.Resolve<IKeywordService>())))
                .ReusedWithinContainer();

            container.Register<IKeywordService>(
                c => new KeywordService(
                    c.Resolve<IKeywordRepository>(),
                    c.Resolve<IArticleRepository>(), 
                    KoreanNounExtractor.Execute))
                .ReusedWithinContainer();

            container.Register<IBookmarkService>(
                c => new BookmarkService(
                    c.Resolve<IBookmarkRepository>(),
                    c.Resolve<IArticleRepository>()))
                .ReusedWithinContainer();

            // Presentation controllers
            container.Register(
                c => new ArticlesController(c.Resolve<IArticleService>()))
                .ReusedWithinContainer();

            container.Register(
                c => new BookmarksController(c.Resolve<IBookmarkService>()))
                .ReusedWithinContainer();

            return this;
        }
    }
}