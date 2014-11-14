namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ModelElementCollectionTest : IdiomaticTest<ModelElementCollection<IIdentity, object>>
    {
        [Test]
        public void SutIsModelElementCollection(ModelElementCollection<IIdentity, object> sut)
        {
            Assert.IsAssignableFrom<IModelElementCollection<IIdentity, object>>(sut);
        }
    }
}