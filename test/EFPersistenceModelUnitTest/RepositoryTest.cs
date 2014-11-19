namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Reflection;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
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

    public class RepositoryOfFooAndBarTest : RepositoryTest<KeyCollection<int>, Article, EFDataAccess.Article>
    {
    }
}