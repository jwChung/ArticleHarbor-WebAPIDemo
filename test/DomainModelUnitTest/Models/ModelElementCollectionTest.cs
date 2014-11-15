namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ModelElementCollectionTest : IdiomaticTest<ModelElementCollection<IKeyCollection, object>>
    {
        [Test]
        public void SutIsModelElementCollection(ModelElementCollection<IKeyCollection, object> sut)
        {
            Assert.IsAssignableFrom<IModelElementCollection<IKeyCollection, object>>(sut);
        }
    }
}