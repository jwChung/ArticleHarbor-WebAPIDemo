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
    using Jwc.Funz;
    using WebApiPresentationModel.Controllers;
    using Keyword = DomainModel.Keyword;

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

            // Repositories
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

            container.Register<IRepositories>(
                c =>
                {
                    c.Resolve<IUnitOfWork>();
                    return new Repositories(c.Resolve<ArticleHarborDbContext>());
                })
                .ReusedWithinContainer();

            container.Register(
                c => c.Resolve<IRepositories>().Articles)
                .ReusedWithinContainer();

            // Commands
            container.Register(
                c => new InsertConfirmableCommand(
                    c.Resolve<IPrincipal>()))
                .ReusedWithinContainer();

            container.Register(
                c => new UpdateConfirmableCommand(
                    c.Resolve<IPrincipal>(),
                    c.Resolve<IRepositories>()))
                .ReusedWithinContainer();

            container.Register(
                c => new DeleteConfirmableCommand(
                    c.Resolve<IPrincipal>(),
                    c.Resolve<IRepositories>()))
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
                c => new DeleteBookmarksCommand(
                    c.Resolve<IRepositories>()))
                .ReusedWithinContainer();

            container.Register(
               c => new UpdateCommand(
                   c.Resolve<IRepositories>()))
               .ReusedWithinContainer();

            container.Register(
               c => new DeleteCommand(
                   c.Resolve<IRepositories>()))
               .ReusedWithinContainer();

            container.Register(
               c => new SelectBookmarkedArticlesCommand(
                   c.Resolve<IRepositories>()))
               .ReusedWithinContainer();

           // Controllers
            container.Register(
                c => new ArticlesController(
                    c.Resolve<IRepositories>(),
                    new CompositeCommand<IModel>(
                        c.Resolve<InsertConfirmableCommand>(),
                        new InsertCommand(
                            c.Resolve<IRepositories>(),
                            new RelayKeywordsCommand(
                                KoreanNounExtractor.Execute,
                                new InsertCommand(
                                    c.Resolve<IRepositories>(),
                                    new ModelCommand<IModel>())))),
                    new CompositeCommand<IModel>(
                        c.Resolve<UpdateConfirmableCommand>(),
                        c.Resolve<UpdateCommand>(),
                        new ConditionalCommand<IModel>(
                            c.Resolve<UpdateKeywordsCondition>(),
                            new CompositeCommand<IModel>(
                                c.Resolve<DeleteKeywordsCommand>(),
                                new RelayKeywordsCommand(
                                    KoreanNounExtractor.Execute,
                                    new InsertCommand(
                                        c.Resolve<IRepositories>(),
                                        new ModelCommand<IModel>()))))),
                    new CompositeCommand<IModel>(
                        c.Resolve<DeleteConfirmableCommand>(),
                        c.Resolve<DeleteKeywordsCommand>(),
                        c.Resolve<DeleteBookmarksCommand>(),
                        c.Resolve<DeleteCommand>())))
                .ReusedWithinContainer();

            container.Register(
                c => new BookmarksController(
                    c.Resolve<IRepositories>(),
                    c.Resolve<SelectBookmarkedArticlesCommand>(),
                    new InsertCommand(
                        c.Resolve<IRepositories>(),
                        new ModelCommand<IModel>()),
                    new ModelCommand<IModel>()))
                .ReusedWithinContainer();

            return this;
        }
    }
}