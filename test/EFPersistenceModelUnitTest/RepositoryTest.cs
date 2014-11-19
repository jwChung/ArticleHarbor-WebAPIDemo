namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using Xunit;

    public abstract class RepositoryTest<TKeys, TModel, TPersistence>
        : IdiomaticTest<Repository<TKeys, TModel, TPersistence>>
        where TKeys : IKeyCollection
        where TModel : IModel
    {
        [Test]
        public void SutIsRepository(Repository<TKeys, TModel, TPersistence> sut)
        {
            Assert.IsAssignableFrom<IRepository<TKeys, TModel>>(sut);
        }
    }

    public class RepositoryOfFooAndBarTest : RepositoryTest<KeyCollection<int>, Article, EFDataAccess.Article>
    {
    }
}