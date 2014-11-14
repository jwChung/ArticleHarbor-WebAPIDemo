namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ModelElementCollectionTest : IdiomaticTest<ModelElementCollection<IId, object>>
    {
        [Test]
        public void SutIsModelElementCollection(ModelElementCollection<IId, object> sut)
        {
            Assert.IsAssignableFrom<IModelElementCollection<IId, object>>(sut);
        }
    }
}