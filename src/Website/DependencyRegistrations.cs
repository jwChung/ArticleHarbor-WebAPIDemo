namespace ArticleHarbor.Website
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.DomainModel.Models;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;
    using ArticleHarbor.WebApiPresentationModel;
    using DomainModel.Repositories;
    using DomainModel.Services;
    using Jwc.Funz;
    using WebApiPresentationModel.Controllers;
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
                c => c.Resolve<ArticleHarborDbContext>().Articles)
                .ReusedWithinContainer();

            container.Register(
                c => new Lazy<IUnitOfWork>(
                    () => new UnitOfWork(c.Resolve<ArticleHarborDbContext>())))
                .ReusedWithinContainer();

            container.Register<IUnitOfWork>(
                c => c.Resolve<Lazy<IUnitOfWork>>().Value)
                .ReusedWithinContainer();

            container.Register<IArticleRepository>(
                c => new ArticleRepository(c.Resolve<ArticleHarborDbContext>()))
                .ReusedWithinContainer();

            container.Register<IKeywordRepository>(
                c => new KeywordRepository(c.Resolve<ArticleHarborDbContext>()))
                .ReusedWithinContainer();

            container.Register<IUserRepository>(
                c => new UserRepository(c.Resolve<ArticleHarborDbContext>()))
                .ReusedWithinContainer();

            container.Register<IBookmarkRepository>(
                c => new BookmarkRepository(c.Resolve<ArticleHarborDbContext>()))
                .ReusedWithinContainer();

            container.Register<IRepositories>(
                c => new Repositories(c.Resolve<ArticleHarborDbContext>()))
                .ReusedWithinContainer();

            container.Register(
                c => c.Resolve<IRepositories>().Articles)
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

            // Commands
            container.Register(
                c => new NullCommand())
                .ReusedWithinContainer();

            container.Register(
                c => new InsertConfirmableCommand(
                    c.Resolve<IPrincipal>()))
                .ReusedWithinContainer();

            container.Register(
                c => new UpdateConfirmableCommand(
                    c.Resolve<IRepositories>(),
                    c.Resolve<IPrincipal>()))
                .ReusedWithinContainer();

            container.Register(
                c => new UpdateKeywordsCondition(
                    c.Resolve<IRepositories>()))
                .ReusedWithinContainer();

            container.Register(
                c => new DeleteKeywordsCommand(
                    c.Resolve<IRepositories>()))
                .ReusedWithinContainer();

            container.Register(
               c => new UpdateCommand(
                   c.Resolve<IRepositories>()))
               .ReusedWithinContainer();

           // Presentation controllers
            container.Register(
                c => new ArticlesController(
                    c.Resolve<IArticleService>(),
                    c.Resolve<IRepositories>(),
                    new CompositeEnumerableCommand<IModel>(
                        c.Resolve<InsertConfirmableCommand>(),
                        new InsertCommand(
                            c.Resolve<IRepositories>(),
                            new RelayKeywordCommand(
                                new InsertCommand(
                                    c.Resolve<IRepositories>(),
                                    c.Resolve<NullCommand>(),
                                    new IModel[0]),
                                KoreanNounExtractor.Execute),
                            new IModel[0])),
                    new CompositeEnumerableCommand<IModel>(
                        c.Resolve<UpdateConfirmableCommand>(),
                        c.Resolve<UpdateCommand>(),
                        new ConditionalCommand<IEnumerable<IModel>>(
                            c.Resolve<UpdateKeywordsCondition>(),
                            new CompositeEnumerableCommand<IModel>(
                                c.Resolve<DeleteKeywordsCommand>(),
                                new RelayKeywordCommand(
                                new InsertCommand(
                                    c.Resolve<IRepositories>(),
                                    c.Resolve<NullCommand>(),
                                    new IModel[0]),
                                KoreanNounExtractor.Execute))))))
                .ReusedWithinContainer();

            container.Register(
                c => new BookmarksController(c.Resolve<IBookmarkService>()))
                .ReusedWithinContainer();

            return this;
        }
    }
}