namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public abstract class RepositoryTest<TKeys, TModel, TPersistence>
        : IdiomaticTest<Repository<TKeys, TModel, TPersistence>>
        where TKeys : IKeyCollection
        where TModel : IModel
        where TPersistence : class
    {
        [Test]
        public void SutIsRepository(Repository<TKeys, TModel, TPersistence> sut)
        {
            Assert.IsAssignableFrom<IRepository<TKeys, TModel>>(sut);
        }

        [Test]
        public void InitializeWithNullDatabaseThrows(IFixture fixture)
        {
            fixture.Inject<Database>(null);
            var e = Assert.Throws<TargetInvocationException>(
                () => fixture.Create<Repository<TKeys, TModel, TPersistence>>());
            Assert.IsType<ArgumentNullException>(e.InnerException);
        }

        [Test]
        public void InitializeWithNullDbSetThrows(IFixture fixture)
        {
            fixture.Inject<DbSet<TPersistence>>(null);
            var e = Assert.Throws<TargetInvocationException>(
                () => fixture.Create<Repository<TKeys, TModel, TPersistence>>());
            Assert.IsType<ArgumentNullException>(e.InnerException);
        }

        [Test]
        public void DatabaseIsCorrect(
            [Frozen] Database expected,
            Repository<TKeys, TModel, TPersistence> sut)
        {
            var actual = sut.Database;
            Assert.Equal(expected, actual);
        }

        [Test]
        public void DbSetIsCorrect(
            [Frozen] DbSet<TPersistence> expected,
            Repository<TKeys, TModel, TPersistence> sut)
        {
            var actual = sut.DbSet;
            Assert.Equal(expected, actual);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Database);
            yield return this.Properties.Select(x => x.DbSet);
        }
    }

    public class RepositoryOfArticleTest : RepositoryTest<KeyCollection<int>, Article, EFDataAccess.Article>
    {
        [Test]
        public void FindAsyncReturnsCorrectResult(TssArticleRepository sut)
        {
            var keys = new KeyCollection<int>(1);
            var article = sut.DbSet.Find(keys.ToArray());

            Article actual = sut.FindAsync(keys).Result;
            
            article.AsSource().OfLikeness<Article>()
                .Without(x => x.UserId)
                .ShouldEqual(actual);
        }

        [Test]
        public void SelectAsyncReturnsCorrectResult(TssArticleRepository sut)
        {
            var articles = sut.DbSet.AsNoTracking().ToArray();

            var actual = sut.SelectAsync().Result.ToArray();

            Assert.Equal(3, actual.Length);
            int index = 0;
            foreach (var article in articles)
                article.AsSource().OfLikeness<Article>()
                    .Without(x => x.UserId)
                    .ShouldEqual(actual[index++]);
            Assert.Equal(0, sut.DbSet.Local.Count);
        }
    }

    public class TssArticleRepository : Repository<KeyCollection<int>, Article, EFDataAccess.Article>
    {
        public TssArticleRepository(Database database, DbSet<EFDataAccess.Article> dbSet)
            : base(database, dbSet)
        {
        }

        public override Article ConvertToModel(EFDataAccess.Article persistence)
        {
            return new Article(
                persistence.Id,
                persistence.Provider,
                persistence.Guid,
                persistence.Subject,
                persistence.Body,
                persistence.Date,
                persistence.Url,
                persistence.User.UserName);
        }

        public override EFDataAccess.Article ConvertToPersistence(Article model)
        {
            throw new NotImplementedException();
        }
    }
}